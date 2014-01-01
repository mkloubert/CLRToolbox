// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
using System.Collections.Generic;

namespace AppServerProcessManager.JSON.ListProcesses
{
    /// <summary>
    /// Result parameters of ListProcesses server function.
    /// </summary>
    public sealed class ListProcessesFuncResultParams : IEnumerable<RemoteProcess>
    {
        #region Fields (1)

        /// <summary>
        /// Stores the list of processes.
        /// </summary>
        public IList<RemoteProcess> Processes;

        #endregion Fields

        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEnumerable{T}.GetEnumerator()" />
        public IEnumerator<RemoteProcess> GetEnumerator()
        {
            return this.Processes
                       .GetEnumerator();
        }
        // Private Methods (1) 

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods
    }
}
