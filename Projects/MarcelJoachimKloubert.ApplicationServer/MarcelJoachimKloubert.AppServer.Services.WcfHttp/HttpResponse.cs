// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Serialization;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    internal sealed class HttpResponse : HttpResponseBase
    {
        #region Fields (4)

        private readonly IDictionary<string, object> _FRONTEND_VARS = new ConcurrentDictionary<string, object>();
        private readonly IDictionary<string, string> _HEADERS = new ConcurrentDictionary<string, string>();
        private readonly HttpResponseMessageProperty _PROPERTY;
        private readonly ISerializer _SERIALIZER;

        #endregion Fields

        #region Constructors (1)

        internal HttpResponse(HttpResponseMessageProperty property,
                              ISerializer serializer,
                              Stream stream)
        {
            this._PROPERTY = property;
            this._SERIALIZER = serializer;

            this.OnSetStream(stream: stream,
                             disposeOld: true);
        }

        #endregion Constructors

        #region Properties (3)

        public override bool CanSetStreamCapacity
        {
            get { return this.Stream is MemoryStream; }
        }

        public override IDictionary<string, object> FrontendVars
        {
            get { return this._FRONTEND_VARS; }
        }

        public override IDictionary<string, string> Headers
        {
            get { return this._HEADERS; }
        }

        #endregion Properties

        #region Methods (2)

        // Protected Methods (2) 

        protected override void OnSetStreamCapacity(int? capacity)
        {
            this.OnSetStream(stream: capacity.HasValue ? new MemoryStream(capacity.Value)
                                                       : new MemoryStream(),
                             disposeOld: true);
        }

        protected override void OnWriteJson<T>(T obj, ref StringBuilder json)
        {
            json.Append(this._SERIALIZER
                            .ToJson<T>(obj));
        }

        #endregion Methods
    }
}
