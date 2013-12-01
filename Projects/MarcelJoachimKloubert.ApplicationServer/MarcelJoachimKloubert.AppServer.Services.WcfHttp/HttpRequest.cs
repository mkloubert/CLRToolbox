// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.Net;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    internal sealed class HttpRequest : HttpRequestBase
    {
        #region Fields (8)

        private readonly IReadOnlyDictionary<string, string> _HEADERS;
        private readonly Message _MESSAGE;
        private readonly string _METHOD;
        private readonly HttpRequestMessageProperty _PROPERTY;
        private readonly ITcpAddress _REMOTE_ADDRESS;
        private readonly WcfHttpServer _SERVER;
        private readonly IPrincipal _USER;
        private readonly MessageEncoder _WEB_ENCODER = WcfHttpServerService.CreateWebMessageBindingEncoder()
                                                                           .CreateMessageEncoderFactory()
                                                                           .Encoder;

        #endregion Fields

        #region Constructors (1)

        internal HttpRequest(HttpRequestMessageProperty property,
                             Message message,
                             WcfHttpServer server)
        {
            this._PROPERTY = property;
            this._MESSAGE = message;
            this._SERVER = server;

            this._METHOD = (this._PROPERTY.Method ?? string.Empty).ToUpper().Trim();
            if (this._METHOD == string.Empty)
            {
                this._METHOD = null;
            }

            //  address of remote client
            try
            {
                var messageProperties = OperationContext.Current.IncomingMessageProperties;
                var endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

                this._REMOTE_ADDRESS = new SimpleTcpAddress()
                {
                    Address = endpointProperty.Address,
                    Port = endpointProperty.Port,
                };
            }
            catch
            {
                // ignore here
            }

            // requesting user
            try
            {
                ServiceSecurityContext context = ServiceSecurityContext.Current;
                if (context != null)
                {
                    var finder = this._SERVER.PrincipalFinder;
                    if (finder != null)
                    {
                        this._USER = finder(context.PrimaryIdentity);
                    }
                }
            }
            catch
            {
                // ignore here
            }

            // request headers
            {
                var headers = new ConcurrentDictionary<string, string>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(trim: true,
                                                                                                                                   emptyIsNull: true));
                foreach (var key in this._PROPERTY.Headers.AllKeys)
                {
                    headers[key] = this._PROPERTY.Headers[key];
                }

                this._HEADERS = new TMReadOnlyDictionary<string, string>(headers);
            }
        }

        #endregion Constructors

        #region Properties (5)

        public override Uri Address
        {
            get { return this._MESSAGE.Headers.To; }
        }

        public override IReadOnlyDictionary<string, string> Headers
        {
            get { return this._HEADERS; }
        }

        public override string Method
        {
            get { return this._METHOD; }
        }

        public override ITcpAddress RemoteAddress
        {
            get { return this._REMOTE_ADDRESS; }
        }

        public override IPrincipal User
        {
            get { return this._USER; }
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        public override Stream GetBody()
        {
            var result = new MemoryStream();
            try
            {
                this._WEB_ENCODER
                    .WriteMessage(this._MESSAGE, result);
            }
            catch
            {
                result.Dispose();

                throw;
            }

            return result;
        }

        #endregion Methods
    }
}
