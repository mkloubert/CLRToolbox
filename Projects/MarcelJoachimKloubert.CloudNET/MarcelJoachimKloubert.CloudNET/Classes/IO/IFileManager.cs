// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CloudNET.Classes.IO
{
    /// <summary>
    /// Describes a file manager.
    /// </summary>
    public interface IFileManager : ITMObject
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the unique object for thread safe operations.
        /// </summary>
        object SyncRoot { get; }

        #endregion Data Members

        #region Operations (3)

        /// <summary>
        /// Tries to return a directory by its path.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="createIfNotExist">Create directory if it does not exist.</param>
        /// <returns>The directory or <see langword="null" /> if not found.</returns>
        IDirectory GetDirectory(IEnumerable<char> path, bool createIfNotExist = false);

        /// <summary>
        /// Tries to return a file by its path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="createDirIfNotExist">Create underlying directory if it does not exist.</param>
        /// <returns>The file or <see langword="null" /> if not found.</returns>
        IFile GetFile(IEnumerable<char> path, bool createDirIfNotExist = false);

        /// <summary>
        /// Returns the root directory.
        /// </summary>
        /// <returns>The root directory.</returns>
        IDirectory GetRootDirectory();

        #endregion Operations
    }
}
