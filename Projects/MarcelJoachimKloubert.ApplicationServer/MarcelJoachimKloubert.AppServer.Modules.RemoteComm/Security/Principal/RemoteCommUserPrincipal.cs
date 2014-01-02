// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox.Security.AccessControl;
using RemoteCommEntities = MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm
{
    internal sealed class RemoteCommUserPrincipal : AclPrincipalBase
    {
        #region Fields (2)

        private readonly IIdentity _IDENTITY;
        private readonly RemoteCommEntities.REMCOMM_Users _USER;

        #endregion Fields

        #region Constructors (1)

        internal RemoteCommUserPrincipal(RemoteCommEntities.REMCOMM_Users user, IIdentity id)
            : base(new SimpleAcl())
        {
            this._USER = user;
            this._IDENTITY = id;
        }

        #endregion Constructors

        #region Properties (2)

        public override IIdentity Identity
        {
            get { return this._IDENTITY; }
        }

        internal RemoteCommEntities.REMCOMM_Users User
        {
            get { return this._USER; }
        }

        #endregion Properties
    }
}
