// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    partial class HttpServerBase
    {
        #region Methods (1)

        // Private Methods (1) 

        IHttpServer IHttpServer.SetSslCertificateByThumbprint(IEnumerable<char> thumbprint)
        {
            return this.SetSslCertificateByThumbprint(thumbprint);
        }

        #endregion Methods
    }
}
