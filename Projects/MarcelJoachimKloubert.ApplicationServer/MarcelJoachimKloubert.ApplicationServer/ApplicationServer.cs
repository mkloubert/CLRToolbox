// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Common implementation of <see cref="IAppServer" /> interface.
    /// </summary>
    public class ApplicationServer : AppServerBase
    {
        #region Properties (2)

        /// <summary>
        /// Gets the current list of modules.
        /// </summary>
        public IAppServerModule[] Modules
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get { return Environment.CurrentDirectory; }
        }

        #endregion Properties

        #region Methods (3)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnStart(AppServerBase.StartStopContext, ref bool)" />
        protected override void OnStart(AppServerBase.StartStopContext context, ref bool isRunning)
        {
            switch (context)
            {
                case StartStopContext.Start:
                    this.StartServer();
                    break;

                case StartStopContext.Restart:
                    var moduleList = this.Modules;
                    if (moduleList != null)
                    {
                        var ex = moduleList.Where(m => m.CanRestart)
                                           .ForAllAsync(ctx =>
                                                {
                                                    var m = ctx.Item;

                                                    m.Restart();
                                                }, throwExceptions: false);

                        if (ex != null)
                        {
                            //TODO: log
                        }
                    }
                    else
                    {
                        this.StartServer();
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnStop(AppServerBase.StartStopContext, ref bool)" />
        protected override void OnStop(AppServerBase.StartStopContext context, ref bool isRunning)
        {
            var moduleList = this.Modules;
            if (moduleList == null)
            {
                return;
            }

            var ex = moduleList.Where(m => m.CanStop)
                               .ForAllAsync(ctx =>
                                            {
                                                var m = ctx.Item;

                                                m.Stop();
                                            }, throwExceptions: false);

            if (ex != null)
            {
                //TODO: log
            }
        }
        // Private Methods (1) 

        private void StartServer()
        {
            AggregateException ex = null;

            var moduleList = this.Modules;
            if (moduleList != null)
            {
                ex = moduleList.OfType<global::System.IDisposable>()
                               .ForAllAsync(ctx =>
                               {
                                   var m = ctx.Item;
                                   var doDispose = true;

                                   var dispObj = m as ITMDisposable;
                                   if (dispObj != null)
                                   {
                                       doDispose = !dispObj.IsDisposed;
                                   }

                                   if (doDispose)
                                   {
                                       m.Dispose();
                                   }
                               }, throwExceptions: false);
            }

            if (ex != null)
            {
                //TODO: log
            }

            IList<IAppServerModule> newModules = new SynchronizedCollection<IAppServerModule>();

            var modDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "modules"));
            if (modDir.Exists)
            {
                ex = modDir.GetFiles("*.dll")
                           .ForAllAsync(ctx =>
                                        {
                                            var f = ctx.Item;
                                            var asmBlob = File.ReadAllBytes(f.FullName);

                                            var asm = Assembly.Load(asmBlob);

                                            DelegateServiceLocator serviceLocator;
                                            {
                                                var catalog = new AggregateCatalog();
                                                catalog.Catalogs.Add(new AssemblyCatalog(asm));

                                                var container = new CompositionContainer(catalog,
                                                                                         isThreadSafe: true);

                                                serviceLocator = new DelegateServiceLocator(new ExportProviderServiceLocator(container));
                                            }

                                            var modules = serviceLocator.GetAllInstances<IAppServerModule>().ToArray();
                                            foreach (var m in modules)
                                            {
                                                var moduleCtx = new SimpleAppServerModuleContext(m);
                                                moduleCtx.AssemblyContent = asmBlob;
                                                moduleCtx.SetAssemblyFile(f.FullName);
                                                moduleCtx.InnerServiceLocator = serviceLocator;

                                                //TODO initiaize by sending module context to module.

                                                ctx.State
                                                   .NewModules
                                                   .Add(m);
                                            }
                                        }, actionState: new
                                        {
                                            NewModules = newModules,
                                        }, throwExceptions: false);
            }

            //TODO filter out these modules that were successfully initialized
            this.Modules = newModules.AsArray();
        }

        #endregion Methods
    }
}
