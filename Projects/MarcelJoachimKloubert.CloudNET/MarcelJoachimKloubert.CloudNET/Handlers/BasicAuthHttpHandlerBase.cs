// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes._Impl.Web;
using MarcelJoachimKloubert.CloudNET.Classes.Security;
using MarcelJoachimKloubert.CloudNET.Classes.Web;
using MarcelJoachimKloubert.CloudNET.Extensions;
using MarcelJoachimKloubert.CLRToolbox;
using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace MarcelJoachimKloubert.CloudNET.Handlers
{
    /// <summary>
    /// A basic <see cref="IHttpHandler" /> that uses basic auth for checking username and password.
    /// </summary>
    public abstract class BasicAuthHttpHandlerBase : TMObject, IHttpHandler, IRouteHandler
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthHttpHandlerBase"/> class.
        /// </summary>
        /// <param name="sync">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected BasicAuthHttpHandlerBase(object sync)
            : base(sync)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthHttpHandlerBase"/> class.
        /// </summary>
        protected BasicAuthHttpHandlerBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <inheriteddoc />
        public virtual bool IsReusable
        {
            get { return false; }
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (2) 

        /// <inheriteddoc />
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }

        /// <inheriteddoc />
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                // check login
                ICloudPrincipal loggedInUser = null;
                {
                    var data = (context.Request.ServerVariables["HTTP_AUTHORIZATION"] ?? string.Empty).Trim();
                    if (string.IsNullOrWhiteSpace(data) == false)
                    {
                        if (data.ToLower().StartsWith("basic "))
                        {
                            try
                            {
                                var base64EncodedData = data.Substring(data.IndexOf(" ")).Trim();
                                var blobData = Convert.FromBase64String(base64EncodedData);

                                var strData = new UTF8Encoding().GetString(blobData);

                                var semicolon = strData.IndexOf(":");
                                if (semicolon > -1)
                                {
                                    var username = strData.Substring(0, semicolon).ToLower().Trim();
                                    if (username == string.Empty)
                                    {
                                        username = null;
                                    }

                                    string pwd = null;
                                    if (semicolon < (strData.Length - 1))
                                    {
                                        pwd = strData.Substring(semicolon + 1);
                                    }

                                    if (string.IsNullOrEmpty(pwd))
                                    {
                                        pwd = null;
                                    }

                                    loggedInUser = context.GetPrincipalRepository()
                                                          .TryFindPrincipalByLogin(username, pwd);
                                }
                            }
                            catch
                            {
                                // ignore errors here
                            }
                        }
                    }
                }

                if (loggedInUser == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.AddHeader("WWW-Authenticate", "BASIC Realm=Cloud.NET");

                    return;
                }

                var req = new CloudRequest()
                    {
                        Context = context,
                        Principal = loggedInUser,
                    };

                this.OnProcessRequest(req);
            }
            finally
            {
                context.Response.End();
            }
        }
        // Protected Methods (1) 

        /// <summary>
        /// Processes a request.
        /// </summary>
        /// <param name="request">The request context.</param>
        protected abstract void OnProcessRequest(ICloudRequest request);

        #endregion Methods
    }
}
