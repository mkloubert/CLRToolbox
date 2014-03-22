// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System;
using System.Drawing;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO
{
    /// <summary>
    /// Describes a file.
    /// </summary>
    public interface IFile : IHasName
    {
        #region Data Members (4)

        /// <summary>
        /// Gets the underlying directory.
        /// </summary>
        IDirectory Directory { get; }

        /// <summary>
        /// Gets the full path of that file.
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Gets the UTC time of the last write operation.
        /// </summary>
        DateTime LastWriteTime { get; }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        long Size { get; }

        #endregion Data Members

        #region Operations (2)

        /// <summary>
        /// Gets the icon of that file.
        /// </summary>
        /// <returns>The icon of that file.</returns>
        Image GetIcon();

        /// <summary>
        /// Refreshes that object.
        /// </summary>
        /// <returns>Object was refresh or not.</returns>
        bool Refresh();

        #endregion Operations
    }
}
