// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    internal sealed class HttpResponse : HttpResponseBase
    {
        #region Fields (3)

        private readonly IDictionary<string, string> _HEADERS = new ConcurrentDictionary<string, string>();
        private readonly HttpResponseMessageProperty _PROPERTY;
        private readonly Stream _STREAM;

        #endregion Fields

        #region Constructors (1)

        internal HttpResponse(HttpResponseMessageProperty property,
                              Stream stream)
        {
            this._PROPERTY = property;
            this._STREAM = stream;
        }

        #endregion Constructors

        #region Properties (2)

        public override IDictionary<string, string> Headers
        {
            get { return this._HEADERS; }
        }

        public override Stream Stream
        {
            get { return this._STREAM; }
        }

        #endregion Properties
    }
}
