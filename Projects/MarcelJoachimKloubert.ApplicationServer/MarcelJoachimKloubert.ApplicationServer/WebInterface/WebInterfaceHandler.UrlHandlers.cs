// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Text.RegularExpressions;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    partial class WebInterfaceHandler
    {
        #region Fields (1)

        private UriHandler[] _URL_HANDLERS;

        #endregion Fields

        #region Methods (5)

        // Private Methods (5) 

        private void HandleUrl_AppServerModule(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var moduleHash = (match.Groups[2].Value ?? string.Empty).ToLower().Trim();

            var moduleCtx = this.TryFindAppModuleContextByHash(moduleHash);
            if (moduleCtx != null)
            {
                var module = TryGetDefaultModule(moduleCtx.GetAllInstances<IHttpModule>(), match.Groups[4].Value);
                if (module != null)
                {
                    found = true;
                    this.HandleHttpModule(module, e);
                }
            }
        }

        private void HandleUrl_AppServerModuleFile(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var moduleHash = (match.Groups[2].Value ?? string.Empty).ToLower().Trim();

            var moduleCtx = this.TryFindAppModuleContextByHash(moduleHash);
            if (moduleCtx != null)
            {
                var fileName = (match.Groups[4].Value ?? string.Empty).Trim();
                var fileExt = (match.Groups[6].Value ?? string.Empty).Trim();

                using (var stream = moduleCtx.TryGetResourceStream(string.Format("Web.{0}{1}{2}",
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

        private void HandleUrl_DefaultAppServerModule(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var moduleHash = (match.Groups[2].Value ?? string.Empty).ToLower().Trim();

            var moduleCtx = this.TryFindAppModuleContextByHash(moduleHash);
            if (moduleCtx != null)
            {
                var defaultModule = TryGetDefaultModule(moduleCtx.GetAllInstances<IHttpModule>());
                if (defaultModule != null)
                {
                    found = true;
                    this.HandleHttpModule(defaultModule, e);
                }
            }
        }

        private void HandleUrl_Module(Match match, HttpRequestEventArgs e, ref bool found)
        {
            var module = TryGetDefaultModule(ServiceLocator.Current
                                                           .GetAllInstances<IHttpModule>(), match.Groups[2].Value);

            if (module != null)
            {
                found = true;
                this.HandleHttpModule(module, e);
            }
        }

        private void InitUrlHandlers()
        {
            var handlers = new List<UriHandler>();

            handlers.Add(new UriHandler(_REGEX_MODULES,
                                        this.HandleUrl_Module));
            handlers.Add(new UriHandler(_REGEX_DEFAULT_SERVER_MODULE,
                                        this.HandleUrl_DefaultAppServerModule));
            handlers.Add(new UriHandler(_REGEX_SERVER_MODULES,
                                        this.HandleUrl_AppServerModule));
            handlers.Add(new UriHandler(_REGEX_FILES_SERVER_MODULE,
                                        this.HandleUrl_AppServerModuleFile));

            this._URL_HANDLERS = handlers.ToArray();
        }

        #endregion Methods
    }
}
