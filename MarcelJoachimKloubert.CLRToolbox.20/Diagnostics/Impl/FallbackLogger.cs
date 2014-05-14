// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that defines a main logger and fallbacks that are called if
    /// the main logger fails.
    /// </summary>
    public sealed class FallbackLogger : LoggerFacadeBase
    {
        #region Fields (2)

        private readonly ILoggerFacade _MAIN_LOGGER;
        private readonly List<ILoggerFacade> _FALLBACK_LOGGERS = new List<ILoggerFacade>();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackLogger" /> class.
        /// </summary>
        /// <param name="mainLogger">The value for the <see cref="FallbackLogger.MainLogger" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="mainLogger" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Logging is thread safe.
        /// </remarks>
        public FallbackLogger(ILoggerFacade mainLogger)
            : base(true)
        {
            if (mainLogger == null)
            {
                throw new ArgumentNullException("mainLogger");
            }

            this._MAIN_LOGGER = mainLogger;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the main logger.
        /// </summary>
        public ILoggerFacade MainLogger
        {
            get { return this._MAIN_LOGGER; }
        }

        #endregion

        #region Methods (6)

        // Public Methods (5) 

        /// <summary>
        /// Adds a fallback logger.
        /// </summary>
        /// <param name="logger">The logger to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        public void AddFallback(ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            lock (this._SYNC)
            {
                this._FALLBACK_LOGGERS.Add(logger);
            }
        }

        /// <summary>
        /// Clears the list of fallback loggers.
        /// </summary>
        public void ClearFallbacks()
        {
            lock (this._SYNC)
            {
                this._FALLBACK_LOGGERS.Clear();
            }
        }

        /// <summary>
        /// Gets a new list of current stored fallback loggers.
        /// </summary>
        /// <returns>A list of current fallback loggers.</returns>
        public List<ILoggerFacade> GetFallbacks()
        {
            List<ILoggerFacade> result;

            lock (this._SYNC)
            {
                result = new List<ILoggerFacade>(this._FALLBACK_LOGGERS);
            }

            return result;
        }

        /// <summary>
        /// Removes a fallback logger.
        /// </summary>
        /// <param name="logger">The logger to remove.</param>
        /// <returns>Logger was removed or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        public bool RemoveFallback(ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            bool result;

            lock (this._SYNC)
            {
                result = this._FALLBACK_LOGGERS.Remove(logger);
            }

            return result;
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            using (IEnumerator<ILoggerFacade> e = this._FALLBACK_LOGGERS.GetEnumerator())
            {
                ILoggerFacade currentLogger = this._MAIN_LOGGER;
                while (currentLogger != null)
                {
                    bool findNextFallback = false;

                    try
                    {
                        if (currentLogger.Log(msg) == false)
                        {
                            findNextFallback = true;
                        }
                    }
                    catch
                    {
                        findNextFallback = true;
                    }
                    finally
                    {
                        currentLogger = null;
                    }

                    if (findNextFallback)
                    {
                        if (e.MoveNext())
                        {
                            currentLogger = e.Current;
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}