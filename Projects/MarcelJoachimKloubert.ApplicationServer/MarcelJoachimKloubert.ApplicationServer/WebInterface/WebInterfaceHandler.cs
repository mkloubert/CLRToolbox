// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using MarcelJoachimKloubert.ApplicationServer.Extensions;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    internal sealed partial class WebInterfaceHandler : DisposableBase
    {
        #region Fields (3)

        private readonly ApplicationServer _APP_SERVER;
        private readonly IHttpServer _HTTP_SERVER;
        private static Assembly _RESOURCE_ASSEMBLY;

        #endregion Fields

        #region Constructors (2)

        internal WebInterfaceHandler(ApplicationServer appServer, IHttpServer httpServer)
        {
            this._APP_SERVER = appServer;
            this._HTTP_SERVER = httpServer;

            this.InitUrlHandlers();

            this._HTTP_SERVER.RequestValidator = this.ValidateRequest;
            this._HTTP_SERVER.PrincipalFinder = this.FindPrincipal;

            this._HTTP_SERVER.HandleRequest += this.HandleRequest;
            this._HTTP_SERVER.HandleError += this.HandleError;
        }

        static WebInterfaceHandler()
        {
            _RESOURCE_ASSEMBLY = Assembly.GetExecutingAssembly();
        }

        #endregion Constructors

        #region Properties (1)

        internal ILoggerFacade Logger
        {
            get { return this._APP_SERVER.Logger; }
        }

        #endregion Properties

        #region Methods (13)

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
        // Private Methods (12) 

        private IPrincipal FindPrincipal(IIdentity id)
        {
            if (id == null)
            {
                return null;
            }

            // TODO
            return null;
        }

        private static string GetMimeTypeByFileExtension(string ext)
        {
            Encoding charset;
            return GetMimeTypeByFileExtension(ext, out charset);
        }

        private static string GetMimeTypeByFileExtension(string ext, out Encoding charset)
        {
            charset = null;

            var result = MediaTypeNames.Application.Octet;
            switch ((ext ?? string.Empty).ToUpper().Trim())
            {
                case ".CSS":
                    result = "text/css";
                    charset = Encoding.UTF8;
                    break;

                case ".GIF":
                    result = MediaTypeNames.Image.Gif;
                    break;

                case ".ICO":
                case ".ICON":
                    result = "image/x-icon";
                    break;

                case ".JPEG":
                case ".JPG":
                    result = MediaTypeNames.Image.Jpeg;
                    break;

                case ".JS":
                    result = "text/javascript";
                    charset = Encoding.UTF8;
                    break;

                case ".PNG":
                    result = "image/png";
                    break;

                case ".SVG":
                    result = "image/svg+xml";
                    break;

                case ".TXT":
                    result = "text/plain";
                    charset = Encoding.UTF8;
                    break;
            }

            return result;
        }

        private void HandleError(object sender, HttpRequestErrorEventArgs e)
        {

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
            var found = false;

            string addr = null;
            if (e.Request.Address != null)
            {
                addr = e.Request.Address.AbsolutePath;
            }

            if (string.IsNullOrWhiteSpace(addr) ||
                addr.ToLower().Trim() == "/")
            {
                var defaultModule = TryGetDefaultModule(ServiceLocator.Current);

                if (defaultModule != null)
                {
                    found = true;
                    this.HandleHttpModule(defaultModule, e);
                }
            }
            else
            {
                foreach (var h in this._URL_HANDLERS)
                {
                    h.Handle(e, ref found);
                    if (found)
                    {
                        break;
                    }
                }
            }

            if (!found)
            {
                e.Response.DocumentNotFound = true;
            }
        }

        private IAppServerModuleContext TryFindAppModuleContextByHash(string modHash)
        {
            modHash = (modHash ?? string.Empty).ToLower().Trim();

            return this._APP_SERVER
                       .Modules
                       .Select(m => m.Context)
                       .OfType<IAppServerModuleContext>()
                       .Select(mc => new
                        {
                            Assembly = mc.Object.GetType().Assembly,
                            Context = mc,
                            Hash = mc.GetWebHashAsHexString(),
                        }).Where(x => x.Hash == modHash)
                          .Select(x => x.Context)
                          .SingleOrDefault();
        }

        private static IHttpModule TryGetDefaultModule(IServiceLocator serviceLocator)
        {
            var modules = serviceLocator.GetAllInstances<IHttpModule>();

            return modules.SingleOrDefault(m => m.GetType()
                                                 .GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.DefaultHttpModuleAttribute), false)
                                                 .Any());
        }

        private static IHttpModule TryGetModuleByName(IServiceLocator serviceLocator, string modName)
        {
            var modules = serviceLocator.GetAllInstances<IHttpModule>();
            modName = (modName ?? string.Empty).ToUpper().Trim();

            return modules.SingleOrDefault(m => modName == (m.Name ?? string.Empty).ToUpper().Trim());
        }

        private static Stream TryGetWebResourceStream(IEnumerable<char> resName)
        {
            var fullResName = string.Format("MarcelJoachimKloubert.ApplicationServer.Resources.Web.{0}",
                                            resName.AsString()).ToLower()
                                                               .Trim();

            var matchingRes = CollectionHelper.SingleOrDefault(_RESOURCE_ASSEMBLY.GetManifestResourceNames(),
                                                               n => fullResName == (n ?? string.Empty).ToLower().Trim());

            return matchingRes != null ? _RESOURCE_ASSEMBLY.GetManifestResourceStream(matchingRes) : null;
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
