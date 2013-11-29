// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox.IO;

namespace MarcelJoachimKloubert.ApplicationServer.TestHost
{
    internal static class Program
    {
        #region Methods (1)

        // Private Methods (1) 

        private static int Main(string[] args)
        {
            try
            {
                using (var server = new ApplicationServer())
                {
                    GlobalConsole.Current.Write("Starting server... ");
                    server.Start();
                    GlobalConsole.Current.WriteLine("[OK]");

                    GlobalConsole.Current.WriteLine();
                    GlobalConsole.Current.WriteLine("===== ENTER to quit... =====");
                    GlobalConsole.Current.ReadLine();

                    GlobalConsole.Current.Write("Shutting down server... ");
                }
                GlobalConsole.Current.WriteLine("[OK]");

                return 0;
            }
            catch (Exception ex)
            {
                GlobalConsole.Current
                             .WriteLine(ex.GetBaseException() ?? ex);

                return 1;
            }
        }

        #endregion Methods
    }
}
