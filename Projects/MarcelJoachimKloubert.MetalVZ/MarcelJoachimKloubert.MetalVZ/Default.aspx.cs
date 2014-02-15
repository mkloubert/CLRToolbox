// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.MetalVZ.Classes.Data;
using MarcelJoachimKloubert.MetalVZ.Classes.Security.Cryptography;
using MarcelJoachimKloubert.MetalVZ.Classes.Sessions;
using MarcelJoachimKloubert.MetalVZ.Classes.UI;
using MarcelJoachimKloubert.MetalVZ.DataModels.Entities.General;
using System.Linq;

namespace MarcelJoachimKloubert.MetalVZ
{
    /// <summary>
    /// The default page.
    /// </summary>
    public partial class Default : MVZPageBase
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnPageLoad()
        {
            var db = ServiceLocator.Current.GetInstance<IMVZDatabase>();

            var pwd = ServiceLocator.Current.GetInstance<IMVZPasswordHasher>();

            var sessionMgr = ServiceLocator.Current.GetInstance<IMVZSessionManager>();

            var users = db.Query<Users>().ToArray();
        }

        #endregion Methods
    }
}
