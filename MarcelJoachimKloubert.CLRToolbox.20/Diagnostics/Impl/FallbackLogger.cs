// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that defines a main logger and fallbacks that are called if
    /// the main logger fails.
    /// </summary>
    public sealed class FallbackLogger : LoggerFacadeWrapperBase
    {
        #region Fields (1)

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
            : base(mainLogger, true)
        {
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the main logger.
        /// </summary>
        public ILoggerFacade MainLogger
        {
            get { return this._INNER_LOGGER; }
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (7) 

        /// <summary>
        /// Adds a fallback logger.
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
                this._FALLBACK_LOGGERS.Add(logger);
            }
        }

        /// <summary>
        /// Clears the list of fallback loggers.
        /// </summary>
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._FALLBACK_LOGGERS.Clear();
            }
        }

        /// <summary>
        /// Creates a new instance from an inital list of loggers.
        /// </summary>
        /// <param name="mainLogger">The value for the <see cref="FallbackLogger.MainLogger" /> property.</param>
        /// <param name="loggers">The initial list of loggers to add to the new instance.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="mainLogger" /> and/or <paramref name="loggers" /> are <see langword="null" />.
        /// </exception>
        public static FallbackLogger Create(ILoggerFacade mainLogger, IEnumerable<ILoggerFacade> loggers)
        {
            if (loggers == null)
            {
                throw new ArgumentNullException("loggers");
            }

            FallbackLogger result = new FallbackLogger(mainLogger);
            CollectionHelper.ForEach(loggers,
                                     delegate(IForEachItemExecutionContext<ILoggerFacade> ctx)
                                     {
                                         result.Add(ctx.Item);
                                     });

            return result;
        }

        /// <summary>
        /// Creates a new instance from an inital list of loggers.
        /// </summary>
        /// <param name="mainLogger">The value for the <see cref="FallbackLogger.MainLogger" /> property.</param>
        /// <param name="loggers">The initial list of loggers to add to the new instance.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="mainLogger" /> and/or <paramref name="loggers" /> are <see langword="null" />.
        /// </exception>
        public static FallbackLogger Create(ILoggerFacade mainLogger, params ILoggerFacade[] loggers)
        {
            return Create(mainLogger,
                          (IEnumerable<ILoggerFacade>)loggers);
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
        public bool Remove(ILoggerFacade logger)
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
            List<Exception> occuredExceptions = new List<Exception>();
            bool throwExceptions = false;
            int index = 0;

            using (IEnumerator<ILoggerFacade> e = this._FALLBACK_LOGGERS.GetEnumerator())
            {
                ILoggerFacade currentLogger = this._INNER_LOGGER;
                while (currentLogger != null)
                {
                    bool searchForNextFallback = false;
                    throwExceptions = false;

                    try
                    {
                        if (currentLogger.Log(msg) == false)
                        {
                            throw new Exception(string.Format("Logger #{0} ({1}; {2}) failed!",
                                                              index,
                                                              currentLogger.GetType().FullName,
                                                              currentLogger.GetHashCode()));
                        }
                    }
                    catch (Exception ex)
                    {
                        searchForNextFallback = true;
                        throwExceptions = true;

                        occuredExceptions.Add(ex);
                    }
                    finally
                    {
                        currentLogger = null;
                    }

                    if (searchForNextFallback)
                    {
                        if (e.MoveNext())
                        {
                            currentLogger = e.Current;
                            ++index;
                        }
                    }
                }
            }

            if (throwExceptions)
            {
                throw new AggregateException(occuredExceptions);
            }
        }

        #endregion Methods
    }
}