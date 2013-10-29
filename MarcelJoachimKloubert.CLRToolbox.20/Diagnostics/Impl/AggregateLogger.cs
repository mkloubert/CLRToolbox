// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that invokes an internal list of stored loggers step-by-step.
    /// </summary>
    public sealed class AggregateLogger : LoggerFacadeBase
    {
        #region Fields (1)

        private readonly List<ILoggerFacade> _LOGGERS = new List<ILoggerFacade>();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateLogger" /> class.
        /// </summary>
        /// <remarks>
        /// Logging is thread safe.
        /// </remarks>
        public AggregateLogger()
            : base(true)
        {

        }

        #endregion Constructors

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// Adds a logger.
        /// </summary>
        /// <param name="logger">The logger to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        public void Add(ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            lock (this._SYNC)
            {
                this._LOGGERS.Add(logger);
            }
        }

        /// <summary>
        /// Clears the list of loggers.
        /// </summary>
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._LOGGERS.Clear();
            }
        }

        /// <summary>
        /// Gets a new list of current stored loggers.
        /// </summary>
        /// <returns>A list of current loggers.</returns>
        public List<ILoggerFacade> GetLoggers()
        {
            lock (this._SYNC)
            {
                return new List<ILoggerFacade>(this._LOGGERS);
            }
        }

        /// <summary>
        /// Removes a logger.
        /// </summary>
        /// <param name="logger">The logger to remove.</param>
        /// <returns>Logger was removed or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        public bool Remove(ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            lock (this._SYNC)
            {
                return this._LOGGERS.Remove(logger);
            }
        }
        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="LoggerFacadeBase.OnLog(ILogMessage)" />
        protected override void OnLog(ILogMessage msg)
        {
            foreach (ILoggerFacade logger in this._LOGGERS)
            {
                try
                {
                    logger.Log(msg);
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
