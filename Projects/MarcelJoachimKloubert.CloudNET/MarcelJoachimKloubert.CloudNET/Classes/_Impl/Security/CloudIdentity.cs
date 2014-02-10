// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Security;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.Security
{
    internal sealed class CloudIdentity : ICloudIdentity
    {
        #region Properties (3)

        public string AuthenticationType
        {
            get;
            internal set;
        }

        public bool IsAuthenticated
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            internal set;
        }

        #endregion Properties
    }
}
