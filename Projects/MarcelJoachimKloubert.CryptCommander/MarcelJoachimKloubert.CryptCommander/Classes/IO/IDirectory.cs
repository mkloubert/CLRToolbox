// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO
{
    /// <summary>
    /// Descrives a directory inside an <see cref="IDrive" />.
    /// </summary>
    public interface IDirectory : IHasName
    {
        #region Data Members (4)

        /// <summary>
        /// Gets the underlying drive.
        /// </summary>
        IDrive Drive { get; }

        /// <summary>
        /// Gets the full path of that directory.
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Gets the UTC time of the last write operation.
        /// </summary>
        DateTime LastWriteTime { get; }

        /// <summary>
        /// Gets the parent directory or <see langword="null" /> if that directory has no parent.
        /// </summary>
        IDirectory Parent { get; }

        #endregion Data Members

        #region Operations (4)

        /// <summary>
        /// Returns the list of sub directories.
        /// </summary>
        /// <returns>The list of sub directories.</returns>
        IEnumerable<IDirectory> GetDirectories();

        /// <summary>
        /// Returns the list of files.
        /// </summary>
        /// <returns>The list of files.</returns>
        IEnumerable<IFile> GetFiles();

        /// <summary>
        /// Gets the icon of that directory.
        /// </summary>
        /// <returns>The icon of that directory.</returns>
        Image GetIcon();

        /// <summary>
        /// Refreshes that object.
        /// </summary>
        /// <returns>Object was refresh or not.</returns>
        bool Refresh();

        #endregion Operations
    }
}
