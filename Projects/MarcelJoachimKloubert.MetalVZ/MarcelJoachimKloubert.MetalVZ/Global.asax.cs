// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl;
using MarcelJoachimKloubert.MetalVZ.Classes._Impl.Sessions;
using MarcelJoachimKloubert.MetalVZ.Classes.Sessions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Web;

namespace MarcelJoachimKloubert.MetalVZ
{
    /// <summary>
    /// The ASP.NET application context.
    /// </summary>
    public class Global : global::System.Web.HttpApplication
    {
        #region Fields (1)

        public const string SESSION_VAR_MVZSESSION = "MetalVZSession";

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

        #region Methods (11)

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

            var config = new IniFileConfigRepository(filePath: Path.Combine(rootDir.FullName,
                                                                            "config.ini"),
                                                     isReadOnly: false);

            var sessionMgr = new MVZSessionManager();
            sessionMgr.CurrentSessionProvider = GetCurrentSession;

            // ServiceLocator
            {
                this.GlobalCompositionCatalog = new AggregateCatalog();
                this.GlobalCompositionCatalog.Catalogs.Add(new AssemblyCatalog(typeof(global::MarcelJoachimKloubert.MetalVZ.Classes.IMVZObject).Assembly));

                this.GlobalCompositionContainer = new CompositionContainer(this.GlobalCompositionCatalog, true);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.Configuration.IConfigRepository>(config.MakeReadOnly());
                this.GlobalCompositionContainer.ComposeExportedValue<global::System.Web.HttpApplication>(this);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.MetalVZ.Classes.Sessions.IMVZSessionManager>(sessionMgr);

                var innerLocator = new ExportProviderServiceLocator(this.GlobalCompositionContainer);

                this.GlobalServiceLocator = new DelegateServiceLocator(innerLocator);
                this.GlobalServiceLocator
                    .RegisterSingleProvider<global::MarcelJoachimKloubert.MetalVZ.Classes.Sessions.IMVZSession>(this.GetSingleSession, false)
                    .RegisterMultiProvider<global::MarcelJoachimKloubert.MetalVZ.Classes.Sessions.IMVZSession>(this.GetAllSessions, false);

                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.IServiceLocator>(this.GlobalServiceLocator);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl.DelegateServiceLocator>(this.GlobalServiceLocator);
                this.GlobalCompositionContainer.ComposeExportedValue<global::MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl.ExportProviderServiceLocator>(innerLocator);

                ServiceLocator.SetLocator(this.GlobalServiceLocator);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            var sessionMgr = ServiceLocator.Current.GetInstance<IMVZSessionManager>();

            var session = (IMVZSession)this.Session[SESSION_VAR_MVZSESSION];

            this.Session.Remove(SESSION_VAR_MVZSESSION);
            sessionMgr.Unregister(session);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var now = DateTimeOffset.Now;

            var sessionMgr = ServiceLocator.Current.GetInstance<IMVZSessionManager>();

            var newSession = new MVZSession();
            newSession.StartTime = now;
            newSession.SystemIdProvider = GetSessionIdBySystem;

            this.Session.Add(SESSION_VAR_MVZSESSION, newSession);
            sessionMgr.Register(newSession);
        }
        // Private Methods (4) 

        private IEnumerable<IMVZSession> GetAllSessions(IServiceLocator baseLocator, object key)
        {
            var sessions = baseLocator.GetInstance<IMVZSessionManager>()
                                      .GetSessions();

            if (key.IsNull())
            {
                return sessions;
            }

            var id = GlobalConverter.Current.ChangeType<Guid>(key);
            return sessions.Where(s => s.Id == id);
        }

        private static IMVZSession GetCurrentSession()
        {
            return (IMVZSession)HttpContext.Current.Session[SESSION_VAR_MVZSESSION];
        }

        private static string GetSessionIdBySystem()
        {
            return HttpContext.Current.Session.SessionID;
        }

        private IMVZSession GetSingleSession(IServiceLocator baseLocator, object key)
        {
            if (key.IsNull())
            {
                return GetCurrentSession();
            }

            var id = GlobalConverter.Current.ChangeType<Guid>(key);
            return CollectionHelper.Single(this.GetAllSessions(baseLocator, key),
                                           s => s.Id == id);
        }

        #endregion Methods
    }
}
