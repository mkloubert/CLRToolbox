﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

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
    public abstract partial class LoggerFacadeBase : TMObject, ILoggerFacade
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
                this._ON_LOG_ACTION = this.OnLog_NonThreadSafe;
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

        #region Methods (10)

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
        // Protected Methods (3) 

        /// <summary>
        /// Creates a copy of a <see cref="ILogMessage" /> object with a new ID.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <returns>
        /// The copy of <paramref name="src" /> or <see langword="null" /> if
        /// <paramref name="src" /> is also <see langword="null" />.
        /// </returns>
        protected static ILogMessage CreateCopyOfLogMessage(ILogMessage src)
        {
            if (src != null)
            {
                return CreateCopyOfLogMessage(src, src.Message);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a copy of a <see cref="ILogMessage" /> object with a new ID and a new value.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="msgVal">The value for the <see cref="ILogMessage.Message" /> property of the copy.</param>
        /// <returns>
        /// The copy of <paramref name="src" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="src" /> is <see langword="null" />.</exception>
        protected static ILogMessage CreateCopyOfLogMessage(ILogMessage src, object msgVal)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            LogMessage result = new LogMessage();
            result.Assembly = src.Assembly;
            result.Categories = src.Categories;
            result.Context = src.Context;
            result.Id = Guid.NewGuid();
            result.Member = src.Member;
            result.Message = msgVal;
            result.Principal = src.Principal;
            result.Tag = src.Tag;
            result.Thread = src.Thread;
            result.Time = src.Time;

            return result;
        }

        /// <summary>
        /// Contains the logic of logging.
        /// </summary>
        /// <param name="msg">The message to log.</param>
        protected abstract void OnLog(ILogMessage msg);
        // Private Methods (2) 

        private void OnLog_NonThreadSafe(ILogMessage msg)
        {
            if (msg != null)
            {
                this.OnLog(msg);
            }
        }

        private void OnLog_ThreadSafe(ILogMessage msg)
        {
            lock (this._SYNC)
            {
                this.OnLog_NonThreadSafe(msg);
            }
        }

        #endregion Methods
    }
}
