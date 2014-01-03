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
using MarcelJoachimKloubert.ApplicationServer.DataLayer.Entities;
using MarcelJoachimKloubert.ApplicationServer.Extensions;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.ApplicationServer.Security.Cryptography;
using MarcelJoachimKloubert.ApplicationServer.Services.Templates.Text.Html;
using MarcelJoachimKloubert.ApplicationServer.WebInterface.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.CLRToolbox.Templates.Text.Html;
using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    internal sealed partial class WebInterfaceHandler : DisposableBase
    {
        #region Fields (5)

        private readonly ApplicationServer _APP_SERVER;
        private readonly IHttpServer _HTTP_SERVER;
        private readonly IPasswordHasher _PWD_HASHER = ServiceLocator.Current.GetInstance<IPasswordHasher>();
        private static Assembly _RESOURCE_ASSEMBLY;
        private ServerEntities.WebInterface.WebInterfaceUsers[] _users;

        #endregion Fields

        #region Constructors (2)

        internal WebInterfaceHandler(ApplicationServer appServer, IHttpServer httpServer)
        {
            this._APP_SERVER = appServer;
            this._HTTP_SERVER = httpServer;

            this.InitUrlHandlers();

            this._HTTP_SERVER.CredentialValidator = this.Server_ValidateCredential;
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

        #region Methods (19)

        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            this._HTTP_SERVER.RequestValidator = (req) => false;
            this._HTTP_SERVER.CredentialValidator = (u, p) => false;
            this._HTTP_SERVER.PrincipalFinder = (i) => null;

            this._HTTP_SERVER.HandleRequest -= this.HandleRequest;

            if (disposing)
            {
                this._HTTP_SERVER.Dispose();
            }
        }
        // Private Methods (16) 

        private IPrincipal FindPrincipal(IIdentity id)
        {
            if (id != null)
            {
                var user = this.TryFindUserEntityByName(id.Name);
                if (user != null)
                {
                    //TODO set ACL
                    return new WebUserPrincipal(user, id);
                }
            }

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
            var frontend = new FrontendHandler(req, resp);

            var handleCtx = new SimpleHttpRequestContext();
            handleCtx.Request = req;
            handleCtx.Response = resp;

            var result = module.HandleRequest(handleCtx);
            if (result.Errors.Count > 0)
            {
                throw new AggregateException(result.Errors);
            }

            var directOutput = resp.DirectOutput;

            if (!directOutput)
            {
                var header = ServiceLocator.Current.GetInstance<IHtmlTemplate>("__header");
                frontend.WriteVars(header);

                resp.Prefix(header.Render());
            }

            if (!directOutput)
            {
                var footer = ServiceLocator.Current.GetInstance<IHtmlTemplate>("__footer");
                frontend.WriteVars(footer);

                resp.Append(footer.Render());
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

        private void ReloadUsers()
        {
            var loadedUsers = new HashSet<ServerEntities.WebInterface.WebInterfaceUsers>();

            using (var repo = ServiceLocator.Current.GetInstance<IAppServerEntityRepository>())
            {
                foreach (var user in repo.LoadAll<ServerEntities.WebInterface.WebInterfaceUsers>()
                                         .Where(u => u.IsActive)
                                         .ToArray())
                {
                    var srvUser = repo.LoadAll<ServerEntities.Security.Users>()
                                      .Where(su => su.IsActive &&
                                                   su.UserID == user.UserID)
                                      .Single();

                    user.Users = srvUser;
                    srvUser.CharacterPasswordHasher = this.Users_HashPassword;

                    loadedUsers.Add(user);
                }
            }

            this._users = loadedUsers.ToArray();
        }

        private bool Server_ValidateCredential(string username, string password)
        {
            try
            {
                var user = this.TryFindUserEntityByName(username);
                if (user != null)
                {
                    return user.Users
                               .CheckPassword(password);
                }
            }
            catch
            {
                // ignore errors here
            }

            return false;
        }

        private IAppServerModuleContext TryFindAppModuleContextByHash(string modHash)
        {
            modHash = (modHash ?? string.Empty).ToLower().Trim();

            return CollectionHelper.SingleOrDefault(this._APP_SERVER
                                                        .Modules
                                                        .Select(m => m.Context),
                                                    mc => mc != null &&
                                                          mc.GetWebHashAsHexString() == modHash);
        }

        private ServerEntities.WebInterface.WebInterfaceUsers TryFindUserEntityByName(string username)
        {
            return CollectionHelper.SingleOrDefault((this._users ?? Enumerable.Empty<ServerEntities.WebInterface.WebInterfaceUsers>()).OfType<ServerEntities.WebInterface.WebInterfaceUsers>(),
                                                    u => (u.Users.Name ?? string.Empty).ToLower().Trim() == (username ?? string.Empty).ToLower().Trim());
        }

        private static IHttpModule TryGetDefaultModule(IServiceLocator serviceLocator)
        {
            return CollectionHelper.SingleOrDefault(serviceLocator.GetAllInstances<IHttpModule>(),
                                                    m => m.GetType()
                                                          .GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.DefaultHttpModuleAttribute), false)
                                                          .Any());
        }

        private static IHttpModule TryGetModuleByName(IServiceLocator serviceLocator, string modName)
        {
            modName = (modName ?? string.Empty).ToUpper().Trim();

            return CollectionHelper.SingleOrDefault(serviceLocator.GetAllInstances<IHttpModule>(),
                                                    m => modName == (m.Name ?? string.Empty).ToUpper().Trim());
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

        private IEnumerable<byte> Users_HashPassword(string pwd)
        {
            if (string.IsNullOrEmpty(pwd))
            {
                return null;
            }

            return this._PWD_HASHER.Hash(pwd);
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
        // Internal Methods (2) 

        internal static IHtmlTemplate GetHtmlTemplate(IServiceLocator baseLocator, object key)
        {
            var throwActivationException = new Action(() =>
                {
                    throw new ServiceActivationException(typeof(global::MarcelJoachimKloubert.CLRToolbox.Templates.Text.Html.IHtmlTemplate),
                                                         key);
                });

            var tplName = (StringHelper.AsString(key, true) ?? string.Empty).Trim();
            if (tplName == string.Empty)
            {
                throwActivationException();
            }

            using (var stream = TryGetWebResourceStream("html." + tplName + ".html"))
            {
                if (stream == null)
                {
                    throwActivationException();
                }

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return new DotLiquidHtmlTemplate(reader.ReadToEnd());
                }
            }
        }

        internal void Start()
        {
            this.ReloadUsers();

            this._HTTP_SERVER.Start();
        }

        #endregion Methods
    }
}
