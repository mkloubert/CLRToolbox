// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Composition;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl;
using MarcelJoachimKloubert.DragNBatch.PlugIns;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MarcelJoachimKloubert.DragNBatch.ViewModel
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public sealed class MainViewModel : NotificationObjectBase
    {
        #region Fields (2)

        private IPlugIn _selectedPlugIn;
        private Task _task;

        #endregion Fields

        #region Properties (5)

        /// <summary>
        /// Gets if the application can currently handling files and directories or not.
        /// </summary>
        public bool CanHandleFiles
        {
            get
            {
                return this.SelectedPlugIn != null &&
                       this.IsRunning == false;
            }
        }

        /// <summary>
        /// Gets if a batch process is currently running or not.
        /// </summary>
        public bool IsRunning
        {
            get { return this.Task != null; }
        }

        /// <summary>
        /// Gets the list of loaded plugins.
        /// </summary>
        public SynchronizedObservableCollection<IPlugIn> PlugIns
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected plug in.
        /// </summary>
        public IPlugIn SelectedPlugIn
        {
            get { return this._selectedPlugIn; }

            set
            {
                if (this.SetProperty(ref this._selectedPlugIn, value))
                {
                    this.OnPropertyChanged(() => this.CanHandleFiles);
                }
            }
        }

        /// <summary>
        /// Gets the current running task.
        /// </summary>
        public Task Task
        {
            get { return this._task; }

            private set
            {
                if (this.SetProperty(ref this._task, value))
                {
                    this.OnPropertyChanged(() => this.IsRunning);
                    this.OnPropertyChanged(() => this.CanHandleFiles);
                }
            }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (2) 

        /// <summary>
        /// Reloads the list of plugins.
        /// </summary>
        public void ReloadPlugIns()
        {
            try
            {
                var loadedPlugIns = new List<IPlugIn>();

                var plugInDir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "PlugIns"));
                if (plugInDir.Exists)
                {
                    this.PlugIns.Clear();

                    foreach (var file in plugInDir.GetFiles("*.dll"))
                    {
                        try
                        {
                            var asmBlob = File.ReadAllBytes(file.FullName);
                            var asm = Assembly.Load(asmBlob);

                            var catalog = new AssemblyCatalog(asm);

                            var ctx = new PlugInContext();
                            ctx.Assembly = asm;
                            ctx.AssemblyFile = file.FullName;

                            var container = new CompositionContainer(catalog,
                                                                     isThreadSafe: true);
                            container.ComposeExportedValue<global::MarcelJoachimKloubert.DragNBatch.PlugIns.IPlugInContext>(ctx);

                            var instances = new MultiInstanceComposer<IPlugIn>(container);
                            instances.RefeshIfNeeded();

                            // service locator
                            {
                                var mefLocator = new ExportProviderServiceLocator(container);

                                var sl = new DelegateServiceLocator(mefLocator);

                                ctx.ServiceLocator = sl;
                            }

                            var initializedPlugIns = new List<IPlugIn>();
                            foreach (var i in instances.Instances)
                            {
                                try
                                {
                                    i.Initialize(ctx);

                                    initializedPlugIns.Add(i);
                                }
                                catch (Exception ex)
                                {
                                    this.OnError(ex);
                                }
                            }

                            ctx.PlugIns = initializedPlugIns.ToArray();
                            loadedPlugIns.AddRange(initializedPlugIns);
                        }
                        catch (Exception ex)
                        {
                            this.OnError(ex);
                        }
                    }
                }

                App.Current
                   .BeginInvoke((a, state) =>
                       {
                           try
                           {
                               // clear
                               state.ViewModel
                                    .PlugIns
                                    .Clear();

                               // add plug ins
                               state.ViewModel
                                    .PlugIns
                                    .AddRange(state.LoadedPlugIns);
                           }
                           catch (Exception ex)
                           {
                               this.OnError(ex);
                           }
                       }, new
                       {
                           LoadedPlugIns = loadedPlugIns.ToArray(),
                           ViewModel = this,
                       });
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        /// <summary>
        /// Starts handling files.
        /// </summary>
        /// <param name="plugIn">The plugin that should handle files.</param>
        /// <param name="context">The underlying context.</param>
        public bool HandleFiles(IPlugIn plugIn, IHandleFilesContext context)
        {
            bool result = false;
            Exception occuredException = null;

            lock (this._SYNC)
            {
                if (this.IsRunning)
                {
                    try
                    {
                        var newTask = this.CreateHandleFilesTask(plugIn, context);
                        this.Task = newTask;

                        newTask.Start();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        this.Task = null;
                        occuredException = ex;
                    }
                }
            }

            if (occuredException != null)
            {
                this.OnError(occuredException);
            }

            return result;
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnConstructor()
        {
            this.PlugIns = new SynchronizedObservableCollection<IPlugIn>();
        }

        // Private Methods (1) 
        private Task CreateHandleFilesTask(IPlugIn plugIn, IHandleFilesContext ctx)
        {
            return new Task(() =>
                {
                    try
                    {
                        plugIn.HandleFiles(ctx);
                    }
                    catch (Exception ex)
                    {
                        this.OnError(ex);
                    }
                    finally
                    {
                        this.Task = null;
                    }
                });
        }

        #endregion Methods
    }
}