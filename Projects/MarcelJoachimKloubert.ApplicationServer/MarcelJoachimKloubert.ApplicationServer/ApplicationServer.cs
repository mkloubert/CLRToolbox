// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Common implementation of <see cref="IAppServer" /> interface.
    /// </summary>
    public class ApplicationServer : AppServerBase
    {
        #region Properties (9)

        /// <summary>
        /// Gets the global catalog for the instance of <see cref="ApplicationServer.GlobalCompositionContainer" />.
        /// </summary>
        public AggregateCatalog GlobalCompositionCatalog
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the global container for the instance of <see cref="ApplicationServer.GlobalServiceLocator" />.
        /// </summary>
        public CompositionContainer GlobalCompositionContainer
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the global service locator.
        /// </summary>
        public DelegateServiceLocator GlobalServiceLocator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the logger of that server.
        /// </summary>
        public AggregateLogger Logger
        {
            get;
            private set;
        }

        public DelegateLogger LoggerFuncs
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current list of modules.
        /// </summary>
        public IAppServerModule[] Modules
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the composition catalog for the services.
        /// </summary>
        public AggregateCatalog ServiceCompositionCatalog
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the HTTP server for the web interface.
        /// </summary>
        public IHttpServer WebInterface
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

        #region Methods (10)

        // Protected Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnInitialize(IAppServerInitContext, ref bool)" />
        protected override void OnInitialize(IAppServerInitContext initContext, ref bool isInitialized)
        {
            // service locator
            CompositionContainer compContainer;
            AggregateCatalog compCatalog;
            DelegateServiceLocator serviceLocator;
            {
                compCatalog = new AggregateCatalog();
                compCatalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));
                compCatalog.Catalogs
                           .Add(this.ServiceCompositionCatalog = new AggregateCatalog());

                compContainer = new CompositionContainer(compCatalog,
                                                         isThreadSafe: true);

                var mefServiceLocator = new ExportProviderServiceLocator(compContainer);
                serviceLocator = new DelegateServiceLocator(mefServiceLocator);
            }

            // logger
            AggregateLogger logger;
            DelegateLogger loggerFuncs;
            {
                loggerFuncs = new DelegateLogger();

                logger = new AggregateLogger();
                logger.Add(loggerFuncs);

                var outerLogger = initContext.Logger;
                if (outerLogger != null)
                {
                    logger.Add(outerLogger);
                }

                serviceLocator.RegisterMultiProvider(this.GetAllLoggers, false);

                compContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.ILoggerFacade>(new AsyncLogger(logger));
            }

            this.LoggerFuncs = loggerFuncs;
            this.Logger = logger;

            this.GlobalCompositionCatalog = compCatalog;
            this.GlobalCompositionContainer = compContainer;

            this.GlobalServiceLocator = serviceLocator;
            ServiceLocator.SetLocatorProvider(this.GetGlobalServiceLocator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnStart(AppServerBase.StartStopContext, ref bool)" />
        protected override void OnStart(AppServerBase.StartStopContext context, ref bool isRunning)
        {
            const string LOG_TAG_PREFIX = "ApplicationServer::OnStart::";

            AggregateException ex = null;

            switch (context)
            {
                case StartStopContext.Start:
                    this.StartServer();
                    break;

                case StartStopContext.Restart:
                    var moduleList = this.Modules;
                    if (moduleList != null)
                    {
                        ex = moduleList.Where(m => m.CanRestart)
                                       .ForAllAsync(ctx =>
                                                    {
                                                        var m = ctx.Item;

                                                        m.Restart();
                                                    }, throwExceptions: false);

                        if (ex != null)
                        {
                            this.Logger
                                .Log(msg: ex,
                                     tag: LOG_TAG_PREFIX + "Restart",
                                     categories: LoggerFacadeCategories.Errors);
                        }
                    }
                    else
                    {
                        this.StartServer();
                    }
                    break;
            }

            IList<Assembly> serviceAssemblies = new SynchronizedCollection<Assembly>();

            var serviceDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "services"));
            if (serviceDir.Exists)
            {
                ex = serviceDir.GetFiles("*.dll")
                               .ForAllAsync(ctx =>
                               {
                                   var f = ctx.Item;

                                   var asmBlob = File.ReadAllBytes(f.FullName);
                                   var asm = Assembly.Load(asmBlob);

                                   ctx.State
                                      .Assemblies.Add(asm);
                               }, actionState: new
                               {
                                   Assemblies = serviceAssemblies,
                               }, throwExceptions: false);

                if (ex != null)
                {
                    this.Logger
                        .Log(msg: ex,
                             tag: LOG_TAG_PREFIX + "LoadServices",
                             categories: LoggerFacadeCategories.Errors);
                }
            }

            this.ServiceCompositionCatalog.Catalogs.Clear();
            foreach (var asm in serviceAssemblies)
            {
                this.ServiceCompositionCatalog
                    .Catalogs
                    .Add(new AssemblyCatalog(asm));
            }

            if (serviceAssemblies.Count > 0)
            {
                this.Logger
                    .Log(msg: string.Format("{0} services assemblies were loaded.", serviceAssemblies.Count),
                         tag: LOG_TAG_PREFIX + "LoadModules",
                         categories: LoggerFacadeCategories.Information);
            }
            else
            {
                this.Logger
                    .Log(msg: "No service assembly was loaded.",
                         tag: LOG_TAG_PREFIX + "LoadModules",
                         categories: LoggerFacadeCategories.Warnings);
            }

            try
            {
                this.DisposeOldWebInterfaceServer();

                var newWebInterfaceServer = ServiceLocator.Current.GetInstance<IHttpServer>();
                newWebInterfaceServer.Port = 23979;
                newWebInterfaceServer.HandleRequest += this.WebInterface_HandleRequest;

                this.WebInterface = newWebInterfaceServer;

                newWebInterfaceServer.Start();
            }
            catch (Exception e)
            {
                this.Logger
                    .Log(msg: e.GetBaseException() ?? e,
                         tag: LOG_TAG_PREFIX + "WebInterface",
                         categories: LoggerFacadeCategories.Errors);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnStop(AppServerBase.StartStopContext, ref bool)" />
        protected override void OnStop(AppServerBase.StartStopContext context, ref bool isRunning)
        {
            const string LOG_TAG_PREFIX = "ApplicationServer::OnStop::";

            this.DisposeOldWebInterfaceServer();

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
                this.Logger
                    .Log(msg: ex,
                         tag: LOG_TAG_PREFIX + "StopModules",
                         categories: LoggerFacadeCategories.Errors);
            }
        }
        // Private Methods (7) 

        private static Func<IAppServerModule, bool> CreateWherePredicateForExtractingOtherModules(IAppServerModule module)
        {
            return new Func<IAppServerModule, bool>((otherModule) => !object.ReferenceEquals(module, otherModule) &&
                                                                     otherModule.IsInitialized);
        }

        private void DisposeOldWebInterfaceServer()
        {
            const string LOG_TAG_PREFIX = "ApplicationServer::DisposeOldHttpServer::";

            try
            {
                using (var srv = this.WebInterface)
                {
                    if (srv != null)
                    {
                        this.WebInterface = null;

                        srv.HandleRequest -= this.WebInterface_HandleRequest;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger
                    .Log(msg: ex.GetBaseException() ?? ex,
                         tag: LOG_TAG_PREFIX + "Dispose",
                         categories: LoggerFacadeCategories.Errors);
            }
        }

        private IEnumerable<ILoggerFacade> GetAllLoggers(IServiceLocator baseLocator, object key)
        {
            if (!IsNullOrDBNull(key))
            {
                return null;
            }

            return this.Logger
                       .Flatten();
        }

        private IServiceLocator GetGlobalServiceLocator()
        {
            return this.GlobalServiceLocator;
        }

        private static bool IsNullOrDBNull(object key)
        {
            return key == null ||
                   DBNull.Value.Equals(key);
        }

        private void StartServer()
        {
            const string LOG_TAG_PREFIX = "ApplicationServer::StartServer::";

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
                this.Logger
                    .Log(msg: ex,
                         tag: LOG_TAG_PREFIX + "UnloadOldModules",
                         categories: LoggerFacadeCategories.Errors);
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

                                            var modules = serviceLocator.GetAllInstances<IAppServerModule>().AsArray();
                                            modules.ForAll(ctx2 =>
                                                           {
                                                               var m = ctx2.Item;
                                                               if (m.IsInitialized)
                                                               {
                                                                   // no need to initialize
                                                                   return;
                                                               }

                                                               //TODO: add implementation(s)
                                                               var logger = new AggregateLogger();

                                                               var moduleCtx = new SimpleAppServerModuleContext(m);
                                                               moduleCtx.AssemblyContent = ctx2.State.AssemblyContent;
                                                               moduleCtx.InnerServiceLocator = ctx2.State.ServiceLocator;
                                                               moduleCtx.Logger = logger;
                                                               moduleCtx.OtherModules = ctx2.State.AllModules
                                                                                                  .Where(CreateWherePredicateForExtractingOtherModules(m));
                                                               moduleCtx.SetAssemblyFile(ctx2.State.AssemblyFile.FullName);

                                                               var moduleInitCtx = new SimpleAppServerModuleInitContext();
                                                               moduleInitCtx.ModuleContext = moduleCtx;

                                                               m.Initialize(moduleInitCtx);

                                                               ctx2.State
                                                                   .NewModules
                                                                   .Add(m);
                                                           }, actionState: new
                                                           {
                                                               AllModules = modules,
                                                               AssemblyContent = asmBlob,
                                                               AssemblyFile = f,
                                                               NewModules = ctx.State.NewModules,
                                                               ServiceLocator = serviceLocator,
                                                           }, throwExceptions: true);
                                        }, actionState: new
                                        {
                                            NewModules = newModules,
                                        }, throwExceptions: false);

                if (ex != null)
                {
                    this.Logger
                        .Log(msg: ex,
                             tag: LOG_TAG_PREFIX + "LoadModules",
                             categories: LoggerFacadeCategories.Errors);
                }
            }

            this.Modules = newModules.Where(m => m.IsInitialized)
                                     .ToArray();

            if (this.Modules.Length > 0)
            {
                this.Logger
                    .Log(msg: string.Format("{0} modules were loaded.", this.Modules.Length),
                         tag: LOG_TAG_PREFIX + "LoadModules",
                         categories: LoggerFacadeCategories.Information);
            }
            else
            {
                this.Logger
                    .Log(msg: "No module was loaded.",
                         tag: LOG_TAG_PREFIX + "LoadModules",
                         categories: LoggerFacadeCategories.Warnings);
            }
        }

        private void WebInterface_HandleRequest(object sender, HttpRequestEventArgs e)
        {
            const string LOG_TAG_PREFIX = "ApplicationServer::WebInterface_HandleRequest::";

            try
            {
                //TODO
            }
            catch (Exception ex)
            {
                this.Logger
                    .Log(msg: ex.GetBaseException() ?? ex,
                         tag: LOG_TAG_PREFIX + "HandleRequest",
                         categories: LoggerFacadeCategories.Errors);
            }
        }

        #endregion Methods
    }
}
