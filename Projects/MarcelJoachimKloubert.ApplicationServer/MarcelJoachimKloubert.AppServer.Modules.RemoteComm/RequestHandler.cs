// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm
{
    internal sealed class RequestHandler : DisposableBase
    {
        #region Fields (2)

        private readonly RemoteCommModule _MODULE;
        private readonly IHttpServer _SERVER;

        #endregion Fields

        #region Constructors (1)

        internal RequestHandler(RemoteCommModule module, IHttpServer server)
        {
            this._MODULE = module;
            this._SERVER = server;

            server.CredentialValidator = this.Server_ValidateCredential;
            server.PrincipalFinder = this.Server_FindPrincipal;
            server.HandleRequest += this.Server_HandleRequest;
        }

        #endregion Constructors

        #region Methods (5)

        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this._SERVER.Dispose();
            }
        }
        // Private Methods (3) 

        private IPrincipal Server_FindPrincipal(IIdentity id)
        {
            return null;
        }

        private void Server_HandleRequest(object sender, HttpRequestEventArgs e)
        {
            var statusCode = HttpStatusCode.OK;
            IEnumerable<char> statusDescription = null;

            switch (e.Request.Method)
            {
                case "POST":
                    {

                    }
                    break;

                default:
                    statusCode = HttpStatusCode.MethodNotAllowed;
                    break;
            }

            e.Response.StatusCode = statusCode;
            e.Response.StatusDescription = statusDescription.AsString();
        }

        private bool Server_ValidateCredential(string username, string password)
        {
            return false;
        }
        // Internal Methods (1) 

        internal void Start()
        {
            this._SERVER.Start();
        }

        #endregion Methods
    }
}
