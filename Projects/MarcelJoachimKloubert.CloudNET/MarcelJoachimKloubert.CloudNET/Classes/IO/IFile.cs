// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;

namespace MarcelJoachimKloubert.CloudNET.Classes.IO
{
    /// <summary>
    /// Describes a file.
    /// </summary>
    public interface IFile : IFileSystemItem
    {
        #region Data Members (3)

        /// <summary>
        /// Gets the directory the file is stored in.
        /// </summary>
        IDirectory Directory { get; }

        /// <summary>
        /// Gets if the file exists or not.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        long? Size { get; }

        #endregion Data Members

        #region Operations (2)

        /// <summary>
        /// Deletes the file.
        /// </summary>
        void Delete();

        /// <summary>
        /// Opens that file for reading.
        /// </summary>
        /// <returns>The stream that contains the data of that file.</returns>
        Stream OpenRead();

        #endregion Operations
    }
}
