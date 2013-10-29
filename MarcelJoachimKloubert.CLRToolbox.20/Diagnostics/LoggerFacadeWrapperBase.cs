using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// A basic logger that wraps another logger.
    /// </summary>
    public abstract class LoggerFacadeWrapperBase : LoggerFacadeBase
    {
        #region Fields (1)

        /// <summary>
        /// Stores the inner logger.
        /// </summary>
        protected readonly ILoggerFacade _INNER_LOGGER;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerFacadeWrapperBase"/> class.
        /// </summary>
        /// <param name="innerLogger">The inner logger.</param>
        /// <param name="isThreadSafe">Logging is thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="innerLogger" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>Logging is thread safe.</remarks>
        protected LoggerFacadeWrapperBase(ILoggerFacade innerLogger,
                                          bool isThreadSafe)
            : base(isThreadSafe)
        {
            if (innerLogger == null)
            {
                throw new ArgumentNullException("innerLogger");
            }

            this._INNER_LOGGER = innerLogger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerFacadeWrapperBase"/> class.
        /// </summary>
        /// <param name="innerLogger">The inner logger.</param>
        /// <remarks>Logging is thread safe.</remarks>
        protected LoggerFacadeWrapperBase(ILoggerFacade innerLogger)
            : this(innerLogger,
                   true)
        {

        }

        #endregion Constructors
    }
}
