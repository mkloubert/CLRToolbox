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
    /// Result of ListProcess server function.
    /// </summary>
    public sealed class ListProcessesFuncResult
    {
        #region Fields (3)

        /// <summary>
        /// Stores the result code.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int? Code;
        /// <summary>
        /// Stores the result message.
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Message;
        /// <summary>
        /// Stores the result parameters.
        /// </summary>
        public ListProcessesFuncResultParams Parameters;

        #endregion Fields

        #region Properties (1)

        [JsonProperty(PropertyName = "params")]
        private JArray ParametersInner
        {
            set
            {
                ListProcessesFuncResultParams newValue = null;
                if (value != null)
                {
                    newValue = new ListProcessesFuncResultParams();
                    foreach (var i in value)
                    {
                        var dict = i.ToObject<IDictionary<string, object>>();

                        if (dict["Key"].Equals("processes"))
                        {
                            var processList = (JArray)dict["Value"];
                            if (processList != null)
                            {
                                newValue.Processes = new SynchronizedCollection<RemoteProcess>(new object(),
                                                                                               processList.ToObject<IEnumerable<RemoteProcess>>()
                                                                                                          .OrderBy(pi => (pi.Name ?? string.Empty).Trim(), StringComparer.InvariantCultureIgnoreCase)
                                                                                                          .ThenBy(pi => pi.StartTime));
                            }
                        }
                    }
                }

                this.Parameters = newValue;
            }
        }

        #endregion Properties
    }
}
