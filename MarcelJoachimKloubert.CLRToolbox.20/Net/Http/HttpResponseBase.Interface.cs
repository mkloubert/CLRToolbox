// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    partial class HttpResponseBase
    {
        #region Methods (7)

        // Private Methods (7) 

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

        IHttpResponse IHttpResponse.Write(IEnumerable<byte> data)
        {
            return this.Write(data);
        }

        IHttpResponse IHttpResponse.Write(IEnumerable<char> chars)
        {
            return this.Write(chars);
        }

        #endregion Methods
    }
}
