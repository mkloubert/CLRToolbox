// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// List of known HTTP methods.
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// Converts the request connection to a transparent TCP/IP tunnel
        /// </summary>
        CONNECT,

        /// <summary>
        /// Deletes a resource
        /// </summary>
        DELETE,

        /// <summary>
        /// Gets a resource
        /// </summary>
        GET,

        /// <summary>
        /// Same as GET but only returns the meta information
        /// </summary>
        HEAD,

        /// <summary>
        /// Supported methods
        /// </summary>
        OPTIONS,

        /// <summary>
        /// Patches a resources
        /// </summary>
        PATCH,

        /// <summary>
        /// Gets a request by using request body
        /// </summary>
        POST,

        /// <summary>
        /// Store a resource
        /// </summary>
        PUT,

        /// <summary>
        /// Echo back the request data for debugging
        /// </summary>
        TRACE,
    }
}
