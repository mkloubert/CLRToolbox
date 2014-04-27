// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    /// <summary>
    /// Helper class for Windows Forms operations.
    /// </summary>
    public static partial class WinFormsHelper
    {
        #region Fields (2)

        private const int _HTCAPTION = 0x2;
        private const int _WM_NCLBUTTONDOWN = 0xA1;

        #endregion Fields

        #region Methods (2)

        // Private Methods (2) 

        private static void Control_MakeMoveable_MouseDown(object sender, MouseEventArgs e)
        {
            Control ctrl = (Control)sender;
            
            Form frm = ctrl.FindForm();
            if (frm == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();

                SendMessage(ctrl,
                            frm.Handle,
                            _WM_NCLBUTTONDOWN,
                            _HTCAPTION,
                            0);
            }
        }

        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        #endregion Methods
    }
}