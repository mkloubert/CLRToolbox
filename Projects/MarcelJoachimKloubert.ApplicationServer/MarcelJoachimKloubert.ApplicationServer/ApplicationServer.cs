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
using MarcelJoachimKloubert.ApplicationServer.WebInterface;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
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
        #region Fields (1)

        private WebInterfaceHandler _webHandler;

        #endregion Fields

        #region Properties (13)

        /// <summary>
        /// Gets the command line arguments of the server.
        /// </summary>
        public string[] Arguments
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the (startup) configuration.
        /// </summary>
        public IConfigRepository Config
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of 
        /// </summary>
        public IList<Assembly> EntityAssemblies
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the composition catalog for server functions.
        /// </summary>
        public AggregateCatalog FunctionCompositionCatalog
        {
            get;
            private set;
        }

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

        /// <summary>
        /// Gets the logger that handles logger delegates. This logger is part of <see cref="ApplicationServer.Logger" />.
        /// </summary>
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
        /// Gets the composition catalog for the web interface modules.
        /// </summary>
        public AggregateCatalog WebInterfaceCompositionCatalog
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (9)

        // Protected Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnInitialize(IAppServerInitContext, ref bool)" />
        protected override void OnInitialize(IAppServerInitContext initContext, ref bool isInitialized)
        {
            this.Arguments = (initContext.Arguments ?? Enumerable.Empty<string>()).OfType<string>()
                                                                                  .ToArray();

            this.WorkingDirectory = initContext.WorkingDirectory;
            if (string.IsNullOrWhiteSpace(this.WorkingDirectory))
            {
                this.WorkingDirectory = Environment.CurrentDirectory;
            }

            this.Config = new IniFileConfigRepository(Path.Combine(this.WorkingDirectory,
                                                                   "config.ini"),
                                                      false);

            //TODO: load dynamically
            this.EntityAssemblies = new SynchronizedCollection<Assembly>()
                {
                    typeof(global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Structure.IPersons).Assembly,
                };

            // service locator
            CompositionContainer compContainer;
            AggregateCatalog compCatalog;
            DelegateServiceLocator serviceLocator;
            {
                compCatalog = new AggregateCatalog();
                compCatalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));
                compCatalog.Catalogs
                           .Add(this.FunctionCompositionCatalog = new AggregateCatalog());
                compCatalog.Catalogs
                           .Add(this.ServiceCompositionCatalog = new AggregateCatalog());
                compCatalog.Catalogs
                           .Add(this.WebInterfaceCompositionCatalog = new AggregateCatalog());

                compContainer = new CompositionContainer(compCatalog,
                                                         isThreadSafe: true);

                var mefServiceLocator = new ExportProviderServiceLocator(compContainer);
                serviceLocator = new DelegateServiceLocator(mefServiceLocator);

                serviceLocator.RegisterSingleProvider<global::MarcelJoachimKloubert.CLRToolbox.Templates.Text.Html.IHtmlTemplate>(WebInterfaceHandler.GetHtmlTemplate);
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

            compContainer.ComposeExportedValue<global::MarcelJoachimKloubert.ApplicationServer.ApplicationServer>(this);

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

            // assemblies with services
            {
                IList<Assembly> serviceAssemblies = new SynchronizedCollection<Assembly>();

                var serviceDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "services")).CreateDirectoryDeep();
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
                        .Log(msg: string.Format("{0} service assemblies were loaded.", serviceAssemblies.Count),
                             tag: LOG_TAG_PREFIX + "LoadServices",
                             categories: LoggerFacadeCategories.Information);
                }
                else
                {
                    this.Logger
                        .Log(msg: "No service assembly was loaded.",
                             tag: LOG_TAG_PREFIX + "LoadServices",
                             categories: LoggerFacadeCategories.Warnings);
                }
            }

            // assemblies with functions
            {
                IList<Assembly> funcAssemblies = new SynchronizedCollection<Assembly>();

                var funcDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "funcs")).CreateDirectoryDeep();
                {
                    ex = funcDir.GetFiles("*.dll")
                                .ForAllAsync(ctx =>
                                 {
                                     var f = ctx.Item;

                                     var asmBlob = File.ReadAllBytes(f.FullName);
                                     var asm = Assembly.Load(asmBlob);

                                     ctx.State
                                        .Assemblies.Add(asm);
                                 }, actionState: new
                                 {
                                     Assemblies = funcAssemblies,
                                 }, throwExceptions: false);

                    if (ex != null)
                    {
                        this.Logger
                            .Log(msg: ex,
                                 tag: LOG_TAG_PREFIX + "LoadFunctions",
                                 categories: LoggerFacadeCategories.Errors);
                    }
                }

                this.FunctionCompositionCatalog.Catalogs.Clear();
                foreach (var asm in funcAssemblies)
                {
                    this.FunctionCompositionCatalog
                        .Catalogs
                        .Add(new AssemblyCatalog(asm));
                }

                if (funcAssemblies.Count > 0)
                {
                    this.Logger
                        .Log(msg: string.Format("{0} function assemblies were loaded.", funcAssemblies.Count),
                             tag: LOG_TAG_PREFIX + "LoadFunctions",
                             categories: LoggerFacadeCategories.Information);
                }
                else
                {
                    this.Logger
                        .Log(msg: "No function assembly was loaded.",
                             tag: LOG_TAG_PREFIX + "LoadFunctions",
                             categories: LoggerFacadeCategories.Warnings);
                }
            }

            // web interface
            {
                IList<Assembly> webInterfaceAssemblies = new SynchronizedCollection<Assembly>();

                var webDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "web")).CreateDirectoryDeep();
                {
                    ex = webDir.GetFiles("*.dll")
                               .ForAllAsync(ctx =>
                                            {
                                                var f = ctx.Item;

                                                var asmBlob = File.ReadAllBytes(f.FullName);
                                                var asm = Assembly.Load(asmBlob);

                                                ctx.State
                                                   .Assemblies.Add(asm);
                                            }, actionState: new
                                            {
                                                Assemblies = webInterfaceAssemblies,
                                            }, throwExceptions: false);

                    if (ex != null)
                    {
                        this.Logger
                            .Log(msg: ex,
                                 tag: LOG_TAG_PREFIX + "LoadWebInterfaceModules",
                                 categories: LoggerFacadeCategories.Errors);
                    }
                }

                this.WebInterfaceCompositionCatalog.Catalogs.Clear();
                foreach (var asm in webInterfaceAssemblies)
                {
                    this.WebInterfaceCompositionCatalog
                        .Catalogs
                        .Add(new AssemblyCatalog(asm));
                }

                if (webInterfaceAssemblies.Count > 0)
                {
                    this.Logger
                        .Log(msg: string.Format("{0} web interface assemblies were loaded.", webInterfaceAssemblies.Count),
                             tag: LOG_TAG_PREFIX + "LoadWebInterfaceModules",
                             categories: LoggerFacadeCategories.Information);
                }
                else
                {
                    this.Logger
                        .Log(msg: "No web interface assembly was loaded.",
                             tag: LOG_TAG_PREFIX + "LoadWebInterfaceModules",
                             categories: LoggerFacadeCategories.Warnings);
                }

                try
                {
                    this.DisposeOldWebInterfaceServer();

                    //TODO read from configuration
                    var newWebInterfaceServer = ServiceLocator.Current.GetInstance<IHttpServer>();
                    newWebInterfaceServer.Port = 5979;
                    newWebInterfaceServer.UseSecureHttp = true;
                    newWebInterfaceServer.SetSslCertificateByThumbprint("‎7f 01 5f 4a fc a0 da 76 36 fa f0 a9 d6 e6 e8 cc 63 53 a9 bc");

                    this._webHandler = new WebInterfaceHandler(this, newWebInterfaceServer);

                    newWebInterfaceServer.Start();
                }
                catch (Exception e)
                {
                    this.Logger
                        .Log(msg: e.GetBaseException() ?? e,
                             tag: LOG_TAG_PREFIX + "WebInterface",
                             categories: LoggerFacadeCategories.Errors);

                    this.DisposeOldWebInterfaceServer();
                }
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
        // Private Methods (6) 

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
                using (var srv = this._webHandler)
                {
                    this._webHandler = null;
                }
            }
            catch (Exception ex)
            {
                this.Logger
                    .Log(msg: ex.GetBaseException() ?? ex,
                         tag: LOG_TAG_PREFIX + "Dispose",
                         categories: LoggerFacadeCategories.Errors | LoggerFacadeCategories.Debug);
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

            var modDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "modules")).CreateDirectoryDeep();
            {
                ex = modDir.GetFiles("*.dll")
                           .ForAllAsync(ctx =>
                                        {
                                            var f = ctx.Item;

                                            var asmBlob = File.ReadAllBytes(f.FullName);
                                            var asm = Assembly.Load(asmBlob);

                                            CompositionContainer container;
                                            DelegateServiceLocator serviceLocator;
                                            {
                                                var catalog = new AggregateCatalog();
                                                catalog.Catalogs.Add(new AssemblyCatalog(asm));

                                                container = new CompositionContainer(catalog,
                                                                                     isThreadSafe: true);

                                                serviceLocator = new DelegateServiceLocator(new ExportProviderServiceLocator(container));
                                            }

                                            var modules = serviceLocator.GetAllInstances<IAppServerModule>().AsArray();
                                            if (modules.Length == 1)
                                            {
                                                CompositionHelper.ComposeExportedValueEx(container,
                                                                                         modules[0]);
                                            }
                                            else
                                            {
                                                foreach (var m in modules)
                                                {
                                                    CompositionHelper.ComposeExportedValue(container,
                                                                                           m,
                                                                                           m.GetType());
                                                }
                                            }

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
                                                               moduleCtx.InnerServiceLocator = ctx2.State.ServiceLocator;
                                                               moduleCtx.Logger = logger;
                                                               moduleCtx.OtherModules = ctx2.State.AllModules
                                                                                                  .Where(CreateWherePredicateForExtractingOtherModules(m));
                                                               moduleCtx.SetAssemblyFile(ctx2.State.AssemblyFile.FullName);

                                                               var moduleInitCtx = new SimpleAppServerModuleInitContext();
                                                               moduleInitCtx.ModuleContext = moduleCtx;
                                                               moduleInitCtx.RootDirectory = new DirectoryInfo(Path.Combine(ctx2.State.ModuleDirectory,
                                                                                                                            m.Name)).CreateDirectoryDeep()
                                                                                                                                    .FullName;

                                                               m.Initialize(moduleInitCtx);

                                                               ctx2.State
                                                                   .NewModules
                                                                   .Add(m);
                                                           }, actionState: new
                                                           {
                                                               AllModules = modules,
                                                               AssemblyFile = f,
                                                               ModuleDirectory = ctx.State.ModuleDirectory,
                                                               NewModules = ctx.State.NewModules,
                                                               ServiceLocator = serviceLocator,
                                                           }, throwExceptions: true);
                                        }, actionState: new
                                               {
                                                   ModuleDirectory = modDir.FullName,
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

            ex = newModules.Where(m => m.CanStart &&
                                       m.IsInitialized)
                           .ForAllAsync(ctx =>
                                {
                                    var m = ctx.Item;

                                    m.Start();
                                }, throwExceptions: false);

            if (ex != null)
            {
                this.Logger
                    .Log(msg: ex,
                         tag: LOG_TAG_PREFIX + "StartModules",
                         categories: LoggerFacadeCategories.Errors);
            }
        }

        #endregion Methods
    }
}
