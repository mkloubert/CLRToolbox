// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes._Impl;
using MarcelJoachimKloubert.CloudNET.Classes._Impl.Security;
using MarcelJoachimKloubert.CloudNET.Classes._Impl.Sessions;
using MarcelJoachimKloubert.CloudNET.Handlers;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Web.Routing;

namespace MarcelJoachimKloubert.CloudNET
{
    /// <summary>
    /// Represents the running web application.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        #region Fields (3)

        /// <summary>
        /// The name of the application variable for storing the global application context.
        /// </summary>
        public const string APP_VAR_APPCONTEXT = "ApplicationContext";
        /// <summary>
        /// The name of the application variable for storing the principal repository.
        /// </summary>
        public const string APP_VAR_PRINCIPALS = "Principals";
        /// <summary>
        /// The name of the session variable that stores the session context object.
        /// </summary>
        public const string SESSION_VAR_CLOUDSESSION = "CloudSession";

        #endregion Fields

        #region Properties (3)

        public AggregateCatalog GlobalCompositionCatalog
        {
            get;
            private set;
        }

        public CompositionContainer GlobalCompositionContainer
        {
            get;
            private set;
        }

        public DelegateServiceLocator GlobalServiceLocator
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (7)

        // Protected Methods (7) 

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            this.Application.Remove(APP_VAR_PRINCIPALS);
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Application_Start(object sender, EventArgs e)
        {
            var rootDir = new DirectoryInfo(this.Server.MapPath("~/bin"));

            // app context
            var app = new CloudApp();
            {
                IConfigRepository config;
                var configIni = new FileInfo(Path.Combine(rootDir.FullName,
                                                          "config.ini"));
                if (configIni.Exists)
                {
                    config = new IniFileConfigRepository(configIni);
                }
                else
                {
                    config = new KeyValuePairConfigRepository();
                }

                app.Config = config.MakeReadOnly();
            }

            // principal repository
            var pricRepo = new PrincipalRepository();
            {
                pricRepo.LocalDataDirectory = rootDir.FullName;
                {
                    string temp;
                    if (app.Config.TryGetValue<string>("Data", out temp, "Directories") &&
                        string.IsNullOrWhiteSpace(temp) == false)
                    {
                        pricRepo.LocalDataDirectory = temp.Trim();
                    }
                }

                var iniFile = new FileInfo(Path.Combine(rootDir.FullName, "users.ini"));
                if (iniFile.Exists)
                {
                    pricRepo.UserRepository = new IniFileConfigRepository(iniFile);
                }
            }

            // files
            RouteTable.Routes.Add(new Route
                (
                    "files",
                    new FilesHttpHandler()
                ));

            // ServiceLocator
            {
                this.GlobalCompositionCatalog = new AggregateCatalog();
                this.GlobalCompositionCatalog.Catalogs.Add(new AssemblyCatalog(typeof(global::MarcelJoachimKloubert.CloudNET.Classes.ICloudApp).Assembly));

                this.GlobalCompositionContainer = new CompositionContainer(this.GlobalCompositionCatalog, true);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CloudNET.Classes.ICloudApp>(app);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CloudNET.Classes.Security.IPrincipalRepository>(pricRepo);
                this.GlobalCompositionContainer.ComposeExportedValue<global::System.Web.HttpApplication>(this);

                var innerLocator = new ExportProviderServiceLocator(this.GlobalCompositionContainer);

                this.GlobalServiceLocator = new DelegateServiceLocator(innerLocator);

                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.IServiceLocator>(this.GlobalServiceLocator);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl.DelegateServiceLocator>(this.GlobalServiceLocator);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl.ExportProviderServiceLocator>(innerLocator);

                ServiceLocator.SetLocator(this.GlobalServiceLocator);
            }

            this.Application[APP_VAR_APPCONTEXT] = app;
            this.Application[APP_VAR_PRINCIPALS] = pricRepo;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            this.Session.Remove(SESSION_VAR_CLOUDSESSION);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            this.Session[SESSION_VAR_CLOUDSESSION] = new CloudSession();
        }

        #endregion Methods
    }
}
