// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Prefix,
                     InstanceContextMode = InstanceContextMode.Single,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal sealed class WcfHttpServerService : IWcfHttpServerService
    {
        #region Fields (2)

        private readonly WcfHttpServer _SERVER;
        private readonly MessageEncoder _WEB_ENCODER = CreateWebMessageBindingEncoder().CreateMessageEncoderFactory()
                                                                                       .Encoder;

        #endregion Fields

        #region Constructors (1)

        internal WcfHttpServerService(WcfHttpServer server)
        {
            this._SERVER = server;
        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (1) 

        public Message Request(Message message)
        {
            using (var uncompressedResponse = new MemoryStream())
            {
                var request = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
                var response = new HttpResponseMessageProperty();

                var reqCtx = new HttpRequest();
                var respCtx = new HttpResponse();
                this._SERVER.RaiseHandleRequest(reqCtx, respCtx);

                // HTTP-Methode: bspw. GET oder POST
                var method = request.Method;

                // Kopfdaten der Anfrage
                var requestHeaders = new Dictionary<string, string>();
                foreach (var key in request.Headers.AllKeys)
                {
                    requestHeaders[key] = request.Headers[key];
                }

                // Rohdaten der Anfrage (nur Body) ermitteln
                byte[] requestBody;
                using (var requestStream = new MemoryStream())
                {
                    this._WEB_ENCODER.WriteMessage(message, requestStream);

                    requestBody = requestStream.ToArray();
                }



                // Beispiel: Antwort definieren
                byte[] responseData;
                {
                    // eigene Kopfdaten definieren
                    {
                        //TODO: Dictionary füllen
                        var responseHeaders = new Dictionary<string, string>();

                        foreach (var item in responseHeaders)
                        {
                            response.Headers[item.Key] = item.Value;
                        }
                    }

                    // Beispiel HTML-Ausgabe
                    {
                        var html = new StringBuilder().Append("<html>")
                                                      .Append("<body>")
                                                      .AppendFormat("Hallo, es ist: {0}",
                                                                    DateTimeOffset.Now)
                                                      .Append("</body>")
                                                      .Append("</html>");

                        var utf8Html = Encoding.UTF8
                                               .GetBytes(html.ToString());

                        uncompressedResponse.Write(utf8Html, 0, utf8Html.Length);

                        response.Headers[HttpResponseHeader.ContentType]
                            = "text/html; charset=utf-8";
                    }

                    // komprimieren?
                    var compress = true;
                    if (compress)
                    {
                        // mit GZIP komprimieren

                        using (var compressedResponse = new MemoryStream())
                        {
                            using (var gzip = new GZipStream(compressedResponse,
                                                             CompressionMode.Compress))
                            {
                                long oldPos = uncompressedResponse.Position;
                                try
                                {
                                    uncompressedResponse.Position = 0;

                                    uncompressedResponse.CopyTo(gzip);
                                }
                                finally
                                {
                                    uncompressedResponse.Position = oldPos;
                                }

                                gzip.Flush();
                                gzip.Close();

                                responseData = compressedResponse.ToArray();
                            }
                        }

                        response.Headers[HttpResponseHeader.ContentEncoding] = "gzip";
                    }
                    else
                    {
                        responseData = uncompressedResponse.ToArray();
                    }
                }

                // HTTP-Status Code (hier: 200)
                response.StatusCode = HttpStatusCode.OK;

                // WCF-Antwort erstellen
                var responseMessage = new BinaryMessage(responseData);
                responseMessage.Properties[HttpResponseMessageProperty.Name] = response;

                return responseMessage;
            }
        }
        // Internal Methods (1) 

        internal static WebMessageEncodingBindingElement CreateWebMessageBindingEncoder()
        {
            var encoding = new WebMessageEncodingBindingElement();
            encoding.MaxReadPoolSize = int.MaxValue;
            encoding.ContentTypeMapper = new RawContentTypeMapper();
            encoding.ReaderQuotas.MaxArrayLength = int.MaxValue;

            return encoding;
        }

        #endregion Methods
    }
}
