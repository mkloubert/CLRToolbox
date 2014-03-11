// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Web;

namespace MarcelJoachimKloubert.ServerAdmin
{
    /// <summary>
    /// The global application class.
    /// </summary>
    public class Global : HttpApplication
    {
        #region Fields (1)

        /// <summary>
        /// Stores the session variable that contains the session ID.
        /// </summary>
        public const string SESSION_VAR_SESSIONID = "SERVERADMIN_SESSION_ID";

        #endregion Fields

        #region Properties (3)

        /// <summary>
        /// Gets the global composition catalog.
        /// </summary>
        public AggregateCatalog GlobalCompositionCatalog
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the global composition container.
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

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Application_Start(object sender, EventArgs e)
        {
            var rootDir = new DirectoryInfo(this.Server.MapPath("~/bin"));

            var config = new IniFileConfigRepository(filePath: Path.Combine(rootDir.FullName, "config.ini"),
                                                     isReadOnly: false);

            // ServiceLocator
            {
                this.GlobalCompositionCatalog = new AggregateCatalog();

                // assemblies
                {
                    var asms = new HashSet<Assembly>();
                    asms.Add(typeof(global::MarcelJoachimKloubert.ServerAdmin.IServerAdminObject).Assembly);
                    asms.Add(this.GetType().Assembly);

                    foreach (var a in asms)
                    {
                        this.GlobalCompositionCatalog.Catalogs.Add(new AssemblyCatalog(a));
                    }
                }

                this.GlobalCompositionContainer = new CompositionContainer(this.GlobalCompositionCatalog, true);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.Configuration.IConfigRepository>(config.MakeReadOnly());
                this.GlobalCompositionContainer.ComposeExportedValue<global::System.Web.HttpApplication>(this);

                var innerLocator = new ExportProviderServiceLocator(this.GlobalCompositionContainer);

                this.GlobalServiceLocator = new DelegateServiceLocator(innerLocator);

                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.IServiceLocator>(this.GlobalServiceLocator);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl.DelegateServiceLocator>(this.GlobalServiceLocator);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl.ExportProviderServiceLocator>(innerLocator);

                ServiceLocator.SetLocator(this.GlobalServiceLocator);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            this.Session.Remove(SESSION_VAR_SESSIONID);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            this.Session[SESSION_VAR_SESSIONID] = Guid.NewGuid();
        }

        #endregion Methods
    }
}
