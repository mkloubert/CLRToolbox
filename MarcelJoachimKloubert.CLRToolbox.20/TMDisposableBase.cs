// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// A basic thread safe disposable object.
    /// </summary>
    public abstract class TMDisposableBase : TMObject, ITMDisposable
    {
        #region Fields (2)

        private bool _isDisposed;
        /// <summary>
        /// Stores an unique object for sync operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMDisposableBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected TMDisposableBase(object syncRoot)
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            this._SYNC = syncRoot;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMDisposableBase" /> class.
        /// </summary>
        protected TMDisposableBase()
            : this(new object())
        {

        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TMDisposableBase" /> class.
        /// </summary>
        ~TMDisposableBase()
        {
            this.DisposeInner(false);
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMDisposable.IsDisposed" />
        public virtual bool IsDisposed
        {
            get { return this._isDisposed; }

            protected set { this._isDisposed = value; }
        }

        #endregion Properties

        #region Delegates and Events (2)

        // Events (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMDisposable.Disposed" />
        public event EventHandler Disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMDisposable.Disposing" />
        public event EventHandler Disposing;

        #endregion Delegates and Events

        #region Methods (5)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IDisposable.Dispose()" />
        public void Dispose()
        {
            this.DisposeInner(true);
            GC.SuppressFinalize(this);
        }
        // Protected Methods (3) 

        /// <summary>
        /// The logic for the <see cref="TMDisposableBase.Dispose()" /> method
        /// and the finalizer.
        /// </summary>
        /// <param name="disposing">
        /// Is called from <see cref="TMDisposableBase.Dispose()" /> method (<see langword="true" />)
        /// or the finalizer (<see langword="false" />).
        /// </param>
        protected abstract void OnDispose(bool disposing);

        /// <summary>
        /// Raises an <see cref="EventHandler" /> for this instance.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns><paramref name="handler" /> was invoked or not.</returns>
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
        /// Throws an exception if that object has already been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">That object has already been disposed.</exception>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }
        // Private Methods (1) 

        private void DisposeInner(bool disposing)
        {
            lock (this._SYNC)
            {
                if (disposing && this.IsDisposed)
                {
                    return;
                }

                if (disposing)
                {
                    this.RaiseEventHandler(this.Disposing);
                }

                this.OnDispose(disposing);

                if (disposing)
                {
                    this.RaiseEventHandler(this.Disposed);
                    this.IsDisposed = true;
                }
            }
        }

        #endregion Methods
    }
}
