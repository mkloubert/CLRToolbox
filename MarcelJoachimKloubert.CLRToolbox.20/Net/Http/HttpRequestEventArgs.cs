// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// Arguments for an event that works with HTTP requests and response data.
    /// </summary>
    public class HttpRequestEventArgs : EventArgs
    {
        #region Fields (2)

        private IHttpRequest _request;
        private IHttpResponse _response;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestEventArgs"/> class.
        /// </summary>
        /// <param name="req">The value for the <see cref="HttpRequestEventArgs.Request" /> property.</param>
        /// <param name="resp">The value for the <see cref="HttpRequestEventArgs.Response" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="req" /> and/or <paramref name="resp" /> is <see langword="null" />.
        /// </exception>
        public HttpRequestEventArgs(IHttpRequest req, IHttpResponse resp)
        {
            if (req == null)
            {
                throw new ArgumentNullException("req");
            }

            if (resp == null)
            {
                throw new ArgumentNullException("resp");
            }

            this._request = req;
            this._response = resp;
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpRequestContext.Response" />
        public IHttpResponse Response
        {
            get { return this._response; }
        }

        #endregion Properties
    }
}
