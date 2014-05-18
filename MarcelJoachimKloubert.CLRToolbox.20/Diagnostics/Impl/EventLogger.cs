// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that raises an event if <see cref="EventLogger.OnLog(ILogMessage)" /> method is called.
    /// </summary>
    public class EventLogger : LoggerFacadeBase
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogger" /> class.
        /// </summary>
        public EventLogger()
            : base(false)
        {
        }

        #endregion Constructors

        #region Delegates and events (1)

        /// <summary>
        /// Is invoked when an <see cref="ILogMessage" /> object has been arrived in
        /// <see cref="EventLogger.OnLog(ILogMessage)" /> method.
        /// </summary>
        public event EventHandler<LogMessageEventArgs> MessageReceived;

        #endregion Delegates and events

        #region Methods (1)

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            if (this.RaiseEventHandler(this.MessageReceived, new LogMessageEventArgs(msg)) == false)
            {
                throw new NotImplementedException();
            }
        }

        #endregion Methods
    }
}