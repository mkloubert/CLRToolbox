// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json;

namespace AppServerProcessManager.JSON.ListProcesses
{
    /// <summary>
    /// Stores file information of a remote process.
    /// </summary>
    public sealed class RemoteProcessModuleFileInfo
    {
        #region Fields (6)

        /// <summary>
        /// Stores the description.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description;
        /// <summary>
        /// Stores the internal name.
        /// </summary>
        [JsonProperty(PropertyName = "internalName")]
        public string InternalName;
        /// <summary>
        /// Stores if module is in debug mode or not.
        /// </summary>
        [JsonProperty(PropertyName = "isDebug")]
        public bool? IsDebug;
        /// <summary>
        /// Stores the language of the module.
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language;
        /// <summary>
        /// Stores the product name of the module.
        /// </summary>
        [JsonProperty(PropertyName = "productName")]
        public string ProductName;
        /// <summary>
        /// Stores the version string of the module.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version;

        #endregion Fields

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return string.Format("{0} ({1})",
                                 this.ProductName,
                                 this.InternalName);
        }

        #endregion Methods
    }
}
