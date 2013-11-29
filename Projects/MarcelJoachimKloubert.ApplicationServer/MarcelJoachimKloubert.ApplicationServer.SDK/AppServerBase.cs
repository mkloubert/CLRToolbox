// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// A basic application server.
    /// </summary>
    public abstract partial class AppServerBase : DisposableBase, IAppServer
    {
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

        #region Properties (4)

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
        /// <see cref="IRunnable.IsRunning" />
        public bool IsRunning
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Restart()" />
        public void Restart()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

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

                if (!this.CanStop)
                {
                    throw new InvalidOperationException();
                }

                this.StopInner(StartStopContext.Stop);
            }
        }
        // Protected Methods (3) 

        /// <summary>
        /// The logic for disposing or finalizing that server object.
        /// </summary>
        /// /// <param name="disposing">
        /// Is called from <see cref="DisposableBase.Dispose()" /> method (<see langword="true" />)
        /// or the finalizer (<see langword="false" />).
        /// </param>
        protected virtual void DisposeServer(bool disposing)
        {
            // dummy
        }

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
        // Private Methods (2) 

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

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DisposableBase.OnDispose(bool)" />
        protected override sealed void OnDispose(bool disposing)
        {
            this.StopInner(StartStopContext.Dispose);

            this.DisposeServer(disposing);
        }
    }
}
