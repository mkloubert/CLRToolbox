// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CryptoCopy.Helpers
{
    internal static class FileSystemHelper
    {
        #region Methods (4)

        // Internal Methods (4) 

        internal static bool TrySetCreationTimeUtc(FileSystemInfo fs,
                                                   DateTimeOffset time)
        {
            try
            {
                fs.CreationTimeUtc = time.UtcDateTime;
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static bool TrySetLastAccessTimeUtc(FileSystemInfo fs,
                                                     DateTimeOffset time)
        {
            try
            {
                fs.LastAccessTimeUtc = time.UtcDateTime;
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static bool TrySetLastWriteTimeUtc(FileSystemInfo fs,
                                                    DateTimeOffset time)
        {
            try
            {
                fs.LastWriteTimeUtc = time.UtcDateTime;
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static void TrySetTimestampsUtc(FileSystemInfo fs,
                                                 DateTimeOffset time)
        {
            TrySetLastWriteTimeUtc(fs, time);
            TrySetCreationTimeUtc(fs, time);
            TrySetLastAccessTimeUtc(fs, time);
        }

        #endregion Methods
    }
}
