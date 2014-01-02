// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;
using ServerEntities = MarcelJoachimKloubert.ApplicationServer.DataModels.Entities;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService
{
    partial class REMCOMM_Users
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

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Checks if a function can be executed or not.
        /// </summary>
        /// <param name="func">The function to check.</param>
        /// <returns>Can be executed or not.</returns>
        public bool CanExecuteFunction(IFunction func)
        {
            if (func == null)
            {
                return false;
            }

            return this.REMCOMM_UserFunctions
                       .Select(uf => uf.ServerFunctions)
                       .Any(sf => sf.ServerFunctionExportID == func.Id);
        }

        #endregion Methods
    }
}
