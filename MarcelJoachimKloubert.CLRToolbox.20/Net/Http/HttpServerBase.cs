// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// A basic HTTP server.
    /// </summary>
    public abstract partial class HttpServerBase : DisposableBase, IHttpServer
    {
        #region Fields (3)

        private bool _isRunning;
        private int _port = 80;
        /// <summary>
        /// The default port for HTTP requests.
        /// </summary>
        public const int DEFAULT_PORT_HTTP = 80;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpServerBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HttpServerBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpServerBase" /> class.
        /// </summary>
        protected HttpServerBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (5)

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
            get { return this._isRunning; }

            private set { this._isRunning = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.Port" />
        public int Port
        {
            get { return this._port; }

            set { this._port = value; }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.HandleRequest" />
        public event EventHandler<HttpRequestEventArgs> HandleRequest;

        #endregion Delegates and Events

        #region Methods (9)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Restart()" />
        public void Restart()
        {
            lock (this._SYNC)
            {
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
                if (!this.CanStop)
                {
                    throw new InvalidOperationException();
                }

                this.StopInner(StartStopContext.Stop);
            }
        }
        // Protected Methods (4) 

        /// <summary>
        /// The logic that disposes that server.
        /// </summary>
        /// <param name="disposing">
        /// Is called from <see cref="DisposableBase.Dispose()" /> method (<see langword="true" />)
        /// or the finalizer (<see langword="false" />).
        /// </param>
        protected virtual void DisposeServer(bool disposing)
        {
            // dummy
        }

        /// <summary>
        /// Raises the <see cref="HttpServerBase.HandleRequest" /> event.
        /// </summary>
        /// <param name="req">The request context.</param>
        /// <param name="resp">The response context.</param>
        /// <returns>Event handler was raised or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="req" /> and/or <paramref name="resp" /> is <see langword="null" />.
        /// </exception>
        protected bool OnHandleRequest(IHttpRequest req, IHttpResponse resp)
        {
            if (req == null)
            {
                throw new ArgumentNullException("req");
            }

            if (resp == null)
            {
                throw new ArgumentNullException("resp");
            }

            EventHandler<HttpRequestEventArgs> handler = this.HandleRequest;
            if (handler != null)
            {
                handler(this, new HttpRequestEventArgs(req, resp));
                return true;
            }

            return false;
        }

        /// <summary>
        /// The logic for <see cref="HttpServerBase.Start()" /> and
        /// the <see cref="HttpServerBase.Restart()" /> method.
        /// </summary>
        /// <param name="context">The invokation context.</param>
        /// <param name="isRunning">
        /// The new value for <see cref="HttpServerBase.IsRunning" /> property.
        /// Is <see langword="true" /> by default.
        /// </param>
        protected abstract void OnStart(StartStopContext context,
                                        ref bool isRunning);

        /// <summary>
        /// The logic for <see cref="HttpServerBase.Stop()" /> and
        /// the <see cref="HttpServerBase.Restart()" /> method.
        /// </summary>
        /// <param name="context">The invokation context.</param>
        /// <param name="isRunning">
        /// The new value for <see cref="HttpServerBase.IsRunning" /> property.
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
