// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    internal sealed class WebInterfaceHandler : DisposableBase
    {
        #region Fields (9)

        private readonly ApplicationServer _APP_SERVER;
        private readonly IHttpServer _HTTP_SERVER;
        private static readonly Regex _REGEX_CSS;
        private static readonly Regex _REGEX_DEFAULT_SERVER_MODULE;
        private static readonly Regex _REGEX_FILES_SERVER_MODULE;
        private static readonly Regex _REGEX_IMAGES;
        private static readonly Regex _REGEX_JAVASCRIPT;
        private static readonly Regex _REGEX_MODULES;
        private static readonly Regex _REGEX_SERVER_MODULES;

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
            const string REGEX_MODULE_EXT = "html";
            const string REGEX_FILENAME = @"[A-Za-z0-9|_|\-|,|\.]+";
            const string REGEX_SERVER_MODULE = @"^(/)([A-Za-z0-9]{2,})(/)";

            _REGEX_CSS = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.)(css)$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            _REGEX_IMAGES = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.)(gif|ico|icon|jpeg|jpg|png|svg)$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            _REGEX_JAVASCRIPT = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.)(js)$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            _REGEX_MODULES = new Regex(@"^(/)(" + REGEX_FILENAME + @")(\." + REGEX_MODULE_EXT + ")", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

            _REGEX_DEFAULT_SERVER_MODULE = new Regex(REGEX_SERVER_MODULE + "?$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            _REGEX_SERVER_MODULES = new Regex(REGEX_SERVER_MODULE + @"(" + REGEX_FILENAME + @")(\." + REGEX_MODULE_EXT + ")", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

            _REGEX_FILES_SERVER_MODULE = new Regex(REGEX_SERVER_MODULE + @"(" + REGEX_FILENAME + @")(\.)(.*?)$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        #endregion Constructors

        #region Properties (1)

        internal ILoggerFacade Logger
        {
            get { return this._APP_SERVER.Logger; }
        }

        #endregion Properties

        #region Methods (10)

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
        // Private Methods (9) 

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
            var result = MediaTypeNames.Application.Octet;
            switch ((ext ?? string.Empty).ToUpper().Trim())
            {
                case "GIF":
                    result = MediaTypeNames.Image.Gif;
                    break;

                case "ICO":
                case "ICON":
                    result = "image/x-icon";
                    break;

                case "JPEG":
                case "JPG":
                    result = MediaTypeNames.Image.Jpeg;
                    break;

                case "PNG":
                    result = "image/png";
                    break;

                case "SVG":
                    result = "image/svg+xml";
                    break;
            }

            return result;
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
                var defaultModule = TryGetDefaultModule(ServiceLocator.Current
                                                                      .GetAllInstances<IHttpModule>());

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

                    var module = TryGetDefaultModule(ServiceLocator.Current
                                                                   .GetAllInstances<IHttpModule>(), match.Groups[2].Value);
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

                            e.Response.ContentType = GetMimeTypeByFileExtension(imgFileExt);
                        }
                    }
                }
                else
                {
                    // inside app server module

                    if ((match = _REGEX_DEFAULT_SERVER_MODULE.Match(addr)).Success)
                    {
                        // default HTTP module inside app server module

                        var modHash = (match.Groups[2].Value ?? string.Empty).ToLower().Trim();

                        var modCtx = this.TryFindAppModuleContextByHash(modHash);
                        if (modCtx != null)
                        {
                            var defaultModule = TryGetDefaultModule(modCtx.GetAllInstances<IHttpModule>());
                            if (defaultModule != null)
                            {
                                found = true;
                                this.HandleHttpModule(defaultModule, e);
                            }
                        }
                    }
                    else if ((match = _REGEX_SERVER_MODULES.Match(addr)).Success)
                    {
                        // specific HTTP module 

                        var modHash = (match.Groups[2].Value ?? string.Empty).ToLower().Trim();

                        var modCtx = this.TryFindAppModuleContextByHash(modHash);
                        if (modCtx != null)
                        {
                            var module = TryGetDefaultModule(modCtx.GetAllInstances<IHttpModule>(), match.Groups[4].Value);
                            if (module != null)
                            {
                                found = true;
                                this.HandleHttpModule(module, e);
                            }
                        }
                    }
                    else if ((match = _REGEX_FILES_SERVER_MODULE.Match(addr)).Success)
                    {
                        // file in web resources of the assembly of the app module

                        var modHash = (match.Groups[2].Value ?? string.Empty).ToLower().Trim();

                        var modCtx = this.TryFindAppModuleContextByHash(modHash);
                        if (modCtx != null)
                        {
                            var fileName = (match.Groups[4].Value ?? string.Empty).Trim();
                            var fileExt = (match.Groups[6].Value ?? string.Empty).Trim();

                            using (var stream = modCtx.TryGetResourceStream(string.Format("Web.{0}{1}{2}",
                                                                                          fileName,
                                                                                          fileExt != string.Empty ? "." : string.Empty,
                                                                                          fileExt)))
                            {
                                if (stream != null)
                                {
                                    found = true;

                                    stream.CopyTo(e.Response.Stream);
                                    e.Response.ContentType = GetMimeTypeByFileExtension(fileExt);
                                }
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

        private IAppServerModuleContext TryFindAppModuleContextByHash(string modHash)
        {
            return this._APP_SERVER
                       .Modules
                       .Select(m => m.Context)
                       .OfType<IAppServerModuleContext>()
                       .Select(mc => new
                        {
                            Assembly = mc.Object.GetType().Assembly,
                            Context = mc,
                            Hash = mc.GetHashAsHexString(),
                        }).Where(x => x.Hash == modHash)
                          .Select(x => x.Context)
                          .SingleOrDefault();
        }

        private static IHttpModule TryGetDefaultModule(IEnumerable<IHttpModule> modules)
        {
            return modules.SingleOrDefault(m => m.GetType()
                                                 .GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.DefaultHttpModuleAttribute), false)
                                                 .Any());
        }

        private static IHttpModule TryGetDefaultModule(IEnumerable<IHttpModule> modules, string modName)
        {
            modName = (modName ?? string.Empty).ToUpper().Trim();

            return modules.SingleOrDefault(m => modName == (m.Name ?? string.Empty).ToUpper().Trim());
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
