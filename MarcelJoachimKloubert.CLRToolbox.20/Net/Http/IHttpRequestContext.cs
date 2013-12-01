// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// Describes the whole context of a HTTP request.
    /// </summary>
    public interface IHttpRequestContext : ITMObject
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the request (data).
        /// </summary>
        IHttpRequest Request { get; }

        /// <summary>
        /// The object that handles the response.
        /// </summary>
        IHttpResponse Response { set; }

        #endregion Data Members
    }
}
