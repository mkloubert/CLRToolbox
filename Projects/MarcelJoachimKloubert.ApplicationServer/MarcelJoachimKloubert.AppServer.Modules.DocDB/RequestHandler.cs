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
using MarcelJoachimKloubert.ApplicationServer.DataLayer.Entities;
using MarcelJoachimKloubert.ApplicationServer.Security.Cryptography;
using MarcelJoachimKloubert.AppServer.Modules.DocDB.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using MarcelJoachimKloubert.CLRToolbox.Serialization.Json;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using DocDBEntities = MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService;
using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General;

namespace MarcelJoachimKloubert.AppServer.Modules.DocDB
{
    internal sealed class RequestHandler : DisposableBase
    {
        #region Fields (7)

        private const string _MIME_JSON = "application/json";
        private readonly DocDBModule _MODULE;
        private readonly IPasswordHasher _PWD_HASHER = ServiceLocator.Current.GetInstance<IPasswordHasher>();
        private readonly Regex _REGEX_CONTENT_TYPE = new Regex(@"^(\s*)(\S+)(\s*)(;\s*charset\s*=\s*\S+)?(\s*)$",
                                                               RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private readonly ISerializer _SERIALIZER = ServiceLocator.Current.GetInstance<ISerializer>();
        private readonly IHttpServer _SERVER;
        private DocDBEntities.DOCDB_Users[] _users;

        #endregion Fields

        #region Constructors (1)

        internal RequestHandler(DocDBModule module, IHttpServer server)
        {
            this._MODULE = module;
            this._SERVER = server;

            server.CredentialValidator = this.Server_ValidateCredential;
            server.PrincipalFinder = this.Server_FindPrincipal;
            server.HandleRequest += this.Server_HandleRequest;
        }

        #endregion Constructors

        #region Methods (14)

        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            this._SERVER.CredentialValidator = (u, p) => false;
            this._SERVER.PrincipalFinder = (i) => null;
            this._SERVER.HandleRequest -= this.Server_HandleRequest;

            if (disposing)
            {
                this._SERVER.Dispose();
            }
        }
        // Private Methods (12) 

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

            using (var repo = ServiceLocator.Current.GetInstance<IAppServerEntityRepository>())
            {
                foreach (var user in repo.LoadAll<DocDBEntities.DOCDB_Users>()
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

        private IPrincipal Server_FindPrincipal(IIdentity id)
        {
            if (id != null)
            {
                var user = this.TryFindUserEntityByName(id.Name);
                if (user != null)
                {
                    //TODO set ACL
                    return new DocDBUserPrincipal(user, id);
                }
            }

            return null;
        }

        private void Server_HandleRequest(object sender, HttpRequestEventArgs e)
        {
            var statusCode = HttpStatusCode.OK;
            IEnumerable<char> statusDescription = null;
            JsonParameterResult result = null;

            var user = (DocDBUserPrincipal)e.Request.User;

            string varNamespace;
            string varName;
            TryExtractVariableData(e.Request,
                                   out varNamespace, out varName);

            var contentType = e.Request.ContentType;
            var enc = Encoding.UTF8;  //TODO read from content-type

            switch (e.Request.Method)
            {
                case "DELETE":
                    // delete value
                    using (var db = ServiceLocator.Current.GetInstance<IAppServerDatabase>())
                    {
                        var existingData = TryFindUserData(db, user, varNamespace, varName);
                        if (existingData != null)
                        {
                            db.Remove(existingData);
                            db.Update();
                        }
                    }
                    break;

                case "GET":
                    // find value
                    {
                        using (var db = ServiceLocator.Current.GetInstance<IAppServerDatabase>())
                        {
                            var existingData = TryFindUserData(db, user, varNamespace, varName);
                            if (existingData != null)
                            {
                                IDictionary<string, object> jsonObj = null;
                                if (existingData.Data != null)
                                {
                                    jsonObj = this._SERIALIZER
                                                  .FromJson<IDictionary<string, object>>(Encoding.UTF8.GetString(existingData.Data));
                                }

                                result = new JsonParameterResult()
                                    {
                                        code = 0,
                                        tag = new Dictionary<string, object>()
                                            {
                                                { "created", existingData.CreationDate },
                                                { "data", new Dictionary<string, object>()
                                                              {
                                                                  { "content", jsonObj },
                                                                  { "mime", "application/json" },
                                                              }},
                                                { "lastUpdate", existingData.LastUpdate },
                                                { "name", string.IsNullOrWhiteSpace(existingData.Name) ? null : existingData.Name.ToLower().Trim() },
                                                { "namespace", string.IsNullOrWhiteSpace(existingData.Namespace) ? null : existingData.Namespace.ToLower().Trim() },
                                            },
                                    };
                            }
                            else
                            {
                                statusCode = HttpStatusCode.NotFound;
                            }
                        }
                    }
                    break;

                case "PUT":
                    // save value
                    {
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

                                using (var db = ServiceLocator.Current.GetInstance<IAppServerDatabase>())
                                {
                                    var existingData = TryFindUserData(db, user, varNamespace, varName);
                                    if (existingData == null)
                                    {
                                        existingData = new DocDBEntities.DOCDB_UserData()
                                        {
                                            CreationDate = this._MODULE.Context.Now,
                                            DOCDB_UserID = user.User.DOCDB_UserID,
                                        };

                                        db.Add(existingData);
                                    }
                                    else
                                    {
                                        db.Attach(existingData);

                                        existingData.LastUpdate = this._MODULE.Context.Now;
                                    }

                                    existingData.Data = string.IsNullOrWhiteSpace(json) ? null : Encoding.UTF8.GetBytes(json);
                                    //TODO use enum value
                                    existingData.MimeTypeID = 1;
                                    existingData.Name = varName;
                                    existingData.Namespace = varNamespace == string.Empty ? null : varNamespace.ToLower().Trim();

                                    db.Update();
                                }
                            }
                            catch (FormatException)
                            {
                                // invalid JSON string

                                statusCode = HttpStatusCode.BadRequest;
                            }
                        }
                        else
                        {
                            // invalid MIME type

                            statusCode = HttpStatusCode.UnsupportedMediaType;
                        }
                    }
                    break;

                default:
                    statusCode = HttpStatusCode.MethodNotAllowed;
                    break;
            }

            this.WriteJsonObject(e.Response, result);

            e.Response.StatusCode = statusCode;
            e.Response.StatusDescription = statusDescription.AsString();
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

        private static void TryExtractVariableData(IHttpRequest request,
                                                   out string varNamespace, out string varName)
        {
            var urlParts = request.Address.AbsolutePath
                                          .Split('/')
                                          .SkipWhile(str => string.IsNullOrWhiteSpace(str))
                                          .Select(str => str.ToLower().Trim())
                                          .ToArray();

            varNamespace = string.Join(".",
                                       urlParts.Take(urlParts.Length - 1)).ToLower().Trim();
            if (varNamespace == string.Empty)
            {
                varNamespace = null;
            }

            varName = (urlParts.LastOrDefault() ?? string.Empty).ToLower().Trim();
            if (varName == string.Empty)
            {
                varName = null;
            }
        }

        private static DocDBEntities.DOCDB_UserData TryFindUserData(IAppServerDatabase db,
                                                                    DocDBUserPrincipal user,
                                                                    string varNamespace, string varName)
        {
            return CollectionHelper.SingleOrDefault(db.Query<DocDBEntities.DOCDB_UserData>(),
                                                    ud => ud.DOCDB_UserID == user.User.DOCDB_UserID &&
                                                    varNamespace == (ud.Namespace ?? string.Empty).ToLower().Trim() &&
                                                    varName == ud.Name.ToLower().Trim());
        }

        private DocDBEntities.DOCDB_Users TryFindUserEntityByName(string username)
        {
            return CollectionHelper.SingleOrDefault((this._users ?? Enumerable.Empty<DocDBEntities.DOCDB_Users>()).OfType<DocDBEntities.DOCDB_Users>(),
                                                    u => (u.Users.Name ?? string.Empty).ToLower().Trim() == (username ?? string.Empty).ToLower().Trim());
        }

        private IEnumerable<byte> Users_HashPassword(string pwd)
        {
            if (string.IsNullOrEmpty(pwd))
            {
                return null;
            }

            return this._PWD_HASHER.Hash(pwd);
        }

        private void WriteJsonObject(IHttpResponse response, object obj)
        {
            var json = this._SERIALIZER.ToJson(obj);

            response.ContentType = "application/json";
            response.Charset = Encoding.UTF8;
            response.Write(json);
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
