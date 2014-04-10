// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using System;

namespace MarcelJoachimKloubert.RoboGitGui.Classes
{
    /// <summary>
    /// Arguments for an event that provides / handles an <see cref="ILogMessage" />.
    /// </summary>
    public class GitTaskLogEventArgs : EventArgs
    {
        #region Constructors (1)

        internal GitTaskLogEventArgs(ILogMessage msg)
        {
            this.Message = msg;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying message.
        /// </summary>
        public ILogMessage Message
        {
            get;
            private set;
        }

        #endregion Properties
    }
}
