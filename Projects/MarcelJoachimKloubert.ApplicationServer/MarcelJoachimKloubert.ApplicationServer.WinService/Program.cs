// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace MarcelJoachimKloubert.ApplicationServer.WinService
{
    internal static class Program
    {
        #region Methods (1)

        // Private Methods (1) 

        private static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                // from command line

                if (args.Length > 0)
                {
                    var exeFile = Assembly.GetExecutingAssembly().Location;

                    switch (args[0].ToLower().Trim())
                    {
                        case "/i":
                            // install
                            ManagedInstallerClass.InstallHelper(new string[] { exeFile });
                            break;

                        case "/u":
                            // uninstall
                            ManagedInstallerClass.InstallHelper(new string[] { "/u", exeFile });
                            break;
                    }
                }
            }
            else
            {
                // runs in dedicated service mode

                ServiceBase.Run(new ServiceBase[]
                    {
                        new MainService(),
                    });
            }
        }

        #endregion Methods
    }
}
