// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO
{
    /// <summary>
    /// Describes a drive.
    /// </summary>
    public interface IDrive : IHasName
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying file system.
        /// </summary>
        IFileSystem FileSystem { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Returns the root directory of that drive.
        /// </summary>
        /// <returns>The root directory of that drive.</returns>
        IDirectory GetRootDirectory();

        #endregion Operations
    }
}
