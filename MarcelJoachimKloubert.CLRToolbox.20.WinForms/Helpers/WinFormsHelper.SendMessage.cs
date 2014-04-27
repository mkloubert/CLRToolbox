// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Reflection;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class WinFormsHelper
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="Control.DefWndProc(ref Message)" />
        public static void SendMessage(Control ctrl, ref Message msg)
        {
            if (ctrl == null)
            {
                throw new ArgumentNullException("ctrl");
            }

            Type msgTypeRef = typeof(global::System.Windows.Forms.Message).MakeByRefType();

            // find 'Control.DefWndProc(ref Message)' method
            MethodInfo defWndProcMethod = CollectionHelper.Single(ctrl.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic),
                                                                  delegate(MethodInfo m)
                                                                  {
                                                                      ParameterInfo[] @mp = m.GetParameters();

                                                                      return m.Name == "DefWndProc" &&
                                                                             @mp.Length == 1 &&
                                                                             msgTypeRef.Equals(@mp[0].ParameterType);
                                                                  });

            object[] @params = new object[] { msg };
            defWndProcMethod.Invoke(ctrl, @params);

            msg = (Message)@params[0];
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="Control.DefWndProc(ref Message)" />
        public static Message SendMessage(Control ctrl, IntPtr hWnd, int msg, int wparam, int lparam)
        {
            return SendMessage(ctrl, hWnd, msg, (IntPtr)wparam, (IntPtr)lparam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="Control.DefWndProc(ref Message)" />
        public static Message SendMessage(Control ctrl, int hWnd, int msg, int wparam, int lparam)
        {
            return SendMessage(ctrl, (IntPtr)hWnd, msg, (IntPtr)wparam, (IntPtr)lparam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="Control.DefWndProc(ref Message)" />
        public static Message SendMessage(Control ctrl, IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            Message result = Message.Create(hWnd, msg, wparam, lparam);
            SendMessage(ctrl, ref result);

            return result;
        }

        #endregion Methods
    }
}