// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// The mother of all objects.
    /// </summary>
    public partial class TMObject : ITMObject
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMObject" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public TMObject(object syncRoot)
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            this._SYNC = syncRoot;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMObject" /> class.
        /// </summary>
        public TMObject()
            : this(new object())
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMObject.Tag" />
        public virtual object Tag
        {
            get { return this._tag; }

            set { this._tag = value; }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMObject.Error" />
        public event ErrorEventHandler Error;

        #endregion Delegates and Events

        #region Methods (3)

        // Protected Methods (3) 

        /// <summary>
        /// Raises the <see cref="TMObject.Error" /> event.
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

        /// <summary>
        /// Raises a general event handler.
        /// </summary>
        /// <param name="handler">The handler to raise.</param>
        /// <returns>Event was raised or not.</returns>
        protected bool RaiseEventHandler(EventHandler handler)
        {
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Raises a general event handler.
        /// </summary>
        /// <typeparam name="TArgs">Type of the event arguments.</typeparam>
        /// <param name="handler">The handler to raise.</param>
        /// <param name="e">The arguments for the event.</param>
        /// <returns>Event was raised or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="e" /> is <see langword="null" />.
        /// </exception>
        protected bool RaiseEventHandler<TArgs>(EventHandler<TArgs> handler, TArgs e) where TArgs : global::System.EventArgs
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            if (handler != null)
            {
                handler(this, e);
                return true;
            }

            return false;
        }

        #endregion Methods
    }
}
