// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Security;
using MarcelJoachimKloubert.CloudNET.Classes.Sessions;
using MarcelJoachimKloubert.CloudNET.Classes.Web;
using MarcelJoachimKloubert.CloudNET.Extensions;
using MarcelJoachimKloubert.CLRToolbox;
using System.Web;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.Web
{
    internal sealed class CloudRequest : TMObject, ICloudRequest
    {
        #region Properties (3)

        public HttpContext Context
        {
            get;
            internal set;
        }

        public ICloudPrincipal Principal
        {
            get;
            internal set;
        }

        public ICloudSession Session
        {
            get { return this.Context.GetCloudSession(); }
        }

        #endregion Properties
    }
}
