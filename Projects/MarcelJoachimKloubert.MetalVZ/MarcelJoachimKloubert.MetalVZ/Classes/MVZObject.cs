// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.MetalVZ.Classes
{
    /// <summary>
    /// The mother of all MetalVZ objects.
    /// </summary>
    public class MVZObject : TMObject, IMVZObject
    {
        #region Constructors (2)

        /// <inheriteddoc />
        public MVZObject(object syncRoot)
            : base(syncRoot: syncRoot)
        {

        }

        /// <inheriteddoc />
        public MVZObject()
            : base()
        {

        }

        #endregion Constructors
    }
}
