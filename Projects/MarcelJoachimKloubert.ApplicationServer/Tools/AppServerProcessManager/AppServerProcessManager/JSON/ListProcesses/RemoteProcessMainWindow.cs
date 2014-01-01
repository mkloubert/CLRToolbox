// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json;

namespace AppServerProcessManager.JSON.ListProcesses
{
    /// <summary>
    /// Stores the data of a process's main window.
    /// </summary>
    public sealed class RemoteProcessMainWindow
    {
        #region Fields (1)

        /// <summary>
        /// Stores the title of the window.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title;

        #endregion Fields

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return this.Title ?? string.Empty;
        }

        #endregion Methods
    }
}
