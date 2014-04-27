// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms
{
    static partial class ClrToolboxWinFormsExtensionMethods
    {
        #region Methods (4) 

        // Public Methods (4) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.SendMessage(Control, ref Message)" />
        public static void SendMessage(this Control ctrl, ref Message msg)
        {
            WinFormsHelper.SendMessage(ctrl, ref msg);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.SendMessage(Control, IntPtr, int, int, int)" />
        public static Message SendMessage(this Control ctrl, IntPtr hWnd, int msg, int wparam, int lparam)
        {
            return WinFormsHelper.SendMessage(ctrl, hWnd, msg, wparam, lparam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.SendMessage(Control, int, int, int, int)" />
        public static Message SendMessage(this Control ctrl, int hWnd, int msg, int wparam, int lparam)
        {
            return WinFormsHelper.SendMessage(ctrl, hWnd, msg, wparam, lparam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="WinFormsHelper.SendMessage(Control, IntPtr, int, IntPtr, IntPtr)" />
        public static Message SendMessage(this Control ctrl, IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            return WinFormsHelper.SendMessage(ctrl, hWnd, msg, wparam, lparam);
        }

        #endregion Methods 
    }
}