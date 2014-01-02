// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox.Security.AccessControl;
using DocDBEntities = MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService;

namespace MarcelJoachimKloubert.AppServer.Modules.DocDB.Security.Principal
{
    internal sealed class DocDBUserPrincipal : AclPrincipalBase
    {
        #region Fields (2)

        private readonly IIdentity _IDENTITY;
        private readonly DocDBEntities.DOCDB_Users _USER;

        #endregion Fields

        #region Constructors (1)

        internal DocDBUserPrincipal(DocDBEntities.DOCDB_Users user, IIdentity id)
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

        internal DocDBEntities.DOCDB_Users User
        {
            get { return this._USER; }
        }

        #endregion Properties
    }
}
