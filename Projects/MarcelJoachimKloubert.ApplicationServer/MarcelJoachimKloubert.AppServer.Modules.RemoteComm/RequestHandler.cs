// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm
{
    internal sealed class RequestHandler : DisposableBase
    {
        #region Fields (3)

        private readonly RemoteCommModule _MODULE;
        private readonly ISerializer _SERIALIZER = ServiceLocator.Current.GetInstance<ISerializer>();
        private readonly IHttpServer _SERVER;

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

        #region Methods (6)

        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this._SERVER.Dispose();
            }
        }
        // Private Methods (4) 

        private bool IsFunctionAllowed(IFunction func, IPrincipal user)
        {
            //TODO read from database / ACL

            return func != null;
        }

        private IPrincipal Server_FindPrincipal(IIdentity id)
        {
            return null;
        }

        private void Server_HandleRequest(object sender, HttpRequestEventArgs e)
        {
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
                                            if (this.IsFunctionAllowed(func, e.Request.User))
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
            //TODO
            return true;
        }
        // Internal Methods (1) 

        internal void Start()
        {
            this._SERVER.Start();
        }

        #endregion Methods
    }
}
