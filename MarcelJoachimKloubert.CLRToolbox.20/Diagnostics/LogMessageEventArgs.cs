// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// Arguments for an event that handles a <see cref="ILogMessage" />.
    /// </summary>
    public class LogMessageEventArgs : EventArgs
    {
        #region Fields (1)

        private readonly ILogMessage _MESSAGE;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessageEventArgs" /> class.
        /// </summary>
        /// <param name="msg">
        /// The value for the <see cref="LogMessageEventArgs.Message" /> property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="msg" /> is <see langword="null" />.
        /// </exception>
        public LogMessageEventArgs(ILogMessage msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }

            this._MESSAGE = msg;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying log message.
        /// </summary>
        public ILogMessage Message
        {
            get { return this._MESSAGE; }
        }

        #endregion Properties
    }
}