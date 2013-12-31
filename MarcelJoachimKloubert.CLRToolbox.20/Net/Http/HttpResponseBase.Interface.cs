// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    partial class HttpResponseBase
    {
        #region Methods (13)

        // Private Methods (13) 

        IHttpResponse IHttpResponse.Append(IEnumerable<byte> data)
        {
            return this.Append(data);
        }

        IHttpResponse IHttpResponse.Append(IEnumerable<char> chars)
        {
            return this.Append(chars);
        }

        IHttpResponse IHttpResponse.Clear()
        {
            return this.Clear();
        }

        IHttpResponse IHttpResponse.Prefix(IEnumerable<byte> data)
        {
            return this.Prefix(data);
        }

        IHttpResponse IHttpResponse.Prefix(IEnumerable<char> chars)
        {
            return this.Prefix(chars);
        }

        IHttpResponse IHttpResponse.SetDefaultStreamCapacity()
        {
            return this.SetDefaultStreamCapacity();
        }

        IHttpResponse IHttpResponse.SetStream(Stream stream)
        {
            return this.SetStream(stream);
        }

        IHttpResponse IHttpResponse.SetStream(Stream stream, bool disposeOld)
        {
            return this.SetStream(stream,
                                  disposeOld);
        }

        IHttpResponse IHttpResponse.SetStreamCapacity(int capacity)
        {
            return this.SetStreamCapacity(capacity);
        }

        IHttpResponse IHttpResponse.Write(object obj)
        {
            return this.Write(obj);
        }

        IHttpResponse IHttpResponse.Write(IEnumerable<byte> data)
        {
            return this.Write(data);
        }

        IHttpResponse IHttpResponse.Write(IEnumerable<char> chars)
        {
            return this.Write(chars);
        }

        IHttpResponse IHttpResponse.Write(object obj, bool handleDBNullAsNull)
        {
            return this.Write(obj,
                              handleDBNullAsNull);
        }

        #endregion Methods
    }
}
