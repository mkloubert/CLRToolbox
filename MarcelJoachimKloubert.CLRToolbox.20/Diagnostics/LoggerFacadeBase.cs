// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// A basic logger.
    /// </summary>
    public abstract partial class LoggerFacadeBase : ILoggerFacade
    {
        #region Fields (2)

        private readonly Action<ILogMessage> _ON_LOG_ACTION;
        /// <summary>
        /// An unique object for thread-safe operations.
        /// </summary>
        protected readonly object _SYNC = new object();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerFacadeBase" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Logging should be thread-safe or not.</param>
        protected LoggerFacadeBase(bool isThreadSafe)
        {
            if (isThreadSafe)
            {
                this._ON_LOG_ACTION = this.OnLog_ThreadSafe;
            }
            else
            {
                this._ON_LOG_ACTION = this.OnLog;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerFacadeBase" /> class.
        /// </summary>
        /// <remarks>Logging is thread safe.</remarks>
        protected LoggerFacadeBase()
            : this(true)
        {

        }

        #endregion Constructors

        #region Methods (7)

        // Public Methods (5) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ILoggerFacade.Log(object)" />
        public void Log(object msg)
        {
            this.LogInner(DateTimeOffset.Now,
                          Assembly.GetCallingAssembly(),
                          null,
                          null,
                          msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ILoggerFacade.Log(ILogMessage)" />
        public void Log(ILogMessage msgObj)
        {
            if (msgObj == null)
            {
                throw new ArgumentNullException("msgObj");
            }

            this.OnLog(msgObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ILoggerFacade.Log(object, IEnumerable{char})" />
        public void Log(object msg, IEnumerable<char> tag)
        {
            this.LogInner(DateTimeOffset.Now,
                          Assembly.GetCallingAssembly(),
                          null,
                          StringHelper.AsString(tag),
                          msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ILoggerFacade.Log(object, LoggerFacadeCategories)" />
        public void Log(object msg, LoggerFacadeCategories categories)
        {
            this.LogInner(DateTimeOffset.Now,
                          Assembly.GetCallingAssembly(),
                          categories,
                          null,
                          msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ILoggerFacade.Log(object, IEnumerable{char}, LoggerFacadeCategories)" />
        public void Log(object msg, IEnumerable<char> tag, LoggerFacadeCategories categories)
        {
            this.LogInner(DateTimeOffset.Now,
                          Assembly.GetCallingAssembly(),
                          categories,
                          StringHelper.AsString(tag),
                          msg);
        }
        // Protected Methods (1) 

        /// <summary>
        /// Contains the logic of logging.
        /// </summary>
        /// <param name="msg">The message to log.</param>
        protected abstract void OnLog(ILogMessage msg);
        // Private Methods (1) 

        private void OnLog_ThreadSafe(ILogMessage msg)
        {
            lock (this._SYNC)
            {
                this.OnLog(msg);
            }
        }

        #endregion Methods
    }
}
