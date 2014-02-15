// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Data.Entities;

namespace MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities
{
    /// <summary>
    /// A basic MetalVZ entity.
    /// </summary>
    public abstract class MVZEntityBase : EntityBase, IMVZEntity
    {
        #region Constructors (1)

        /// <inheriteddoc />
        protected MVZEntityBase()
        {

        }

        #endregion Constructors
    }
}
