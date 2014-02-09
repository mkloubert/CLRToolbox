// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes._Impl;
using MarcelJoachimKloubert.CloudNET.Classes._Impl.Security;
using MarcelJoachimKloubert.CloudNET.Classes._Impl.Sessions;
using MarcelJoachimKloubert.CloudNET.Handlers;
using System;
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
            var pricRepo = new PrincipalRepository();

            // app context
            var app = new CloudApp();
            {
                this.Application[APP_VAR_APPCONTEXT] = app;
            }

            this.Application[APP_VAR_PRINCIPALS] = pricRepo;

            // files
            RouteTable.Routes.Add(new Route
                (
                    "files",
                    new FileHttpHandler()
                ));
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
