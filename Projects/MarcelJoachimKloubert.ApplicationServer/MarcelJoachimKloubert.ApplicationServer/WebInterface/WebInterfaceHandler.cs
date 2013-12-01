// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using System.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    internal sealed class WebInterfaceHandler : DisposableBase
    {
        #region Fields (2)

        private readonly ApplicationServer _APP_SERVER;
        private readonly IHttpServer _HTTP_SERVER;

        #endregion Fields

        #region Constructors (1)

        internal WebInterfaceHandler(ApplicationServer appServer, IHttpServer httpServer)
        {
            this._APP_SERVER = appServer;
            this._HTTP_SERVER = httpServer;

            this._HTTP_SERVER.RequestValidator = this.ValidateRequest;
            this._HTTP_SERVER.PrincipalFinder = this.FindPrincipal;

            this._HTTP_SERVER.HandleRequest += this.HandleRequest;
        }

        #endregion Constructors

        #region Properties (1)

        internal ILoggerFacade Logger
        {
            get { return this._APP_SERVER.Logger; }
        }

        #endregion Properties

        #region Methods (6)

        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            this._HTTP_SERVER.HandleRequest -= this.HandleRequest;

            this._HTTP_SERVER.RequestValidator = null;
            this._HTTP_SERVER.PrincipalFinder = null;

            if (disposing)
            {
                this._HTTP_SERVER.Dispose();
            }
        }
        // Private Methods (5) 

        private IPrincipal FindPrincipal(IIdentity id)
        {
            if (id == null)
            {
                return null;
            }

            // TODO
            return null;
        }

        private void HandleHttpModule(IHttpModule module, HttpRequestEventArgs e)
        {
            this.HandleHttpModule(module,
                                  e.Request, e.Response);
        }

        private void HandleHttpModule(IHttpModule module, IHttpRequest req, IHttpResponse resp)
        {
            var handleCtx = new SimpleHttpRequestContext();
            handleCtx.Request = req;
            handleCtx.Response = resp;

            var result = module.HandleRequest(handleCtx);
            if (result.Errors.Count > 0)
            {
                throw new AggregateException(result.Errors);
            }
        }

        private void HandleRequest(object sender, HttpRequestEventArgs e)
        {
            const string LOG_TAG_PREFIX = "ApplicationServer::WebInterface_HandleRequest::";

            try
            {
                var found = false;

                string addr = null;
                if (e.Request.Address != null)
                {
                    addr = e.Request.Address.AbsolutePath;
                }

                if (string.IsNullOrWhiteSpace(addr) ||
                    addr.ToLower().Trim() == "/")
                {
                    var defaultModule = ServiceLocator.Current
                                                      .GetAllInstances<IHttpModule>()
                                                      .SingleOrDefault(m => m.GetType()
                                                                             .GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.DefaultHttpModuleAttribute), false)
                                                                             .Any());

                    if (defaultModule != null)
                    {
                        found = true;
                        this.HandleHttpModule(defaultModule, e);
                    }
                }

                if (!found)
                {
                    e.Response.DocumentNotFound = true;
                }
            }
            catch (Exception ex)
            {
                this.Logger
                    .Log(msg: ex.GetBaseException() ?? ex,
                         tag: LOG_TAG_PREFIX + "HandleRequest",
                         categories: LoggerFacadeCategories.Errors);
            }
        }

        private bool ValidateRequest(IHttpRequest request)
        {
            try
            {
                if (request == null)
                {
                    return false;
                }

                //TODO
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Methods
    }
}
