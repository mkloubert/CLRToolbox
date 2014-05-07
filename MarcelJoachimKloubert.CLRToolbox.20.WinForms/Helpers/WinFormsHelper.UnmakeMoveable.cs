// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class WinFormsHelper
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Removes the feature from controls to handle the move of a form.
        /// </summary>
        /// <param name="frm">The form that contains the controls.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="frm" /> is <see langword="null" />.
        /// </exception>
        public static void UnmakeMoveable(Form frm)
        {
            if (frm == null)
            {
                throw new ArgumentNullException("frm");
            }

            UnmakeMoveable(frm.Controls);
        }

        /// <summary>
        /// Removes the feature from controls to handle the move of a form.
        /// </summary>
        /// <param name="ctrls">The controls.</param>
        public static void UnmakeMoveable(Control.ControlCollection ctrls)
        {
            IEnumerable allControls = ctrls ?? (IEnumerable)CollectionHelper.Empty<Control>();

            UnmakeMoveable(CollectionHelper.Cast<Control>(allControls));
        }
        
        /// <summary>
        /// Removes the feature from controls to handle the move of a form.
        /// </summary>
        /// <param name="ctrls">The controls.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrls" /> is <see langword="null" />.
        /// </exception>
        public static void UnmakeMoveable(IEnumerable<Control> ctrls)
        {
            if (ctrls == null)
            {
                throw new ArgumentNullException("ctrls");
            }

            foreach (Control c in ctrls)
            {
                if (c == null)
                {
                    continue;
                }

                c.MouseDown -= Control_MakeMoveable_MouseDown;
            }
        }

        #endregion Methods
    }
}