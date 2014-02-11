// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK;
using System.IO;
using System.Net;

namespace MarcelJoachimKloubert.CloudNET.Test
{
    internal static class Program
    {
        #region Methods (1)

        // Private Methods (1) 

        private static void Main(string[] args)
        {
            var server = new CloudServer();
            server.HostAddress = "localhost";
            server.Port = 48109;
            server.Credentials = new NetworkCredential("admin", "admin");

            var rootDir = server.ListRootDirectory();

            using (var stream = File.OpenRead(@"C:\debian-7.0.0-amd64-netinst.iso"))
            {
                rootDir.UploadFile("test.iso", stream);
            }

            rootDir = server.ListRootDirectory();

            using (var stream = File.OpenWrite(@"C:\debian-7.0.0-amd64-netinst.iso"))
            {
                rootDir.Files["test.iso"].Download(stream);
            }
        }

        #endregion Methods
    }
}
