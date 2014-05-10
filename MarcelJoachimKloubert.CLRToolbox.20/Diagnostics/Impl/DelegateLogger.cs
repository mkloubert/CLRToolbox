// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that invokes an internal list of stored delegates that handle <see cref="ILogMessage" />s step-by-step.
    /// </summary>
    public sealed class DelegateLogger : LoggerFacadeBase
    {
        #region Fields (1)

        private readonly List<LogMessageHandler> _DELEGATES = new List<LogMessageHandler>();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateLogger" /> class.
        /// </summary>
        /// <remarks>
        /// Logging is thread safe.
        /// </remarks>
        public DelegateLogger()
            : base(true)
        {
        }

        #endregion Constructors

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes a function or method that handles a log message.
        /// </summary>
        /// <param name="msg">The message to handle.</param>
        public delegate void LogMessageHandler(ILogMessage msg);

        #endregion Delegates and Events

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// Adds a handler.
        /// </summary>
        /// <param name="handler">The handler to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler" /> is <see langword="null" />.
        /// </exception>
        public void Add(LogMessageHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            lock (this._SYNC)
            {
                this._DELEGATES.Add(handler);
            }
        }

        /// <summary>
        /// Clears the list of handlers.
        /// </summary>
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._DELEGATES.Clear();
            }
        }

        /// <summary>
        /// Gets a new list of current stored handlers.
        /// </summary>
        /// <returns>A list of current handlers.</returns>
        public List<LogMessageHandler> GetHandlers()
        {
            List<LogMessageHandler> result;

            lock (this._SYNC)
            {
                result = new List<LogMessageHandler>(this._DELEGATES);
            }

            return result;
        }

        /// <summary>
        /// Removes a handler.
        /// </summary>
        /// <param name="handler">The handler to remove.</param>
        /// <returns>Handler was removed or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler" /> is <see langword="null" />.
        /// </exception>
        public bool Remove(LogMessageHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            bool result;

            lock (this._SYNC)
            {
                result = this._DELEGATES.Remove(handler);
            }

            return result;
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            foreach (LogMessageHandler handler in this._DELEGATES)
            {
                try
                {
                    handler(CloneLogMessage(msg));
                }
                catch
                {
                    // ignore errors here
                }
            }
        }

        #endregion Methods
    }
}