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
        /// Adds the feature to all controls of a form to handle moving of that form by dragging its content or its controls.
        /// </summary>
        /// <param name="frm">The form that contains the controls.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="frm" /> is <see langword="null" />.
        /// </exception>
        public static void MakeMoveable(Form frm)
        {
            if (frm == null)
            {
                throw new ArgumentNullException("frm");
            }

            MakeMoveable(frm,
                         delegate(IList<Control> controlsThatHandleFormMove)
                         {
                             IEnumerable allControls = frm.Controls ?? (IEnumerable)CollectionHelper.Empty<Control>();

                             CollectionHelper.AddRange(controlsThatHandleFormMove,
                                                       CollectionHelper.OfType<Control>(allControls));
                         });
        }

        /// <summary>
        /// Adds the feature to all controls of a form to handle moving of that form by dragging its content or its controls.
        /// </summary>
        /// <param name="frm">The form that contains the controls.</param>
        /// <param name="filter">
        /// The filter to use for each element of the <see cref="Control.Controls" />
        /// collection of <paramref name="frm" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="frm" /> is <see langword="null" />.
        /// </exception>
        public static void MakeMoveable(Form frm, Func<Control, bool> filter)
        {
            if (frm == null)
            {
                throw new ArgumentNullException("frm");
            }

            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            MakeMoveable(frm,
                         delegate(IList<Control> controlsThatHandleFormMove)
                         {
                             IEnumerable allControls = frm.Controls ?? (IEnumerable)CollectionHelper.Empty<Control>();
                             IEnumerable<Control> castedControls = CollectionHelper.OfType<Control>(allControls);

                             CollectionHelper.AddRange(controlsThatHandleFormMove,
                                                       CollectionHelper.Where(castedControls, filter));
                         });
        }

        /// <summary>
        /// Adds the feature to all controls of a form to handle moving of that form by dragging its content or its controls.
        /// </summary>
        /// <param name="frm">The form that contains the controls.</param>
        /// <param name="setupControlForMove">
        /// The action that is invoked to define what controls should be setupped.
        /// If <see langword="null" /> nothing is done.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="frm" /> is <see langword="null" />.
        /// </exception>
        public static void MakeMoveable(Form frm, Action<IList<Control>> setupControlForMove)
        {
            if (frm == null)
            {
                throw new ArgumentNullException("frm");
            }

            if (setupControlForMove != null)
            {
                List<Control> controlsThatHandleFormMove = new List<Control>();
                setupControlForMove(controlsThatHandleFormMove);

                foreach (Control c in controlsThatHandleFormMove)
                {
                    if (c == null)
                    {
                        continue;
                    }

                    c.MouseDown += Control_MakeMoveable_MouseDown;
                }
            }
        }

        #endregion Methods
    }
}