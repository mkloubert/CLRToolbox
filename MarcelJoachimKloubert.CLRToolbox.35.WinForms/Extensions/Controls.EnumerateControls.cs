// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms
{
    static partial class ClrToolboxWinFormsExtensionMethods
    {
        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Returns the controls of a WinForms control from its
        /// <see cref="Control.Controls" /> property.
        /// </summary>
        /// <param name="ctrl">The control from where to get the controls from.</param>
        /// <returns>
        /// The controls or <see langword="null" /> if <see cref="Control.Controls" /> property
        /// contains also a <see langword="null" /> reference.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<Control> EnumerateControls(this Control ctrl)
        {
            if (ctrl == null)
            {
                throw new ArgumentNullException("ctrl");
            }

            Control.ControlCollection coll = ctrl.Controls;
            if (coll != null)
            {
                return EnumerateControlsInner(coll);
            }

            return null;
        }

        /// <summary>
        /// Returns the controls of a WinForms control from its
        /// <see cref="Control.Controls" /> property of a specific type.
        /// </summary>
        /// <typeparam name="TCtrl">The filter type.</typeparam>
        /// <param name="ctrl">The control from where to get the controls from.</param>
        /// <returns>
        /// The controls or <see langword="null" /> if <see cref="Control.Controls" /> property
        /// contains also a <see langword="null" /> reference.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TCtrl> EnumerateControls<TCtrl>(this Control ctrl)
            where TCtrl : global::System.Windows.Forms.Control
        {
            IEnumerable<Control> controls = EnumerateControls(ctrl);

            return controls != null ? controls.OfType<TCtrl>() : null;
        }

        // Private Methods (1) 

        private static IEnumerable<Control> EnumerateControlsInner(Control.ControlCollection coll)
        {
            foreach (Control c in coll)
            {
                yield return c;
            }
        }

        #endregion Methods
    }
}