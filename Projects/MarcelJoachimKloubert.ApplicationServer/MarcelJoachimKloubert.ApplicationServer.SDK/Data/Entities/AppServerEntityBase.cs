// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Data.Entities;

namespace MarcelJoachimKloubert.ApplicationServer.Data.Entities
{
    /// <summary>
    /// A basic entity of the application server.
    /// </summary>
    public abstract class AppServerEntityBase : EntityBase,
                                                IAppServerEntity
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerEntityBase" /> class.
        /// </summary>
        protected AppServerEntityBase()
        {

        }

        #endregion Constructors
    }
}
