// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json;

namespace AppServerProcessManager.JSON.ListProcesses
{
    /// <summary>
    /// Stores data of a process module.
    /// </summary>
    public sealed class RemoteProcessModule
    {
        #region Fields (4)

        /// <summary>
        /// Stores the file path.
        /// </summary>
        [JsonProperty(PropertyName = "filePath")]
        public string FilePath;
        /// <summary>
        /// Stores file version information.
        /// </summary>
        [JsonProperty(PropertyName = "fileVersion")]
        public RemoteProcessModuleFileInfo FileVersion;
        /// <summary>
        /// Stores the zero based index.
        /// </summary>
        [JsonProperty(PropertyName = "index")]
        public int? Index;
        /// <summary>
        /// Stores the name of the module.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name;

        #endregion Fields

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return this.Name ?? string.Empty;
        }

        #endregion Methods
    }
}
