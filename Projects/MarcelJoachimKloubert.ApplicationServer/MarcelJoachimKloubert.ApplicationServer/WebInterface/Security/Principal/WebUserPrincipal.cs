// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox.Security.AccessControl;
using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface.Security.Principal
{
    internal sealed class WebUserPrincipal : AclPrincipalBase
    {
        #region Fields (2)

        private readonly IIdentity _IDENTITY;
        private readonly ServerEntities.WebInterface.WebInterfaceUsers _USER;

        #endregion Fields

        #region Constructors (1)

        internal WebUserPrincipal(ServerEntities.WebInterface.WebInterfaceUsers user, IIdentity id)
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

        internal ServerEntities.WebInterface.WebInterfaceUsers User
        {
            get { return this._USER; }
        }

        #endregion Properties
    }
}
