// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.MetalVZ.Classes
{
    /// <inheriteddoc />
    public abstract class MVZDisposableBase : DisposableBase, IMVZObject
    {
        #region Constructors (2)

        /// <inheriteddoc />
        protected MVZDisposableBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <inheriteddoc />
        protected MVZDisposableBase()
            : base()
        {

        }

        #endregion Constructors
    }
}
