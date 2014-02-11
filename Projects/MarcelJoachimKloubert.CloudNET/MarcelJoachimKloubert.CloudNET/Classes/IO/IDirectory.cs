// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CloudNET.Classes.IO
{
    /// <summary>
    /// Describes a directory.
    /// </summary>
    public interface IDirectory : IFileSystemItem
    {
        #region Data Members (3)

        /// <summary>
        /// Gets if the directory still exists or not.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets if that directory represents the root directory or not.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// Gets the parent directory if available.
        /// </summary>
        IDirectory Parent { get; }

        #endregion Data Members

        #region Operations (5)

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="name">The name of the new directory.</param>
        /// <returns>The new directory.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> is invalid.
        /// </exception>
        IDirectory CreateDirectory(IEnumerable<char> name);

        /// <summary>
        /// Deletes that directory and all its items.
        /// </summary>
        void Delete();

        /// <summary>
        /// Gets the list of sub directories.
        /// </summary>
        /// <returns>The list of sub directories.</returns>
        IEnumerable<IDirectory> GetDirectories();

        /// <summary>
        /// Gets the list of files inside that directory.
        /// </summary>
        /// <returns>The list of files.</returns>
        IEnumerable<IFile> GetFiles();

        /// <summary>
        /// Saves a file in that directory.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <param name="data">The data to save.</param>
        /// <returns>The new file.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> and/or <paramref name="data" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> is invalid and/or <paramref name="data" /> cannot be read.
        /// </exception>
        IFile SaveFile(IEnumerable<char> name, Stream data);

        #endregion Operations
    }
}
