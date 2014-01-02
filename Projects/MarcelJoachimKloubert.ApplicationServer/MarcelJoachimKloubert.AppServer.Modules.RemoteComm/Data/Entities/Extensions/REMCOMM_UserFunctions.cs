// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService
{
    partial class REMCOMM_UserFunctions
    {
        #region Properties (1)

        /// <summary>
        /// Gets or sets the underlying <see cref="ServerEntities.General.Functions.ServerFunctions" /> item.
        /// </summary>
        public ServerEntities.General.Functions.ServerFunctions ServerFunctions
        {
            get;
            set;
        }

        #endregion Properties
    }
}
