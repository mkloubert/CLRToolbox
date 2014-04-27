// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.WinForms.Controls
{
    /// <summary>
    /// A borderless but moveable / draggable form.
    /// </summary>
    public class BorderlessMoveableForm : Form
    {
        #region Fields (3)

        private const int _HTCAPTION = 0x2;
        private const int _HTCLIENT = 0x1;
        private const int _WM_NCHITTEST = 0x84;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="BorderlessMoveableForm" /> class.
        /// </summary>
        public BorderlessMoveableForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        #endregion Constructors

        #region Methods (4)

        // Protected Methods (4) 

        /// <summary>
        /// Is invoked for the <see cref="Form.Load" /> event.
        /// </summary>
        protected virtual void OnLoad()
        {
            WinFormsHelper.MakeMoveable(this,
                                        this.SetupMoveableForm);
        }

        /// <inheriteddoc />
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.OnLoad();
        }

        /// <summary>
        /// Sets up that form or its controls for handling form move.
        /// </summary>
        /// <param name="controlsThatHandleFormMove">The list that defines what controls should handle the form move.</param>
        protected virtual void SetupMoveableForm(IList<Control> controlsThatHandleFormMove)
        {
            CollectionHelper.AddRange(controlsThatHandleFormMove,
                                      CollectionHelper.OfType<Control>(this.Controls));
        }

        /// <inheriteddoc />
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == _WM_NCHITTEST &&
                (int)message.Result == _HTCLIENT)
            {
                message.Result = (IntPtr)_HTCAPTION;
            }
        }

        #endregion Methods
    }
}