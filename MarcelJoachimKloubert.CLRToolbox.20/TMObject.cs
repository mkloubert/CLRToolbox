// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

#if !WINDOWS_PHONE
#define CAN_SERIALIZE
#endif

using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// The mother of all objects.
    /// </summary>
    public partial class TMObject :
#if CAN_SERIALIZE
        global::System.MarshalByRefObject,
#endif
        ITMObject
    {
        #region Fields (2)

        /// <summary>
        /// An unique object for sync operations.
        /// </summary>
#if CAN_SERIALIZE
        [global::System.NonSerialized]
#endif
        protected readonly object _SYNC;
#if CAN_SERIALIZE
        [global::System.NonSerialized]
#endif
        private object _tag;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMObject" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
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

        /// <inheriteddoc />
        public virtual object Tag
        {
            get { return this._tag; }

            set { this._tag = value; }
        }

        #endregion Properties

        #region Methods (2)

        // Protected Methods (2) 

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