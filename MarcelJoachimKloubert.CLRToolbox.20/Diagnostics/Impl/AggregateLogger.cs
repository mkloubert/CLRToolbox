// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
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

        #region Methods (8)

        // Public Methods (7) 

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
        /// Creates a new instance from an inital list of loggers.
        /// </summary>
        /// <param name="loggers">The initial list of loggers to add to the new instance.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="loggers" /> is <see langword="null" />.
        /// </exception>
        public static AggregateLogger Create(IEnumerable<ILoggerFacade> loggers)
        {
            if (loggers == null)
            {
                throw new ArgumentNullException("loggers");
            }

            AggregateLogger result = new AggregateLogger();
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
        /// <param name="loggers">The initial list of loggers to add to the new instance.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="loggers" /> is <see langword="null" />.
        /// </exception>
        public static AggregateLogger Create(params ILoggerFacade[] loggers)
        {
            return Create((IEnumerable<ILoggerFacade>)loggers);
        }

        /// <summary>
        /// Returns a flatten list of loggers and sub loggers that are part of this instance and its children.
        /// </summary>
        /// <returns>The flatten list.</returns>
        public IEnumerable<ILoggerFacade> Flatten()
        {
            List<AggregateLogger> aggLogList = new List<AggregateLogger>();
            aggLogList.Add(this);

            int num = 0;
            while (aggLogList.Count > num)
            {
                IList<ILoggerFacade> innerLoggers = aggLogList[num++].GetLoggers();
                for (int i = 0; i < innerLoggers.Count; i++)
                {
                    ILoggerFacade logger = innerLoggers[i];

                    AggregateLogger aggLog = logger as AggregateLogger;
                    if (aggLog != null)
                    {
                        aggLogList.Add(aggLog);
                    }
                    else
                    {
                        yield return logger;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a new list of current stored loggers.
        /// </summary>
        /// <returns>A list of current loggers.</returns>
        public List<ILoggerFacade> GetLoggers()
        {
            List<ILoggerFacade> result;

            lock (this._SYNC)
            {
                result = new List<ILoggerFacade>(this._LOGGERS);
            }

            return result;
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

            bool result;

            lock (this._SYNC)
            {
                result = this._LOGGERS.Remove(logger);
            }

            return result;
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            bool? allFailed = null;
            List<Exception> occuredExceptions = new List<Exception>();

            CollectionHelper.ForEach(this._LOGGERS,
                                     delegate(IForEachItemExecutionContext<ILoggerFacade> ctx)
                                     {
                                         ILoggerFacade logger = ctx.Item;

                                         try
                                         {
                                             logger.Log(CloneLogMessage(msg));

                                             allFailed = false;
                                         }
                                         catch (Exception ex)
                                         {
                                             if (allFailed.HasValue == false)
                                             {
                                                 allFailed = true;
                                             }

                                             occuredExceptions.Add(ex);
                                         }
                                     });

            if (allFailed == true)
            {
                throw new AggregateException(occuredExceptions);
            }
        }

        #endregion Methods
    }
}