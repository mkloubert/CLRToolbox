// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.ServiceModel;
using System.ServiceModel.Channels;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

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

        #region Methods (4)

        // Public Methods (1) 

        public Message Request(Message message)
        {
            var now = this._SERVER.ApplicationServer.Context.Now;

            var request = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
            var reqCtx = new HttpRequest(now, request, message, this._SERVER);

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

            byte[] responseData;
            Stream uncompressedResponse = null;
            try
            {
                var response = new HttpResponseMessageProperty();
                var respCtx = new HttpResponse(response, uncompressedResponse = new MemoryStream())
                    {
                        Compress = false,
                        DocumentNotFound = false,
                        IsForbidden = isRequestInvalid,
                        StatusCode = HttpStatusCode.OK,
                    };

                try
                {
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
                }
                finally
                {
                    // keep sure to update response stream
                    uncompressedResponse = respCtx.Stream;
                }

                // build response...
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

                    if (uncompressedResponse != null)
                    {
                        // compress?
                        var compress = respCtx.Compress ?? false;
                        if (compress)
                        {
                            // compress with GZIP
                            if (uncompressedResponse.CanSeek)
                            {
                                uncompressedResponse.Position = 0;
                            }

                            responseData = uncompressedResponse.GZip();
                            response.Headers[HttpResponseHeader.ContentEncoding] = "gzip";
                        }
                        else
                        {
                            responseData = ToArray(uncompressedResponse);
                        }
                    }
                    else
                    {
                        responseData = new byte[0];
                    }
                }

                response.StatusCode = respCtx.StatusCode;
                if (responseData.Length < 1)
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
            finally
            {
                responseData = null;

                if (uncompressedResponse != null)
                {
                    uncompressedResponse.Dispose();
                }
            }
        }
        // Private Methods (2) 

        private void HandlerForbiddenError(HttpRequest req, HttpResponse resp)
        {
            resp.StatusCode = HttpStatusCode.Forbidden;

            if (!this._SERVER.RaiseHandleForbidden(req, resp))
            {
                resp.Clear();
                resp.Compress = false;
            }
        }

        private static byte[] ToArray(Stream stream)
        {
            var ms = stream as MemoryStream;
            if (ms != null)
            {
                // no need to convert
                return ms.ToArray();
            }
            else
            {
                int? capacity;
                try
                {
                    capacity = (int)stream.Length;
                    if (capacity < 1)
                    {
                        capacity = null;
                    }
                }
                catch
                {
                    capacity = null;
                }

                using (ms = (capacity.HasValue ? new MemoryStream(capacity.Value + 1024) : new MemoryStream()))
                {
                    long? oldPos = null;
                    try
                    {
                        if (stream.CanSeek)
                        {
                            // mark current position to restore later
                            oldPos = stream.Position;
                        }

                        stream.CopyTo(ms);
                    }
                    finally
                    {
                        if (oldPos.HasValue)
                        {
                            // try to restore old position
                            stream.Position = oldPos.Value;
                        }
                    }

                    return ms.ToArray();
                }
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
