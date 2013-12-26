// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mime;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Prefix,
                     InstanceContextMode = InstanceContextMode.Single,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal sealed class WcfHttpServerService : IWcfHttpServerService
    {
        #region Fields (1)

        private readonly WcfHttpServer _SERVER;

        #endregion Fields

        #region Constructors (1)

        internal WcfHttpServerService(WcfHttpServer server)
        {
            this._SERVER = server;
        }

        #endregion Constructors

        #region Methods (3)

        // Public Methods (1) 

        public Message Request(Message message)
        {
            var request = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
            var reqCtx = new HttpRequest(request, message, this._SERVER);

            var isRequestInvalid = false;

            // check if request data are valid
            // and may be handled
            var reqValidator = this._SERVER.RequestValidator;
            if (reqValidator != null)
            {
                if (!reqValidator(reqCtx))
                {
                    isRequestInvalid = true;
                }
            }

            using (var uncompressedResponse = new MemoryStream())
            {
                var response = new HttpResponseMessageProperty();

                var respCtx = new HttpResponse(response, uncompressedResponse)
                {
                    Compress = false,
                    DocumentNotFound = false,
                    IsForbidden = isRequestInvalid,
                    StatusCode = HttpStatusCode.OK,
                };

                if (!respCtx.IsForbidden)
                {
                    try
                    {
                        if (!this._SERVER.RaiseHandleRequest(reqCtx, respCtx))
                        {
                            // 501
                            respCtx.StatusCode = HttpStatusCode.NotImplemented;

                            respCtx.Clear();
                            respCtx.Compress = false;
                        }
                        else
                        {
                            if (respCtx.IsForbidden)
                            {
                                // 403
                                this.HandlerForbiddenError(reqCtx, respCtx);
                            }
                            else if (respCtx.DocumentNotFound)
                            {
                                // 400
                                respCtx.StatusCode = HttpStatusCode.NotFound;

                                if (!this._SERVER.RaiseHandleDocumentNotFound(reqCtx, respCtx))
                                {
                                    respCtx.Clear();
                                    respCtx.Compress = false;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // 500
                        respCtx.StatusCode = HttpStatusCode.InternalServerError;

                        if (!this._SERVER.RaiseHandleError(reqCtx, respCtx, ex))
                        {
                            respCtx.Clear();
                            respCtx.Compress = false;
                        }
                    }
                }
                else
                {
                    // 403
                    this.HandlerForbiddenError(reqCtx, respCtx);
                }

                // build response...
                byte[] responseData;
                {
                    // response headers
                    foreach (var item in respCtx.Headers)
                    {
                        response.Headers[item.Key] = item.Value;
                    }

                    // mime type
                    {
                        var contentType = (respCtx.ContentType ?? string.Empty).ToLower().Trim();
                        if (contentType == string.Empty)
                        {
                            contentType = MediaTypeNames.Application.Octet;
                        }

                        // append charset?
                        var charset = respCtx.Charset;
                        if (charset != null)
                        {
                            contentType += "; charset=" + charset.WebName;
                        }

                        response.Headers[HttpResponseHeader.ContentType] = contentType;
                    }

                    // compress?
                    var compress = respCtx.Compress;
                    if (compress)
                    {
                        // compress with GZIP

                        using (var compressedResponse = new MemoryStream())
                        {
                            using (var gzip = new GZipStream(compressedResponse,
                                                             CompressionMode.Compress))
                            {
                                var oldPos = uncompressedResponse.Position;
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

                response.StatusCode = respCtx.StatusCode;
                if (responseData.Length == 0)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            response.StatusCode = HttpStatusCode.NoContent;
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(respCtx.StatusDescription))
                {
                    response.StatusDescription = respCtx.StatusDescription.Trim();
                }

                // create WCF answer
                var responseMsg = new BinaryMessage(responseData);
                responseMsg.Properties[HttpResponseMessageProperty.Name] = response;

                return responseMsg;
            }
        }
        // Private Methods (1) 

        private void HandlerForbiddenError(HttpRequest req, HttpResponse resp)
        {
            resp.StatusCode = HttpStatusCode.Forbidden;

            if (!this._SERVER.RaiseHandleForbidden(req, resp))
            {
                resp.Clear();
                resp.Compress = false;
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
