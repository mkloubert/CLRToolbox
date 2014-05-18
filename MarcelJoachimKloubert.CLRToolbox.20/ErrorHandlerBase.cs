// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// A basic object that can handle exceptions via events, e.g.
    /// </summary>
    public abstract class ErrorHandlerBase : TMObject, IErrorHandler
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlerBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected ErrorHandlerBase(object syncRoot)
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlerBase" /> class.
        /// </summary>
        protected ErrorHandlerBase()
            : base()
        {
        }

        #endregion Constructors

        #region Delegates and Events (1)

        // Events (1) 

        /// <inheriteddoc />
        public event ErrorEventHandler Error;

        #endregion Delegates and Events

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// Raises the <see cref="ErrorHandlerBase.Error" /> event.
        /// </summary>
        /// <param name="ex">The underlying exception.</param>
        /// <returns>Event was raised or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ex" /> is <see langword="null" />.
        /// </exception>
        protected bool OnError(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            ErrorEventHandler handler = this.Error;
            if (handler != null)
            {
                handler(this, new ErrorEventArgs(ex));
                return true;
            }

            return false;
        }

        #endregion Methods
    }
}