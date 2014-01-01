// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json;

namespace AppServerProcessManager.JSON.ListProcesses
{
    /// <summary>
    /// Stores the data of a remote process user.
    /// </summary>
    public sealed class RemoteProcessUser
    {
        #region Fields (2)

        /// <summary>
        /// Stores the domain name of the user.
        /// </summary>
        [JsonProperty(PropertyName = "domain")]
        public string Domain;
        /// <summary>
        /// Stores the name of the user.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Username;

        #endregion Fields

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return string.Format("{0}\\{1}",
                                 this.Domain, this.Username);
        }

        #endregion Methods
    }
}
