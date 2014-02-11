// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System;

namespace MarcelJoachimKloubert.CloudNET.Classes.IO
{
    /// <summary>
    /// Describes an item of a file system.
    /// </summary>
    public interface IFileSystemItem : IHasName
    {
        #region Data Members (3)

        /// <summary>
        /// Gets or sets the creation timestamp.
        /// </summary>
        DateTime? CreationTime { get; set; }

        /// <summary>
        /// Returns the full path of that item.
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Gets or sets the write time timestamp.
        /// </summary>
        DateTime? WriteTime { get; set; }

        #endregion Data Members
    }
}
