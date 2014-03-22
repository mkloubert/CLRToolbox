// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO
{
    /// <summary>
    /// Describes a file system.
    /// </summary>
    public interface IFileSystem : ITMObject
    {
        #region Operations (1)

        /// <summary>
        /// Returns all drives that are managed by that file system.
        /// </summary>
        /// <returns>The list of drives.</returns>
        IEnumerable<IDrive> GetDrives();

        #endregion Operations
    }
}
