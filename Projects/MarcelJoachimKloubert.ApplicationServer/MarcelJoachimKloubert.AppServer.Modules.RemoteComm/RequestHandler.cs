// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using MarcelJoachimKloubert.ApplicationServer.DataLayer.Entities;
using MarcelJoachimKloubert.ApplicationServer.Security.Cryptography;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using RemoteCommEntities = MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService;
using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm
{
    internal sealed class RequestHandler : DisposableBase
    {
        #region Fields (5)

        private readonly RemoteCommModule _MODULE;
        private readonly IPasswordHasher _PWD_HASHER = ServiceLocator.Current.GetInstance<IPasswordHasher>();
        private readonly ISerializer _SERIALIZER = ServiceLocator.Current.GetInstance<ISerializer>();
        private readonly IHttpServer _SERVER;
        private RemoteCommEntities.REMCOMM_Users[] _users;

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

        #region Methods (9)

        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this._SERVER.Dispose();
            }
        }
        // Private Methods (7) 

        private bool IsFunctionAllowed(IFunction func, RemoteCommUserPrincipal user)
        {
            return user != null &&
                   user.User.CanExecuteFunction(func);
        }

        private void ReloadUsers()
        {
            var loadedUsers = new HashSet<RemoteCommEntities.REMCOMM_Users>();

            using (var repo = ServiceLocator.Current.GetInstance<IAppServerEntityRepository>())
            {
                foreach (var user in repo.LoadAll<RemoteCommEntities.REMCOMM_Users>()
                                         .Where(u => u.IsActive)
                                         .ToArray())
                {
                    var srvUser = repo.LoadAll<ServerEntities.Security.Users>()
                                    .Where(su => su.IsActive &&
                                                 su.UserID == user.UserID)
                                    .Single();

                    user.Users = srvUser;
                    srvUser.CharacterPasswordHasher = this.Users_HashPassword;

                    user.REMCOMM_UserFunctions = repo.LoadAll<RemoteCommEntities.REMCOMM_UserFunctions>()
                                                     .Where(uf => uf.REMCOMM_UserID == user.REMCOMM_UserID &&
                                                                  uf.CanExecute)
                                                     .ToArray();

                    foreach (var uf in user.REMCOMM_UserFunctions)
                    {
                        uf.ServerFunctions = repo.LoadAll<ServerEntities.Functions.ServerFunctions>()
                                                 .Where(sf => sf.ServerFunctionID == uf.ServerFunctionID)
                                                 .Single();
                    }

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
                    return new RemoteCommUserPrincipal(user, id);
                }
            }

            return null;
        }

        private void Server_HandleRequest(object sender, HttpRequestEventArgs e)
        {
            var user = (RemoteCommUserPrincipal)e.Request.User;

            e.Response.Charset = Encoding.UTF8;
            var statusCode = HttpStatusCode.NotImplemented;
            IEnumerable<char> statusDescription = null;

            var urlParts = e.Request
                            .Address
                            .AbsolutePath
                            .Split('/')
                            .Select(p => p.ToLower().Trim())
                            .ToArray();

            if ((urlParts.Length == 3) ||
                (urlParts.Length == 4 && urlParts[3] == string.Empty))
            {
                switch (urlParts[1])    // action
                {
                    case "exec":
                        switch (e.Request.TryGetKnownMethod())
                        {
                            case HttpMethod.POST:
                                {
                                    // create dictionary from UTF-8 JSON string in body
                                    var @params = this._SERIALIZER
                                                      .FromJson<IDictionary<string, object>>(Encoding.UTF8
                                                                                                     .GetString(e.Request.GetBodyData()));

                                    var funcNameOrId = urlParts[2];
                                    if (!string.IsNullOrWhiteSpace(funcNameOrId))
                                    {
                                        var locator = ServiceLocator.Current.GetInstance<IFunctionLocator>();
                                        IFunction func;

                                        Guid id;
                                        if (Guid.TryParse(funcNameOrId, out id))
                                        {
                                            // try find by ID
                                            func = CollectionHelper.SingleOrDefault(locator.GetAllFunctions(),
                                                                                    f => f.Id == id);
                                        }
                                        else
                                        {
                                            // try find by full name
                                            func = CollectionHelper.SingleOrDefault(locator.GetAllFunctions(),
                                                                                    f => funcNameOrId == (f.FullName ?? string.Empty).ToLower().Trim());
                                        }

                                        if (func != null)
                                        {
                                            if (this.IsFunctionAllowed(func, user))
                                            {
                                                IFunctionExecutionContext execCtx;
                                                if (@params != null)
                                                {
                                                    execCtx = func.Execute(parameters: @params,
                                                                          autoStart: true);
                                                }
                                                else
                                                {
                                                    execCtx = func.Execute(autoStart: true);
                                                }

                                                if (execCtx.Errors != null &&
                                                    execCtx.Errors.Count > 0)
                                                {
                                                    // raise 500 error
                                                    throw new AggregateException(execCtx.Errors);
                                                }

                                                var result = new
                                                    {
                                                        code = execCtx.ResultCode,
                                                        msg = string.IsNullOrWhiteSpace(execCtx.ResultMessage) ? null : execCtx.ResultMessage.Trim(),
                                                        @params = execCtx.ResultParameters,
                                                    };

                                                e.Response.ContentType = "application/json";
                                                e.Response.Write(chars: this._SERIALIZER
                                                                            .ToJson(result));

                                                statusCode = HttpStatusCode.OK;
                                            }
                                            else
                                            {
                                                e.Response.IsForbidden = true;
                                            }
                                        }
                                        else
                                        {
                                            e.Response.DocumentNotFound = true;
                                        }
                                    }
                                    else
                                    {
                                        statusCode = HttpStatusCode.BadRequest;
                                        statusDescription = "Invalid function name or ID";
                                    }
                                }
                                break;

                            default:
                                // invalid HTTP method
                                statusCode = HttpStatusCode.MethodNotAllowed;
                                break;
                        }
                        break;

                    default:
                        // unknown action
                        statusCode = HttpStatusCode.BadRequest;
                        statusDescription = "Unknown action";
                        break;
                }
            }
            else
            {
                statusCode = HttpStatusCode.BadRequest;
                statusDescription = "Invalid request URI";
            }

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

        private RemoteCommEntities.REMCOMM_Users TryFindUserEntityByName(string username)
        {
            return CollectionHelper.SingleOrDefault((this._users ?? Enumerable.Empty<RemoteCommEntities.REMCOMM_Users>())
                                                                              .OfType<RemoteCommEntities.REMCOMM_Users>(),
                                                    u => (u.Users.Name ?? string.Empty).ToLower().Trim() ==
                                                         (username ?? string.Empty).ToLower().Trim());
        }

        private IEnumerable<byte> Users_HashPassword(string pwd)
        {
            if (string.IsNullOrEmpty(pwd))
            {
                return null;
            }

            return this._PWD_HASHER.Hash(pwd);
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
