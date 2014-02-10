// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes._Impl.IO;
using MarcelJoachimKloubert.CloudNET.Classes.IO;
using MarcelJoachimKloubert.CloudNET.Classes.Security;
using MarcelJoachimKloubert.CLRToolbox.Security.AccessControl;
using System.Security.Principal;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.Security
{
    internal sealed class CloudPrincipal : AclPrincipalBase, ICloudPrincipal
    {
        #region Fields (1)

        private readonly ICloudIdentity _ID;

        #endregion Fields

        #region Constructors (1)

        internal CloudPrincipal(ICloudIdentity id, IAccessControlList acl)
            : base(acl)
        {
            this._ID = id;
        }

        #endregion Constructors

        #region Properties (4)

        internal CloudPrincipalFileManager Files
        {
            get;
            set;
        }

        IFileManager ICloudPrincipal.Files
        {
            get { return this.Files; }
        }

        ICloudIdentity ICloudPrincipal.Identity
        {
            get { return (ICloudIdentity)this.Identity; }
        }

        public override IIdentity Identity
        {
            get { return this._ID; }
        }

        #endregion Properties
    }
}
