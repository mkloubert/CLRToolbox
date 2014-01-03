// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.ApplicationServer.WebInterface;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Composition;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Data;
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
        #region Fields (12)

        private WebInterfaceHandler _webHandler;
        /// <summary>
        /// The name of the config category for the server database.
        /// </summary>
        public const string CONFIG_CATEGORY_DATABASE = "database";
        /// <summary>
        /// The name of the config category for the server web interface.
        /// </summary>
        public const string CONFIG_CATEGORY_WEBINTERFACE = "web_interface";
        /// <summary>
        /// The name of the config category for the server database connection string.
        /// </summary>
        public const string CONFIG_VALUE_CONNECTION_STRING = "connection_string";
        /// <summary>
        /// The name of the config category for the TCP port of the server's web interface.
        /// </summary>
        public const string CONFIG_VALUE_PORT = "port";
        /// <summary>
        /// The name of the config category for the thumbprint of the SSL certificate of the server's web interface.
        /// </summary>
        public const string CONFIG_VALUE_SSL_THUMBPRINT = "ssl_thumbprint";
        /// <summary>
        /// The name of the config category for the flag if HTTPs should be used for the server's web interface or not.
        /// </summary>
        public const string CONFIG_VALUE_USE_HTTPS = "use_https";
        /// <summary>
        /// Name of database provider for ADO.NET Microsoft SQL database connection.
        /// </summary>
        public const string DB_PROVIDER_ADONET_MSSQL = "ado_mssql";
        /// <summary>
        /// Name of database provider for ADO.NET ODBC database connection.
        /// </summary>
        public const string DB_PROVIDER_ADONET_ODBC = "ado_odbc";
        /// <summary>
        /// Name of database provider for ADO.NET OLE database connection.
        /// </summary>
        public const string DB_PROVIDER_ADONET_OLE = "ado_oledb";
        /// <summary>
        /// The default config value for the flag if HTTPs should be used for the server's webinterface or not.
        /// </summary>
        public const bool DEFAULT_CONFIG_VALUE_USE_HTTPS = false;
        /// <summary>
        /// The default config value for TCP port of the server's webinterface.
        /// </summary>
        public const int DEFAULT_CONFIG_VALUE_WEBINTERFACE_PORT = 5979;

        #endregion Fields

        #region Properties (11)

        /// <summary>
        /// Gets the command line arguments of the server.
        /// </summary>
        public string[] Arguments
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
        /// Gets the (startup) configuration.
        /// </summary>
        public IConfigRepository StartupConfig
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the composition catalog for trusted assembies and types.
        /// </summary>
        public StrongNamedAssemblyPartCatalog TrustedCompositionCatalog
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

        #region Methods (21)

        // Public Methods (6) 

        /// <summary>
        /// Tries to return the server database connection by startup config.
        /// </summary>
        /// <param name="doOpen">Open connection (if found) or not.</param>
        /// <returns>The connection or <see langword="null" /> if not found.</returns>
        public IDbConnection TryGetServerDbConnectionByConfig(bool doOpen = false)
        {
            string dbProvider;
            return this.TryGetServerDbConnectionByConfig(dbProvider: out dbProvider,
                                                         doOpen: doOpen);
        }

        /// <summary>
        /// Tries to return the server database connection by startup config.
        /// </summary>
        /// <param name="dbProvider">The variable where to write the name of the database provider from startup config to.</param>
        /// <param name="doOpen">Open connection (if found) or not.</param>
        /// <returns>The connection or <see langword="null" /> if not found.</returns>
        public IDbConnection TryGetServerDbConnectionByConfig(out string dbProvider,
                                                              bool doOpen = false)
        {
            var conn = this.TryGetServerDbConnectionDataByConfig(out dbProvider);
            if (conn != null)
            {
                if (doOpen)
                {
                    return conn.OpenConnection();
                }
                else
                {
                    return conn.GetConnection();
                }
            }

            return null;
        }

        /// <summary>
        /// Tries to return the <see cref="IAdoDataConnection" /> based on the current startup configuration.
        /// </summary>
        /// <returns>The connection data or <see langword="null" /> if not found.</returns>
        public IAdoDataConnection TryGetServerDbConnectionDataByConfig()
        {
            string dbProvider;
            return this.TryGetServerDbConnectionDataByConfig(out dbProvider);
        }

        /// <summary>
        /// Tries to return the <see cref="IAdoDataConnection" /> based on the current startup configuration.
        /// </summary>
        /// <param name="dbProvider">The variable where to write the name of the database provider from startup config to.</param>
        /// <returns>The connection data or <see langword="null" /> if not found.</returns>
        public IAdoDataConnection TryGetServerDbConnectionDataByConfig(out string dbProvider)
        {
            IAdoDataConnection result = null;

            var provider = this.TryGetServerDbProviderByConfig(out dbProvider);
            if (provider != null)
            {
                var genericAdoType = typeof(global::MarcelJoachimKloubert.CLRToolbox.Data.AdoDataConnection<>);
                var adoType = genericAdoType.MakeGenericType(provider);

                var connStr = this.StartupConfig
                                  .GetValue<string>(category: CONFIG_CATEGORY_DATABASE,
                                                    name: CONFIG_VALUE_CONNECTION_STRING);

                result = (global::MarcelJoachimKloubert.CLRToolbox.Data.IAdoDataConnection)Activator.CreateInstance(adoType,
                                                                                                                    args: new object[] { connStr });
            }

            return result;
        }

        /// <summary>
        /// Tries to return the provider of the server database based on the current startup configuration.
        /// </summary>
        /// <returns>The provider or <see langword="null" /> if not found.</returns>
        public Type TryGetServerDbProviderByConfig()
        {
            string dbProvider;
            return this.TryGetServerDbProviderByConfig(out dbProvider);
        }

        /// <summary>
        /// Tries to return the provider of the server database based on the current startup configuration.
        /// </summary>
        /// <param name="dbProvider">The variable where to write the name of the database provider from startup config to.</param>
        /// <returns>The provider or <see langword="null" /> if not found.</returns>
        public Type TryGetServerDbProviderByConfig(out string dbProvider)
        {
            this.StartupConfig
                   .TryGetValue(category: CONFIG_CATEGORY_DATABASE,
                                name: "provider",
                                value: out dbProvider,
                                defaultVal: DB_PROVIDER_ADONET_MSSQL);

            switch ((dbProvider ?? string.Empty).ToLower().Trim())
            {
                case DB_PROVIDER_ADONET_MSSQL:
                    return typeof(global::System.Data.SqlClient.SqlConnection);

                case DB_PROVIDER_ADONET_ODBC:
                    return typeof(global::System.Data.Odbc.OdbcConnection);

                case DB_PROVIDER_ADONET_OLE:
                    return typeof(global::System.Data.OleDb.OleDbConnection);
            }

            return null;
        }
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

            this.StartupConfig = new IniFileConfigRepository(Path.Combine(this.WorkingDirectory,
                                                                   "config.ini"),
                                                      false);

            var trustedAssemblyKeys = this.LoadTrustedAssemblyKeyList();

            this.EntityAssemblies = new SynchronizedCollection<Assembly>();
            this.RefreshEntityAssemblyList();

            // service locator
            CompositionContainer compContainer;
            AggregateCatalog compCatalog;
            DelegateServiceLocator serviceLocator;
            {
                compCatalog = new AggregateCatalog();
                compCatalog.Catalogs
                           .Add(this.TrustedCompositionCatalog = new StrongNamedAssemblyPartCatalog(trustedAssemblyKeys));

                this.ReinitTrustedCompositionCatalog();

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
            compContainer.ComposeExportedValue<global::MarcelJoachimKloubert.ApplicationServer.IAppServer>(this);

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

            // var db = ServiceLocator.Current.GetInstance<MarcelJoachimKloubert.ApplicationServer.DataLayer.IAppServerDatabase>();
            // var test = db.Query<MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.PersonTypes>();
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
        // Private Methods (12) 

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

        private IEnumerable<FileInfo> FilterTrustedAssemblyFiles(IEnumerable<FileInfo> files)
        {
            return files.Where(f =>
                               {
                                   try
                                   {
                                       var asmName = AssemblyName.GetAssemblyName(f.FullName);

                                       var result = this.TrustedCompositionCatalog
                                                         .IsTrustedAssembly(asmName);
                                       if (!result)
                                       {
                                           this.Logger
                                               .Log(categories: LoggerFacadeCategories.Warnings,
                                                    tag: "ApplicationServer::FilterTrustedAssemblyFiles",
                                                    msg: string.Format("'{0}' ('{1}') is no trusted assembly!",
                                                                       asmName.FullName,
                                                                       f.FullName));
                                       }

                                       return result;
                                   }
                                   catch
                                   {
                                       return false;
                                   }
                               });
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

        private void LoadAndAddTrustedAssemblies(IEnumerable<FileInfo> asmFiles,
                                                 string logTagPrefix)
        {
            ICollection<Assembly> asmList = new SynchronizedCollection<Assembly>();

            var ex = this.LoadTrustedAssemblies(asmFiles,
                                                asmList);

            if (ex != null)
            {
                this.Logger
                    .Log(msg: ex,
                         tag: logTagPrefix + "LoadAndAddTrustedAssemblies",
                         categories: LoggerFacadeCategories.Errors);
            }

            if (asmList.Count > 0)
            {
                this.Logger
                    .Log(msg: string.Format("{0} assemblies were loaded.", asmList.Count),
                         tag: logTagPrefix + "LoadAndAddTrustedAssemblies",
                         categories: LoggerFacadeCategories.Information);
            }
            else
            {
                this.Logger
                    .Log(msg: "No assembly was loaded.",
                         tag: logTagPrefix + "LoadAndAddTrustedAssemblies",
                         categories: LoggerFacadeCategories.Warnings);
            }

            this.TrustedCompositionCatalog
                .AddAssemblies(asmList);
        }

        private AggregateException LoadTrustedAssemblies(IEnumerable<FileInfo> asmFiles, ICollection<Assembly> asmList)
        {
            return this.FilterTrustedAssemblyFiles(asmFiles)
                       .ForAllAsync(ctx =>
                       {
                           var f = ctx.Item;

                           var asmBlob = File.ReadAllBytes(f.FullName);
                           var asm = Assembly.Load(asmBlob);

                           ctx.State
                              .Assemblies
                              .Add(asm);
                       }, actionState: new
                        {
                            Assemblies = asmList,
                        }, throwExceptions: false);
        }

        private List<byte[]> LoadTrustedAssemblyKeyList()
        {
            var result = new List<byte[]>()
                {
                    this.GetType().Assembly.GetName().GetPublicKey(),
                };

            using (var dbConn = this.TryGetServerDbConnectionByConfig(doOpen: true))
            {
                if (dbConn != null)
                {
                    using (var cmd = dbConn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TrustedAssemblyKey FROM [Security].[TrustedAssemblies];";

                        foreach (var rec in ((IDbCommand)cmd).ExecuteEnumerableReader())
                        {
                            result.Add(rec.GetBytes(0));
                        }
                    }
                }
            }

            return result;
        }

        private void RefreshEntityAssemblyList()
        {
            this.EntityAssemblies
                .Clear();

            this.EntityAssemblies
                .Add(typeof(global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.IGeneralEntity).Assembly);

            foreach (var asm in (this.Modules ?? Enumerable.Empty<IAppServerModule>()).Select(m => m.GetType().Assembly)
                                                                                      .Distinct())
            {
                if (!this.EntityAssemblies.Contains(asm))
                {
                    this.EntityAssemblies
                        .Add(asm);
                }
            }
        }

        private void ReinitTrustedCompositionCatalog()
        {
            this.TrustedCompositionCatalog.Clear();

            this.TrustedCompositionCatalog
                .AddAssembly(this.GetType().Assembly);
        }

        private void StartServer()
        {
            const string LOG_TAG_PREFIX = "ApplicationServer::StartServer::";

            AggregateException ex = null;

            this.ReinitTrustedCompositionCatalog();

            // assemblies with services
            {
                var serviceDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "services")).CreateDirectoryDeep();

                this.LoadAndAddTrustedAssemblies(serviceDir.GetFiles("*.dll"),
                                                 LOG_TAG_PREFIX);
            }

            // assemblies with functions
            {
                var funcDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "funcs")).CreateDirectoryDeep();

                this.LoadAndAddTrustedAssemblies(funcDir.GetFiles("*.dll"),
                                                 LOG_TAG_PREFIX);
            }

            // web interface
            {
                IList<Assembly> webInterfaceAssemblies = new SynchronizedCollection<Assembly>();

                var webDir = new DirectoryInfo(Path.Combine(this.WorkingDirectory, "web")).CreateDirectoryDeep();
                this.LoadAndAddTrustedAssemblies(webDir.GetFiles("*.dll"),
                                                 LOG_TAG_PREFIX);

                try
                {
                    this.DisposeOldWebInterfaceServer();

                    //TODO read from configuration
                    var newWebInterfaceServer = ServiceLocator.Current.GetInstance<IHttpServer>();
                    {
                        // HTTPs ?
                        {
                            bool? useHttps;
                            this.StartupConfig
                                .TryGetValue<bool?>(category: CONFIG_CATEGORY_WEBINTERFACE,
                                                    name: CONFIG_VALUE_USE_HTTPS,
                                                    value: out useHttps,
                                                    defaultVal: DEFAULT_CONFIG_VALUE_USE_HTTPS);

                            newWebInterfaceServer.UseSecureHttp = useHttps ?? DEFAULT_CONFIG_VALUE_USE_HTTPS;
                        }

                        // TCP port
                        {
                            int? port;
                            this.StartupConfig
                                .TryGetValue<int?>(category: CONFIG_CATEGORY_WEBINTERFACE,
                                                   name: CONFIG_VALUE_PORT,
                                                   value: out port,
                                                   defaultVal: DEFAULT_CONFIG_VALUE_WEBINTERFACE_PORT);

                            newWebInterfaceServer.Port = port ?? DEFAULT_CONFIG_VALUE_WEBINTERFACE_PORT;
                        }

                        if (newWebInterfaceServer.UseSecureHttp)
                        {
                            // SSL thumbprint
                            {
                                newWebInterfaceServer.SetSslCertificateByThumbprint(this.StartupConfig
                                                                                        .GetValue<IEnumerable<char>>(category: CONFIG_CATEGORY_WEBINTERFACE,
                                                                                                                     name: CONFIG_VALUE_SSL_THUMBPRINT));
                            }
                        }
                    }

                    var newHandler = new WebInterfaceHandler(this, newWebInterfaceServer);
                    this._webHandler = newHandler;

                    newHandler.Start();
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

                                            var trustedCatalog = ctx.State.CompositionCatalog.Clone(cloneCatalogData: true);
                                            var asmName = AssemblyName.GetAssemblyName(f.FullName);
                                            if (!trustedCatalog.IsTrustedAssembly(asmName))
                                            {
#if DEBUG
                                                var s = string.Format("INSERT INTO [Security].[TrustedAssemblies] (TrustedAssemblyKey, Name) VALUES (0x{0}, N'{1}');",
                                                                      asmName.GetPublicKey().AsHexString(),
                                                                      asmName.FullName);

#endif
                                                ctx.State
                                                   .Logger
                                                   .Log(categories: LoggerFacadeCategories.Warnings,
                                                        msg: string.Format("'{0}' is no trusted module!",
                                                                           f.FullName));

                                                return;
                                            }

                                            var asmBlob = File.ReadAllBytes(f.FullName);
                                            var asm = Assembly.Load(asmBlob);

                                            trustedCatalog.AddAssembly(asm);

                                            CompositionContainer container;
                                            DelegateServiceLocator serviceLocator;
                                            {
                                                var catalog = new AggregateCatalog();
                                                catalog.Catalogs.Add(trustedCatalog);

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

                                                               var moduleRootDir = new DirectoryInfo(Path.Combine(ctx2.State.ModuleDirectory,
                                                                                                                  m.Name)).CreateDirectoryDeep();

                                                               var moduleCtx = new SimpleAppServerModuleContext(m);
                                                               moduleCtx.Config = new IniFileConfigRepository(Path.Combine(moduleRootDir.FullName,
                                                                                                                           "config.ini"));
                                                               moduleCtx.InnerServiceLocator = ctx2.State.ServiceLocator;
                                                               moduleCtx.Logger = logger;
                                                               moduleCtx.OtherModules = ctx2.State.AllModules
                                                                                                  .Where(CreateWherePredicateForExtractingOtherModules(m));
                                                               moduleCtx.SetAssemblyFile(ctx2.State.AssemblyFile.FullName);

                                                               var moduleInitCtx = new SimpleAppServerModuleInitContext();
                                                               moduleInitCtx.ModuleContext = moduleCtx;
                                                               moduleInitCtx.RootDirectory = moduleRootDir.FullName;

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
                                                   CompositionCatalog = this.TrustedCompositionCatalog
                                                                            .Clone(cloneCatalogData: false),
                                                   Logger = this.Logger,
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

            this.RefreshEntityAssemblyList();

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
