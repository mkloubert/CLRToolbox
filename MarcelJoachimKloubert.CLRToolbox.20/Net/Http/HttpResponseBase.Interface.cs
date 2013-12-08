// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    partial class HttpResponseBase
    {
        #region Methods (3)

        // Private Methods (3) 

        IHttpResponse IHttpResponse.Clear()
        {
            return this.Clear();
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
