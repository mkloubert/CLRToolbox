using MarcelJoachimKloubert.CryptCommander.Forms;
using System;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CryptCommander
{
    internal static class Program
    {
        #region Methods (1)

        // Private Methods (1) 

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        #endregion Methods
    }
}
