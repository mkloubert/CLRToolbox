// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json;

namespace AppServerProcessManager.JSON.ListProcesses
{
    /// <summary>
    /// Stores the icon of a process.
    /// </summary>
    public sealed class RemoteProcessIcon
    {
        #region Fields (2)

        /// <summary>
        /// Stores the data of the icon.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public byte[] Data;

        /// <summary>
        /// Stores the mime type of the icon.
        /// </summary>
        [JsonProperty(PropertyName = "mime")]
        public string MimeType;

        #endregion Fields
    }
}
