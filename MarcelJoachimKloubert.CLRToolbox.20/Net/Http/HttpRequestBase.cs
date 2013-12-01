// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;

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

        #region Properties (5)

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
        /// <see cref="IHttpRequest.RemoteAddress" />
        public abstract ITcpAddress RemoteAddress
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

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequest.GetBody()" />
        public abstract Stream GetBody();

        #endregion Methods
    }
}
