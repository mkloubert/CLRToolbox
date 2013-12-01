// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    internal sealed class WebInterfaceHandler : DisposableBase
    {
        #region Fields (6)

        private readonly ApplicationServer _APP_SERVER;
        private readonly IHttpServer _HTTP_SERVER;
        private static readonly Regex _REGEX_CSS;
        private static readonly Regex _REGEX_IMAGES;
        private static readonly Regex _REGEX_JAVASCRIPT;
        private static readonly Regex _REGEX_MODULES;

        #endregion Fields

        #region Constructors (2)

        internal WebInterfaceHandler(ApplicationServer appServer, IHttpServer httpServer)
        {
            this._APP_SERVER = appServer;
            this._HTTP_SERVER = httpServer;

            this._HTTP_SERVER.RequestValidator = this.ValidateRequest;
            this._HTTP_SERVER.PrincipalFinder = this.FindPrincipal;

            this._HTTP_SERVER.HandleRequest += this.HandleRequest;
        }

        static WebInterfaceHandler()
        {
            const string REGEX_FILENAME = @"[A-Za-z0-9|_|\-|,|\.]+";

            _REGEX_CSS = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.)(css)$", RegexOptions.Compiled);
            _REGEX_IMAGES = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.)(gif|ico|icon|jpeg|jpg|png)$", RegexOptions.Compiled);
            _REGEX_JAVASCRIPT = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.)(js)$", RegexOptions.Compiled);
            _REGEX_MODULES = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.tm)", RegexOptions.Compiled);
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
            else
            {
                Match match;

                if ((match = _REGEX_MODULES.Match(addr)).Success)
                {
                    // module in root path

                    var moduleName = (match.Groups[2].Value ?? string.Empty).ToUpper().Trim();

                    var module = ServiceLocator.Current
                                               .GetAllInstances<IHttpModule>()
                                               .SingleOrDefault(m => moduleName == (m.Name ?? string.Empty).ToUpper().Trim());

                    if (module != null)
                    {
                        found = true;
                        this.HandleHttpModule(module, e);
                    }
                }
                else if ((match = _REGEX_CSS.Match(addr)).Success)
                {
                    // CSS file in root directory

                    var cssDir = new DirectoryInfo(Path.Combine(this._APP_SERVER.WorkingDirectory, "web/css"));
                    if (cssDir.Exists)
                    {
                        var cssFileName = (match.Groups[2].Value ?? string.Empty).Trim();

                        var cssFile = new FileInfo(Path.Combine(cssDir.FullName, cssFileName + ".css"));
                        if (cssFile.Exists)
                        {
                            found = true;

                            using (var stream = cssFile.OpenRead())
                            {
                                stream.CopyTo(e.Response.Stream);

                                e.Response.ContentType = "text/css";
                                e.Response.Charset = Encoding.UTF8;
                            }
                        }
                    }
                }
                else if ((match = _REGEX_JAVASCRIPT.Match(addr)).Success)
                {
                    // JavaScript file in root directory

                    var jsDir = new DirectoryInfo(Path.Combine(this._APP_SERVER.WorkingDirectory, "web/js"));
                    if (jsDir.Exists)
                    {
                        var jsFileName = (match.Groups[2].Value ?? string.Empty).Trim();

                        var jsFile = new FileInfo(Path.Combine(jsDir.FullName, jsFileName + ".js"));
                        if (jsFile.Exists)
                        {
                            found = true;

                            using (var stream = jsFile.OpenRead())
                            {
                                stream.CopyTo(e.Response.Stream);

                                e.Response.ContentType = "text/javascript";
                                e.Response.Charset = Encoding.UTF8;
                            }
                        }
                    }
                }
                else if ((match = _REGEX_IMAGES.Match(addr)).Success)
                {
                    // image / icon in root path

                    var imageDir = new DirectoryInfo(Path.Combine(this._APP_SERVER.WorkingDirectory, "web/images"));
                    if (imageDir.Exists)
                    {
                        var imgFileName = (match.Groups[2].Value ?? string.Empty).Trim();
                        var imgFileExt = (match.Groups[4].Value ?? string.Empty).Trim();

                        var imageFile = new FileInfo(Path.Combine(imageDir.FullName, imgFileName + "." + imgFileExt));
                        if (imageFile.Exists)
                        {
                            found = true;

                            using (var stream = imageFile.OpenRead())
                            {
                                stream.CopyTo(e.Response.Stream);
                            }

                            e.Response.ContentType = MediaTypeNames.Application.Octet;
                            switch (imgFileExt.ToUpper())
                            {
                                case "GIF":
                                    e.Response.ContentType = MediaTypeNames.Image.Gif;
                                    break;

                                case "ICO":
                                case "ICON":
                                    e.Response.ContentType = "image/x-icon";
                                    break;

                                case "JPEG":
                                case "JPG":
                                    e.Response.ContentType = MediaTypeNames.Image.Jpeg;
                                    break;

                                case "PNG":
                                    e.Response.ContentType = "image/png";
                                    break;
                            }
                        }
                    }
                }
            }

            if (!found)
            {
                e.Response.DocumentNotFound = true;
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
