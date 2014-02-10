// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;

namespace MarcelJoachimKloubert.CloudNET.SDK.Helpers
{
    internal static class IOHelper
    {
        #region Methods (2)

        // Internal Methods (2) 

        internal static void CopyTo(Stream src, Stream dest)
        {
            CopyTo(src, dest, 81920);
        }

        internal static void CopyTo(Stream src, Stream dest, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            while ((bytesRead = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, bytesRead);
            }
        }

        #endregion Methods
    }
}
