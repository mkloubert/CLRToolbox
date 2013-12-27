// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using MarcelJoachimKloubert.ApplicationServer;
using MarcelJoachimKloubert.AppServer.Services.WcfHttp.Wcf;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using WcfTransferMode = System.ServiceModel.TransferMode;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.IHttpServer))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class WcfHttpServer : HttpServerBase
    {
        #region Constructors (1)

        internal WcfHttpServer()
        {
            this.TransferMode = HttpTransferMode.Buffered;
        }

        #endregion Constructors

        #region Properties (5)

        [Import]
        internal IAppServer ApplicationServer
        {
            get;
            private set;
        }

        internal ServiceHost Host
        {
            get;
            private set;
        }

        [Import]
        internal ILoggerFacade Logger
        {
            get;
            private set;
        }

        internal X509Certificate2 SslCertificate
        {
            get;
            set;
        }

        public override bool SupportsSecureHttp
        {
            get { return true; }
        }

        #endregion Properties

        #region Methods (10)

        // Protected Methods (3) 

        protected override void OnSetSslCertificateByThumbprint(string thumbprint)
        {
            this.SetSslCertificate(StoreLocation.LocalMachine,
                                   StoreName.My,
                                   X509FindType.FindByThumbprint,
                                   new string((thumbprint ?? string.Empty).Select(c => char.ToUpper(c))
                                                                          .Where(c => (c >= 'A' && c <= 'F') ||
                                                                                      (c >= '0' && c <= '9'))
                                                                          .ToArray()));
        }

        protected override void OnStart(HttpServerBase.StartStopContext context, ref bool isRunning)
        {
            this.DisposeOldServiceHost();

            ServiceHost newHost = new ServiceHost(new WcfHttpServerService(this));
            try
            {
                var useHttps = this.UseSecureHttp;

                int port;
                if (useHttps)
                {
                    port = this.Port ?? DEFAULT_PORT_SECURE_HTTP;
                }
                else
                {
                    port = this.Port ?? DEFAULT_PORT_HTTP;
                }

                var baseUrl = new Uri(string.Format("http{0}://localhost:{1}",
                                                    useHttps ? "s" : string.Empty,
                                                    port));

                HttpTransportBindingElement transport;
                if (useHttps)
                {
                    transport = new HttpsTransportBindingElement();

                    // SSL certificate
                    newHost.Credentials
                           .ClientCertificate
                           .Certificate = this.SslCertificate;
                }
                else
                {
                    transport = new HttpTransportBindingElement();
                }

                transport.KeepAliveEnabled = false;
                transport.MaxBufferPoolSize = int.MaxValue;
                transport.MaxBufferSize = int.MaxValue;
                transport.MaxReceivedMessageSize = int.MaxValue;
                transport.TransferMode = ToWcfTransferMode(this.TransferMode);

                var credValidator = this.CredentialValidator;
                if (credValidator != null)
                {
                    transport.AuthenticationScheme = AuthenticationSchemes.Basic;

                    newHost.Description.Behaviors.Remove<ServiceCredentials>();
                    newHost.Description.Behaviors.Add(new PasswordCredentials(credValidator));
                }
                else
                {
                    transport.AuthenticationScheme = AuthenticationSchemes.Anonymous;
                }

                var binding = new CustomBinding(WcfHttpServerService.CreateWebMessageBindingEncoder(),
                                                transport);

                newHost.AddServiceEndpoint(typeof(IWcfHttpServerService), binding, baseUrl);

                newHost.Open();
                this.Host = newHost;
            }
            catch
            {
                // dispose before rethrow exception
                ((IDisposable)newHost).Dispose();

                throw;
            }
        }

        protected override void OnStop(HttpServerBase.StartStopContext context, ref bool isRunning)
        {
            this.DisposeOldServiceHost();
        }
        // Private Methods (3) 

        private void DisposeOldServiceHost()
        {
            const string LOG_TAG_PREFIX = "WcfHttpServer::DisposeOldServiceHost::";

            try
            {
                using (var srv = this.Host)
                {
                    this.Host = null;
                }
            }
            catch (Exception ex)
            {
                this.Logger
                    .Log(msg: ex.GetBaseException() ?? ex,
                         tag: LOG_TAG_PREFIX + "Dispose",
                         categories: LoggerFacadeCategories.Errors | LoggerFacadeCategories.Debug);
            }
        }

        private void SetSslCertificate(StoreLocation storeLocation,
                                       StoreName storeName,
                                       X509FindType findType,
                                       object findValue)
        {
            X509Certificate2Collection machtingCerts;

            var store = new X509Store(storeName, storeLocation);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                machtingCerts = store.Certificates
                                     .Find(findType, findValue, false);
            }
            finally
            {
                store.Close();
            }

            this.SslCertificate = machtingCerts.Cast<X509Certificate2>()
                                               .Single();
        }

        private static WcfTransferMode ToWcfTransferMode(HttpTransferMode mode)
        {
            WcfTransferMode result;
            if (!Enum.TryParse<WcfTransferMode>(mode.ToString(), true, out result))
            {
                result = WcfTransferMode.Buffered;
            }

            return result;
        }
        // Internal Methods (4) 

        internal bool RaiseHandleDocumentNotFound(IHttpRequest req, IHttpResponse resp)
        {
            return this.OnHandleDocumentNotFound(req, resp);
        }

        internal bool RaiseHandleError(IHttpRequest req, IHttpResponse resp, Exception ex)
        {
            return this.OnHandleError(req, resp, ex);
        }

        internal bool RaiseHandleForbidden(IHttpRequest req, IHttpResponse resp)
        {
            return this.OnHandleForbidden(req, resp);
        }

        internal bool RaiseHandleRequest(IHttpRequest req, IHttpResponse resp)
        {
            return this.OnHandleRequest(req, resp);
        }

        #endregion Methods
    }
}
