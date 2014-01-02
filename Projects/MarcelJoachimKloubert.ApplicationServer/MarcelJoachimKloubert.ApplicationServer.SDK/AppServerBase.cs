// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// A basic application server.
    /// </summary>
    public abstract partial class AppServerBase : NotificationObjectBase, IAppServer
    {
        #region Fields (2)

        private IAppServerContext _context;
        private bool _isDisposed;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AppServerBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerBase" /> class.
        /// </summary>
        protected AppServerBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (7)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanRestart" />
        public virtual bool CanRestart
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanStart" />
        public virtual bool CanStart
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanStop" />
        public virtual bool CanStop
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServer.Context" />
        public virtual IAppServerContext Context
        {
            get { return this._context; }

            private set
            {
                if (!object.Equals(this._context, value))
                {
                    this.OnPropertyChanging(() => this.Context);
                    this.OnPropertyChanging(() => this.IsInitialized);

                    this._context = value;

                    this.OnPropertyChanged(() => this.Context);
                    this.OnPropertyChanged(() => this.IsInitialized);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMDisposable.IsDisposed" />
        public bool IsDisposed
        {
            get { return this._isDisposed; }

            private set
            {
                if (value != this._isDisposed)
                {
                    this.OnPropertyChanging(() => this.IsDisposed);
                    this._isDisposed = value;
                    this.OnPropertyChanged(() => this.IsDisposed);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServer.IsInitialized" />
        public bool IsInitialized
        {
            get { return this.Context != null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.IsRunning" />
        public bool IsRunning
        {
            get;
            private set;
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

        #region Methods (14)

        // Public Methods (5) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IDisposable.Dispose()" />
        public void Dispose()
        {
            this.DisposeInner(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServer.Initialize(IAppServerInitContext)" />
        public void Initialize(IAppServerInitContext initContext)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (initContext == null)
                {
                    throw new ArgumentNullException("initContext");
                }

                if (this.IsInitialized)
                {
                    throw new InvalidOperationException();
                }

                var context = initContext.ServerContext;
                if (context == null)
                {
                    throw new ArgumentException("initContext");
                }

                var isInitialized = true;
                this.OnInitialize(initContext,
                                  ref isInitialized);

                this.Context = isInitialized ? context : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Restart()" />
        public void Restart()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();
                this.ThrowIfNotInitialized();

                if (!this.CanRestart)
                {
                    throw new InvalidOperationException();
                }

                this.StopInner(StartStopContext.Restart);
                this.StartInner(StartStopContext.Restart);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Start()" />
        public void Start()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();
                this.ThrowIfNotInitialized();

                if (!this.CanStart)
                {
                    throw new InvalidOperationException();
                }

                this.StartInner(StartStopContext.Start);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Stop()" />
        public void Stop()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();
                this.ThrowIfNotInitialized();

                if (!this.CanStop)
                {
                    throw new InvalidOperationException();
                }

                this.StopInner(StartStopContext.Stop);
            }
        }
        // Protected Methods (6) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DisposableBase.OnDispose(bool)" />
        protected virtual void OnDispose(bool disposing)
        {
            // dummy
        }

        /// <summary>
        /// The logic for the <see cref="AppServerBase.Initialize(IAppServerInitContext)" /> method.
        /// </summary>
        /// <param name="initContext">The context.</param>
        /// <param name="isInitialized">
        /// Defines if initilize operation was successful or not.
        /// Is <see langword="true" /> at the beginning.
        /// </param>
        protected abstract void OnInitialize(IAppServerInitContext initContext,
                                             ref bool isInitialized);

        /// <summary>
        /// The logic for <see cref="AppServerBase.Start()" /> and
        /// the <see cref="AppServerBase.Restart()" /> method.
        /// </summary>
        /// <param name="context">The invokation context.</param>
        /// <param name="isRunning">
        /// The new value for <see cref="AppServerBase.IsRunning" /> property.
        /// Is <see langword="true" /> by default.
        /// </param>
        protected abstract void OnStart(StartStopContext context,
                                        ref bool isRunning);

        /// <summary>
        /// The logic for <see cref="AppServerBase.Stop()" /> and
        /// the <see cref="AppServerBase.Restart()" /> method.
        /// </summary>
        /// <param name="context">The invokation context.</param>
        /// <param name="isRunning">
        /// The new value for <see cref="AppServerBase.IsRunning" /> property.
        /// Is <see langword="false" /> by default.
        /// </param>
        protected abstract void OnStop(StartStopContext context,
                                       ref bool isRunning);

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

        /// <summary>
        /// Throws an exception if that object has not been initialized yet.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Object has not been initialized yet.
        /// </exception>
        protected void ThrowIfNotInitialized()
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException("Object has not been initialized yet!");
            }
        }
        // Private Methods (3) 

        private void DisposeInner(bool disposing)
        {
            lock (this._SYNC)
            {
                if (disposing && this.IsDisposed)
                {
                    return;
                }

                this.StopInner(StartStopContext.Dispose);

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

        private void StartInner(StartStopContext context)
        {
            if (this.IsRunning)
            {
                return;
            }

            var isRunning = true;
            try
            {
                this.OnStart(context, ref isRunning);
            }
            finally
            {
                this.IsRunning = isRunning;
            }
        }

        private void StopInner(StartStopContext context)
        {
            if (!this.IsRunning)
            {
                return;
            }

            var isRunning = false;
            try
            {
                this.OnStop(context, ref isRunning);
            }
            finally
            {
                this.IsRunning = isRunning;
            }
        }

        #endregion Methods
    }
}
