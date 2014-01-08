using System;
using System.IO;
using System.Windows.Forms;
using MarcelJoachimKloubert.MusicSorter.Forms;
using Vlc.DotNet.Core;

namespace MarcelJoachimKloubert.MusicSorter
{
    internal static class Program
    {
        #region Methods (2)

        // Private Methods (2) 

        public static void InitVLC()
        {
            var appDir = new DirectoryInfo(Environment.CurrentDirectory);
            var vlcDir = new DirectoryInfo(Path.Combine(appDir.FullName,
                                                        "vlc"));

            VlcContext.LibVlcDllsPath = vlcDir.FullName;
            VlcContext.LibVlcPluginsPath = Path.Combine(vlcDir.FullName,
                                                        "plugins");

            VlcContext.StartupOptions.IgnoreConfig = true;
            VlcContext.StartupOptions.LogOptions.LogInFile = false;
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = false;
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Warnings;

            VlcContext.Initialize();
        }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitVLC();

            Application.Run(new MainForm());

            VlcContext.CloseAll(); 
        }

        #endregion Methods
    }
}
