// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Security;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// A basic HTTP server.
    /// </summary>
    public abstract partial class HttpServerBase : DisposableBase, IHttpServer
    {
        #region Fields (9)

        private UsernamePasswordValidator _credentialValidator;
        private bool _isRunning;
        private int? _port;
        private HttpPrincipalProvider _principalFinder;
        private HttpRequestValidator _requestValidator;
        private HttpTransferMode _transferMode;
        private bool _useSecureHttp;
        /// <summary>
        /// The default port for HTTP requests.
        /// </summary>
        public const int DEFAULT_PORT_HTTP = 80;
        /// <summary>
        /// The default port for secure HTTP requests.
        /// </summary>
        public const int DEFAULT_PORT_SECURE_HTTP = 443;

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

        #region Properties (11)

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
        /// <see cref="IHttpServer.CredentialValidator" />
        public UsernamePasswordValidator CredentialValidator
        {
            get { return this._credentialValidator; }

            set { this._credentialValidator = value; }
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
        public int? Port
        {
            get { return this._port; }

            set { this._port = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.PrincipalFinder" />
        public HttpPrincipalProvider PrincipalFinder
        {
            get { return this._principalFinder; }

            set { this._principalFinder = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.RequestValidator" />
        public HttpRequestValidator RequestValidator
        {
            get { return this._requestValidator; }

            set { this._requestValidator = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.SupportsSecureHttp" />
        public virtual bool SupportsSecureHttp
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.TransferMode" />
        public HttpTransferMode TransferMode
        {
            get { return this._transferMode; }

            set { this._transferMode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.UseSecureHttp" />
        public bool UseSecureHttp
        {
            get { return this._useSecureHttp; }

            set { this._useSecureHttp = value; }
        }

        #endregion Properties

        #region Delegates and Events (4)

        // Events (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.HandleDocumentNotFound" />
        public event EventHandler<HttpRequestEventArgs> HandleDocumentNotFound;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.HandleError" />
        public event EventHandler<HttpRequestErrorEventArgs> HandleError;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.HandleForbidden" />
        public event EventHandler<HttpRequestEventArgs> HandleForbidden;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpServer.HandleRequest" />
        public event EventHandler<HttpRequestEventArgs> HandleRequest;

        #endregion Delegates and Events

        #region Methods (15)

        // Public Methods (4) 

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
        /// <see cref="IHttpServer.SetSslCertificateByThumbprint(IEnumerable{char})" />
        public HttpServerBase SetSslCertificateByThumbprint(IEnumerable<char> thumbprint)
        {
            lock (this._SYNC)
            {
                if (!this.SupportsSecureHttp)
                {
                    // does not support HTTPs
                    throw new NotSupportedException();
                }

                this.OnSetSslCertificateByThumbprint(StringHelper.AsString(thumbprint));
                return this;
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

                if (!this.SupportsSecureHttp &&
                    this.UseSecureHttp)
                {
                    // does not support HTTPs
                    throw new NotSupportedException();
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
        // Protected Methods (8) 

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
        /// Raises the <see cref="HttpServerBase.HandleDocumentNotFound" /> event.
        /// </summary>
        /// <param name="req">The request context.</param>
        /// <param name="resp">The response context.</param>
        /// <returns>Event handler was raised or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="req" /> and/or <paramref name="resp" /> are <see langword="null" />.
        /// </exception>
        protected bool OnHandleDocumentNotFound(IHttpRequest req, IHttpResponse resp)
        {
            return this.OnHandle(this.HandleDocumentNotFound,
                                 req, resp);
        }

        /// <summary>
        /// Raises the <see cref="HttpServerBase.HandleError" /> event.
        /// </summary>
        /// <param name="req">The request context.</param>
        /// <param name="resp">The response context.</param>
        /// <param name="ex">The thrown exception.</param>
        /// <returns>Event handler was raised or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="req" />, <paramref name="resp" /> and/or <paramref name="ex" /> are <see langword="null" />.
        /// </exception>
        protected bool OnHandleError(IHttpRequest req, IHttpResponse resp, Exception ex)
        {
            if (req == null)
            {
                throw new ArgumentNullException("req");
            }

            if (resp == null)
            {
                throw new ArgumentNullException("resp");
            }

            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            HttpRequestErrorEventArgs e = new HttpRequestErrorEventArgs(req, resp, ex);
            e.Handled = false;

            EventHandler<HttpRequestErrorEventArgs> handler = this.HandleError;
            if (handler != null)
            {
                e.Handled = true;
                handler(this, new HttpRequestErrorEventArgs(req, resp, ex));
            }

            return e.Handled;
        }

        /// <summary>
        /// Raises the <see cref="HttpServerBase.HandleForbidden" /> event.
        /// </summary>
        /// <param name="req">The request context.</param>
        /// <param name="resp">The response context.</param>
        /// <returns>Event handler was raised or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="req" /> and/or <paramref name="resp" /> are <see langword="null" />.
        /// </exception>
        protected bool OnHandleForbidden(IHttpRequest req, IHttpResponse resp)
        {
            return this.OnHandle(this.HandleForbidden,
                                 req, resp);
        }

        /// <summary>
        /// Raises the <see cref="HttpServerBase.HandleRequest" /> event.
        /// </summary>
        /// <param name="req">The request context.</param>
        /// <param name="resp">The response context.</param>
        /// <returns>Event handler was raised or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="req" /> and/or <paramref name="resp" /> are <see langword="null" />.
        /// </exception>
        protected bool OnHandleRequest(IHttpRequest req, IHttpResponse resp)
        {
            return this.OnHandle(this.HandleRequest,
                                 req, resp);
        }

        /// <summary>
        /// The logic for <see cref="HttpServerBase.SetSslCertificateByThumbprint(IEnumerable{char})" /> method.
        /// </summary>
        /// <param name="thumbprint">The thumbprint to search for.</param>
        protected virtual void OnSetSslCertificateByThumbprint(string thumbprint)
        {
            throw new NotImplementedException();
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
        // Private Methods (3) 

        private bool OnHandle(EventHandler<HttpRequestEventArgs> handler, IHttpRequest req, IHttpResponse resp)
        {
            if (req == null)
            {
                throw new ArgumentNullException("req");
            }

            if (resp == null)
            {
                throw new ArgumentNullException("resp");
            }

            HttpRequestEventArgs e = new HttpRequestEventArgs(req, resp);
            e.Handled = false;

            if (handler != null)
            {
                e.Handled = true;
                handler(this, e);
            }

            return e.Handled;
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
