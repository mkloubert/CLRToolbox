// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// A basic HTTP request context.
    /// </summary>
    public abstract class HttpResponseBase : TMObject, IHttpResponse
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HttpResponseBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseBase" /> class.
        /// </summary>
        protected HttpResponseBase()
            : base()
        {

        }

        #endregion Constructors
    }
}
