﻿// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.IHttpServer))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class WcfHttpServer : HttpServerBase
    {
        #region Properties (2)

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

        #endregion Properties

        #region Methods (4)

        // Protected Methods (2) 

        protected override void OnStart(HttpServerBase.StartStopContext context, ref bool isRunning)
        {
            this.DisposeOldServiceHost();

            ServiceHost newHost = null;
            try
            {
                newHost = new ServiceHost(new WcfHttpServerService(this));

                var port = this.Port;

                var baseUrl = new Uri("http://localhost:" + port);

                var transport = new HttpTransportBindingElement();
                transport.KeepAliveEnabled = false;
                transport.AuthenticationScheme = AuthenticationSchemes.Anonymous;
                transport.TransferMode = TransferMode.Buffered;
                transport.MaxReceivedMessageSize = int.MaxValue;
                transport.MaxBufferPoolSize = int.MaxValue;
                transport.MaxBufferSize = int.MaxValue;

                var binding = new CustomBinding(WcfHttpServerService.CreateWebMessageBindingEncoder(),
                                                transport);

                newHost.AddServiceEndpoint(typeof(IWcfHttpServerService), binding, baseUrl);
                newHost.Open();

                this.Host = newHost;
            }
            catch
            {
                if (newHost != null)
                {
                    ((IDisposable)newHost).Dispose();
                }

                throw;
            }
        }

        protected override void OnStop(HttpServerBase.StartStopContext context, ref bool isRunning)
        {
            this.DisposeOldServiceHost();
        }
        // Private Methods (1) 

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
                         categories: LoggerFacadeCategories.Errors);
            }
        }
        // Internal Methods (1) 

        internal void RaiseHandleRequest(IHttpRequest req, IHttpResponse resp)
        {
            this.OnHandleRequest(req, resp);
        }

        #endregion Methods
    }
}