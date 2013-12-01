// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// List of transfer modes.
    /// </summary>
    public enum HttpTransferMode
    {
        /// <summary>
        /// Request and response are buffered.
        /// </summary>
        Buffered,

        /// <summary>
        /// Request and response are streamed.
        /// </summary>
        Streamed,

        /// <summary>
        /// Response is streamed.
        /// </summary>
        StreamedRequest,

        /// <summary>
        /// Request is streamed.
        /// </summary>
        StreamedResponse,
    }
}
