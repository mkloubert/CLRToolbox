// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
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
        /// <see cref="WinFormsHelper.MakeMoveable(Form)" />
        public static void MakeMoveable(this Form frm)
        {
            WinFormsHelper.MakeMoveable(frm);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.MakeMoveable(Form, Func{Control, bool})" />
        public static void MakeMoveable(this Form frm, Func<Control, bool> filter)
        {
            WinFormsHelper.MakeMoveable(frm, filter);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.MakeMoveable(Form, Action{IList{Control}})" />
        public static void MakeMoveable(this Form frm, Action<IList<Control>> setupControlForMove)
        {
            WinFormsHelper.MakeMoveable(frm, setupControlForMove);
        }

        #endregion Methods
    }
}