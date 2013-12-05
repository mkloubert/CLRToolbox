// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    partial class WebInterfaceHandler
    {
        #region Fields (1)

        private UriHandler[] _URL_HANDLERS;

        #endregion Fields

        #region Methods (6)

        // Private Methods (6) 

        private void HandleUrl_AppServerModule(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var moduleHash = match.Groups[2].Value;

            var moduleCtx = this.TryFindAppModuleContextByHash(moduleHash);
            if (moduleCtx != null)
            {
                var module = TryGetModuleByName(moduleCtx, match.Groups[4].Value);
                if (module != null)
                {
                    found = true;
                    this.HandleHttpModule(module, e);
                }
            }
        }

        private void HandleUrl_AppServerModuleFile(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var moduleHash = match.Groups[2].Value;

            var moduleCtx = this.TryFindAppModuleContextByHash(moduleHash);
            if (moduleCtx != null)
            {
                var fileName = (match.Groups[4].Value ?? string.Empty).Trim();
                fileName = fileName.Replace("/", ".");

                var fileExt = Path.GetExtension(fileName).Trim();

                using (var stream = moduleCtx.TryGetResourceStream("Web." + fileName))
                {
                    if (stream != null)
                    {
                        found = true;

                        stream.CopyTo(e.Response.Stream);

                        Encoding charset;
                        e.Response.ContentType = GetMimeTypeByFileExtension(fileExt,
                                                                            out charset);
                        e.Response.Charset = charset;
                    }
                }
            }
        }

        private void HandleUrl_DefaultAppServerModule(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var moduleHash = match.Groups[2].Value;

            var moduleCtx = this.TryFindAppModuleContextByHash(moduleHash);
            if (moduleCtx != null)
            {
                var defaultModule = TryGetDefaultModule(moduleCtx);
                if (defaultModule != null)
                {
                    found = true;
                    this.HandleHttpModule(defaultModule, e);
                }
            }
        }

        private void HandleUrl_File(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var fileName = (match.Groups[2].Value ?? string.Empty).Trim();
            fileName = fileName.Replace("/", ".");

            var fileExt = Path.GetExtension(fileName).Trim();

            using (var stream = TryGetWebResourceStream(fileName))
            {
                if (stream != null)
                {
                    found = true;

                    stream.CopyTo(e.Response.Stream);

                    Encoding charset;
                    e.Response.ContentType = GetMimeTypeByFileExtension(Path.GetExtension(fileExt),
                                                                        out charset);
                    e.Response.Charset = charset;
                }
            }
        }

        private void HandleUrl_Module(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var module = TryGetModuleByName(ServiceLocator.Current, match.Groups[2].Value);

            if (module != null)
            {
                found = true;
                this.HandleHttpModule(module, e);
            }
        }

        private void InitUrlHandlers()
        {
            const string REGEX_FILENAME = @"[A-Za-z0-9|\/|_|\-|,|\.]+";
            const string REGEX_MODULE_EXT = "html";
            const string REGEX_SERVER_MODULE = "^(/)([A-Fa-f0-9]{2,})(/)";

            var handlers = new List<UriHandler>();

            // embedded files of app server
            handlers.Add(new UriHandler(new Regex(@"^(/)(" + REGEX_FILENAME + @")(\.)?(.*?)$",
                                                  RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline),
                                        this.HandleUrl_File));

            // modules of app server
            handlers.Add(new UriHandler(new Regex(@"^(/)(" + REGEX_FILENAME + @")(\." + REGEX_MODULE_EXT + ")",
                                                  RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline),
                                        this.HandleUrl_Module));

            // default http module of app server module
            handlers.Add(new UriHandler(new Regex(REGEX_SERVER_MODULE + "?$",
                                                  RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline),
                                        this.HandleUrl_DefaultAppServerModule));
            // specific http module of app server module
            handlers.Add(new UriHandler(new Regex(REGEX_SERVER_MODULE + @"(" + REGEX_FILENAME + @")(\." + REGEX_MODULE_EXT + ")",
                                                  RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline),
                                        this.HandleUrl_AppServerModule));

            // file inside app server module
            handlers.Add(new UriHandler(new Regex(REGEX_SERVER_MODULE + @"(" + REGEX_FILENAME + @")(\.)?(.*?)$",
                                                  RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline),
                                        this.HandleUrl_AppServerModuleFile));

            this._URL_HANDLERS = handlers.ToArray();
        }

        #endregion Methods
    }
}
