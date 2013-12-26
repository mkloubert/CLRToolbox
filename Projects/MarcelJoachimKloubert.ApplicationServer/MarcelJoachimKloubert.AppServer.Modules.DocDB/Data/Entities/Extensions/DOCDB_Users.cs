// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities;

namespace MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService
{
    partial class DOCDB_Users
    {
        #region Properties (1)

        /// <summary>
        /// Gets or sets the underlying <see cref="ServerEntities.General.Security.Users" /> item.
        /// </summary>
        public ServerEntities.General.Security.Users Users
        {
            get;
            set;
        }

        #endregion Properties
    }
}
