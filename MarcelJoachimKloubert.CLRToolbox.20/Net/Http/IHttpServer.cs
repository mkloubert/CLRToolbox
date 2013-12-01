// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// Describes a HTTP server.
    /// </summary>
    public interface IHttpServer : ITMDisposable, IRunnable
    {
        #region Data Members (1)

        /// <summary>
        /// Gets or sets the TCP port to use.
        /// </summary>
        int Port { get; set; }

        #endregion Data Members

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// Is invoked when a HTTP has been arrived that should be handled.
        /// </summary>
        event EventHandler<HttpRequestEventArgs> HandleRequest;

        #endregion Delegates and Events
    }
}
