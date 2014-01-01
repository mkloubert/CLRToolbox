// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppServerProcessManager.JSON.ListProcesses
{
    /// <summary>
    /// A remote process.
    /// </summary>
    public sealed class RemoteProcess
    {
        #region Fields (12)

        private IList<RemoteProcessModule> _modules;
        /// <summary>
        /// Stores the base priority.
        /// </summary>
        [JsonProperty(PropertyName = "basePriority")]
        public int? BasePriority;
        /// <summary>
        /// Stores the exit time.
        /// </summary>
        [JsonProperty(PropertyName = "exitTime")]
        public DateTimeOffset? ExitTime;
        /// <summary>
        /// Stores if the process has been exited or not.
        /// </summary>
        [JsonProperty(PropertyName = "hasExited")]
        public bool? HasExited;
        /// <summary>
        /// Stores the icon of the process.
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public RemoteProcessIcon Icon;
        /// <summary>
        /// Stores the ID.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id;
        /// <summary>
        /// Stores the name of the machine the process runs on.
        /// </summary>
        [JsonProperty(PropertyName = "machine")]
        public string MachineName;
        /// <summary>
        /// Stores the data of the main window.
        /// </summary>
        public RemoteProcessMainWindow MainWindow;
        /// <summary>
        /// Stores the name of the priority class.
        /// </summary>
        [JsonProperty(PropertyName = "priorityClass")]
        public string PriorityClass;
        /// <summary>
        /// Stores the session OD.
        /// </summary>
        [JsonProperty(PropertyName = "sessionId")]
        public int? SessionId;
        /// <summary>
        /// Stores the start time.
        /// </summary>
        [JsonProperty(PropertyName = "startTime")]
        public DateTimeOffset? StartTime;
        /// <summary>
        /// Stores information about the user the process runs on.
        /// </summary>
        public RemoteProcessUser User;

        #endregion Fields

        #region Properties (6)

        /// <summary>
        /// Gets the main module.
        /// </summary>
        public RemoteProcessModule MainModule
        {
            get;
            private set;
        }

        [JsonProperty(PropertyName = "mainWindow")]
        private JObject MainWindowInner
        {
            set
            {
                RemoteProcessMainWindow newValue = null;
                if (value != null)
                {
                    newValue = value.ToObject<RemoteProcessMainWindow>();
                }

                this.MainWindow = newValue;
            }
        }

        /// <summary>
        /// Gets or sets the list of modules.
        /// </summary>
        public IList<RemoteProcessModule> Modules
        {
            get { return this._modules; }

            set
            {
                RemoteProcessModule newMainModule = null;
                if (value != null)
                {
                    newMainModule = value.LastOrDefault(pm => pm != null &&
                                                              pm.Index == 0);
                }

                this._modules = value;
                this.MainModule = newMainModule;
            }
        }

        [JsonProperty(PropertyName = "modules")]
        private JArray ModulesInner
        {
            set
            {
                IList<RemoteProcessModule> newValue = null;
                if (value != null)
                {
                    newValue = new SynchronizedCollection<RemoteProcessModule>(syncRoot: new object(),
                                                                         list: value.Select(i => i.ToObject<RemoteProcessModule>())
                                                                                    .OrderBy(pm => (pm.Name ?? string.Empty).Trim(), StringComparer.InvariantCultureIgnoreCase)
                                                                                    .ThenBy(pm => pm.Index));
                }

                this.Modules = newValue;
            }
        }

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "user")]
        private JObject UserInner
        {
            set
            {
                RemoteProcessUser newValue = null;
                if (value != null)
                {
                    newValue = value.ToObject<RemoteProcessUser>();
                }

                this.User = newValue;
            }
        }

        #endregion Properties

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
