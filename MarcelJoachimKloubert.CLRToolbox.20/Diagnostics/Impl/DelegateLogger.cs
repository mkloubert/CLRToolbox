// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
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

        #region Methods (7)

        // Public Methods (6) 

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
        /// Creates a new instance from an inital list of handlers.
        /// </summary>
        /// <param name="handlers">The initial list of handlers to add to the new instance.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handlers" /> is <see langword="null" />.
        /// </exception>
        public static DelegateLogger Create(IEnumerable<LogMessageHandler> handlers)
        {
            if (handlers == null)
            {
                throw new ArgumentNullException("handlers");
            }

            DelegateLogger result = new DelegateLogger();
            CollectionHelper.ForEach(handlers,
                                     delegate(IForEachItemExecutionContext<LogMessageHandler> ctx)
                                     {
                                         result.Add(ctx.Item);
                                     });

            return result;
        }

        /// <summary>
        /// Creates a new instance from an inital list of handlers.
        /// </summary>
        /// <param name="handlers">The initial list of handlers to add to the new instance.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handlers" /> is <see langword="null" />.
        /// </exception>
        public static DelegateLogger Create(params LogMessageHandler[] handlers)
        {
            return Create((IEnumerable<LogMessageHandler>)handlers);
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
            bool? allFailed = null;
            List<Exception> occuredExceptions = new List<Exception>();

            CollectionHelper.ForEach(this._DELEGATES,
                                     delegate(IForEachItemExecutionContext<LogMessageHandler> ctx)
                                     {
                                         try
                                         {
                                             ctx.Item(CloneLogMessage(msg));

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