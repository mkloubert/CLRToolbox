// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// Provides access to the global logger instance.
    /// </summary>
    public static class GlobalLogger
    {
        #region Fields (1)

        private static LoggerProvider _provider;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes the <see cref="GlobalLogger" /> class.
        /// </summary>
        static GlobalLogger()
        {
            SetLogger(new ConsoleLogger());
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the global logger instance.
        /// </summary>
        public static ILoggerFacade Current
        {
            get { return _provider(); }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes the logic that returns the global logger.
        /// </summary>
        /// <returns>The global logger instance.</returns>
        public delegate ILoggerFacade LoggerProvider();

        #endregion Delegates and Events

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Sets the value for <see cref="GlobalLogger.Current" />.
        /// </summary>
        /// <param name="newLogger">The new logger.</param>
        public static void SetLogger(ILoggerFacade newLogger)
        {
            SetLoggerProvider(newLogger == null ? null : new LoggerProvider(delegate()
                {
                    return newLogger;
                }));
        }

        /// <summary>
        /// Sets the logic that returns the value for <see cref="GlobalLogger.Current" />.
        /// </summary>
        /// <param name="newProvider">The new provider delegate.</param>
        public static void SetLoggerProvider(LoggerProvider newProvider)
        {
            _provider = newProvider;
        }

        #endregion Methods
    }
}
