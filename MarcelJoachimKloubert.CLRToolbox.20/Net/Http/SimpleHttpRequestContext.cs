// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// Simple implementation of <see cref="IHttpRequestContext" /> interface.
    /// </summary>
    public sealed class SimpleHttpRequestContext : TMObject, IHttpRequestContext
    {
        #region Fields (2)

        private IHttpRequest _request;
        private IHttpResponse _response;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpRequestContext" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SimpleHttpRequestContext(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpRequestContext" /> class.
        /// </summary>
        public SimpleHttpRequestContext()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequestContext.Request" />
        public IHttpRequest Request
        {
            get { return this._request; }

            set { this._request = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequestContext.Response" />
        public IHttpResponse Response
        {
            get { return this._response; }

            set { this._response = value; }
        }

        #endregion Properties
    }
}
