// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// A basic HTTP request context.
    /// </summary>
    public abstract class HttpRequestBase : TMObject, IHttpRequest
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HttpRequestBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestBase" /> class.
        /// </summary>
        protected HttpRequestBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (11)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.Address" />
        public abstract Uri Address
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.ContentType" />
        public abstract string ContentType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.Files" />
        public abstract IReadOnlyDictionary<string, IFile> Files
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.GET" />
        public abstract IReadOnlyDictionary<string, string> GET
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.Headers" />
        public abstract IReadOnlyDictionary<string, string> Headers
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.Method" />
        public abstract string Method
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.POST" />
        public abstract IReadOnlyDictionary<string, string> POST
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.RemoteAddress" />
        public abstract ITcpAddress RemoteAddress
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.REQUEST" />
        public abstract IReadOnlyDictionary<string, string> REQUEST
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.Time" />
        public abstract DateTimeOffset Time
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.User" />
        public abstract IPrincipal User
        {
            get;
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.GetBody()" />
        public abstract Stream GetBody();

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.GetBodyData()" />
        public byte[] GetBodyData()
        {
            using (Stream stream = this.GetBody())
            {
                if (stream != null)
                {
                    using (MemoryStream temp = new MemoryStream())
                    {
                        IOHelper.CopyTo(stream, temp);

                        return temp.ToArray();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.TryGetKnownMethod()" />
        public HttpMethod? TryGetKnownMethod()
        {
            string method = this.Method;
            if (StringHelper.IsNullOrWhiteSpace(method))
            {
                return HttpMethod.GET;
            }

            global::MarcelJoachimKloubert.CLRToolbox.Net.Http.HttpMethod? result;
            if (EnumHelper.TryParse<HttpMethod>(this.Method, true, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        #endregion Methods
    }
}
