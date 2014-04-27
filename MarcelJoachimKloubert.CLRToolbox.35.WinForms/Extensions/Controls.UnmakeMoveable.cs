// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms
{
    static partial class ClrToolboxWinFormsExtensionMethods
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.UnmakeMoveable(Form)" />
        public static void UnmakeMoveable(this Form frm)
        {
            WinFormsHelper.UnmakeMoveable(frm);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.UnmakeMoveable(Control.ControlCollection)" />
        public static void UnmakeMoveable(this Control.ControlCollection ctrls)
        {
            WinFormsHelper.UnmakeMoveable(ctrls);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.UnmakeMoveable(IEnumerable{Control})" />
        public static void UnmakeMoveable(this IEnumerable<Control> ctrls)
        {
            WinFormsHelper.UnmakeMoveable(ctrls);
        }

        #endregion Methods
    }
}