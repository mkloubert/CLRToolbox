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

namespace MarcelJoachimKloubert.DragNBatch.ViewModel
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public sealed class MainViewModel : NotificationObjectBase
    {
        #region Fields (2)

        private bool _isRunning;
        private IPlugIn _selectedPlugIn;

        #endregion Fields

        #region Properties (3)

        /// <summary>
        /// Gets if a batch process is currently running or not.
        /// </summary>
        public bool IsRunning
        {
            get { return this._isRunning; }

            private set { this.SetProperty(ref this._isRunning, value); }
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

            set { this.SetProperty(ref this._selectedPlugIn, value); }
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (1) 

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

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnConstructor()
        {
            this.PlugIns = new SynchronizedObservableCollection<IPlugIn>();
        }

        #endregion Methods
    }
}