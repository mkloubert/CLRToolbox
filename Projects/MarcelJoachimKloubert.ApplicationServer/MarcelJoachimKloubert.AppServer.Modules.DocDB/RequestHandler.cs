// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using MarcelJoachimKloubert.ApplicationServer.DataLayer;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using DocDBEntities = MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService;
using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities;

namespace MarcelJoachimKloubert.AppServer.Modules.DocDB
{
    internal sealed class RequestHandler : DisposableBase
    {
        #region Fields (5)

        private const string _MIME_JSON = "application/json";
        private readonly Regex _REGEX_CONTENT_TYPE = new Regex(@"^(\s*)(\S+)(\s*)(;\s*charset\s*=\s*\S+)?(\s*)$",
                                                               RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private readonly ISerializer _SERIALIZER = ServiceLocator.Current.GetInstance<ISerializer>();
        private readonly IHttpServer _SERVER;
        private DocDBEntities.DOCDB_Users[] _users;

        #endregion Fields

        #region Constructors (1)

        internal RequestHandler(IHttpServer server)
        {
            this._SERVER = server;

            server.CredentialValidator = this.Server_ValidateCredential;
            server.PrincipalFinder = this.Server_FindPrincipal;
            server.HandleDocumentNotFound += this.Server_HandleDocumentNotFound;
            server.HandleError += this.Server_HandleError;
            server.HandleRequest += this.Server_HandleRequest;
        }

        #endregion Constructors

        #region Methods (11)

        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            this._SERVER.CredentialValidator = (u, p) => false;
            this._SERVER.PrincipalFinder = (i) => null;
            this._SERVER.HandleRequest -= this.Server_HandleRequest;
            this._SERVER.HandleDocumentNotFound -= this.Server_HandleDocumentNotFound;
            this._SERVER.HandleError -= this.Server_HandleError;

            if (disposing)
            {
                this._SERVER.Dispose();
            }
        }
        // Private Methods (9) 

        private static bool HasUrlRightFormat(string part)
        {
            if (part == string.Empty)
            {
                return false;
            }

            return part.All(c => (c >= 'a' && c <= 'z') ||
                                 (c == '_') ||
                                 char.IsDigit(c));
        }

        private static bool IsJsonMimeType(string contentType)
        {
            return IsMimeType(_MIME_JSON,
                              contentType);
        }

        private static bool IsMimeType(string mimeType, string contentType)
        {
            mimeType = (mimeType ?? string.Empty).ToLower().Trim();
            contentType = (contentType ?? string.Empty).ToLower().Trim();

            return contentType == mimeType ||
                   contentType.StartsWith(mimeType + " ");
        }

        private void ReloadUsers()
        {
            var loadedUsers = new HashSet<DocDBEntities.DOCDB_Users>();

            using (var db = ServiceLocator.Current.GetInstance<IAppServerDatabase>())
            {
                foreach (var srvUser in db.Query<ServerEntities.General.Security.Users>()
                                          .Where(u => u.IsActive)
                                          .ToArray())
                {
                    var user = db.Query<DocDBEntities.DOCDB_Users>()
                                 .Where(u => u.IsActive &&
                                             u.UserID == srvUser.UserID)
                                 .Single();
                    user.Users = srvUser;

                    loadedUsers.Add(user);
                }
            }

            this._users = loadedUsers.ToArray();
        }

        private IPrincipal Server_FindPrincipal(IIdentity id)
        {
            return null;
        }

        private void Server_HandleDocumentNotFound(object sender, HttpRequestEventArgs e)
        {

        }

        private void Server_HandleError(object sender, HttpRequestErrorEventArgs e)
        {

        }

        private void Server_HandleRequest(object sender, HttpRequestEventArgs e)
        {
            var urlParts = e.Request.Address.AbsolutePath
                                            .Split('/')
                                            .SkipWhile(str => string.IsNullOrWhiteSpace(str))
                                            .Select(str => str.ToLower().Trim())
                                            .ToArray();

            if (urlParts.Length == 0 ||
                !urlParts.All(p => HasUrlRightFormat(p)))
            {
                e.Response.StatusCode = HttpStatusCode.BadRequest;
                e.Response.StatusDescription = "Invalid URL!";

                return;
            }

            var varNamespace = string.Join(".", urlParts.Take(urlParts.Length - 1));
            var varName = urlParts.Last();

            var contentType = e.Request.ContentType;
            var enc = Encoding.UTF8;  //TODO read from content-type

            if (IsJsonMimeType(contentType) ||
                string.IsNullOrWhiteSpace(contentType))
            {
                IDictionary<string, object> obj = null;
                try
                {
                    var body = e.Request.GetBodyData();
                    var json = enc.GetString(body);

                    try
                    {
                        obj = this._SERIALIZER
                                  .FromJson<IDictionary<string, object>>(json);
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException(ex.Message,
                                                  ex);
                    }

                    //TODO
                }
                catch (FormatException)
                {
                    e.Response.StatusCode = HttpStatusCode.BadRequest;
                    e.Response.StatusDescription = "Invalid JSON data!";
                }
            }
            else
            {
                e.Response.StatusCode = HttpStatusCode.BadRequest;
                e.Response.StatusDescription = "Invalid content type!";
            }
        }

        private bool Server_ValidateCredential(string username, string password)
        {
            bool result;

            try
            {
                //TODO
                result = (username ?? string.Empty).ToLower().Trim() == "the_user" &&
                         (password == "the_password");
            }
            catch
            {
                result = false;
            }

            return result;
        }
        // Internal Methods (1) 

        internal void Start()
        {
            this.ReloadUsers();

            this._SERVER.Start();
        }

        #endregion Methods
    }
}
