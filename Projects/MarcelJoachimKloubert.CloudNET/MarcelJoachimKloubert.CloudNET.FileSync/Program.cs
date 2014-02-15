// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.FileSync.Controls.Forms;
using MarcelJoachimKloubert.CloudNET.SDK;
using System;
using System.Net;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CloudNET.FileSync
{
    internal static class Program
    {
        #region Constructors (1)

        static Program()
        {
            ServicePointManager.ServerCertificateValidationCallback = CloudServer.AllowAnySslCertificate;
        }

        #endregion Constructors

        #region Methods (1)

        // Private Methods (1) 

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args));
        }

        #endregion Methods
    }
}
