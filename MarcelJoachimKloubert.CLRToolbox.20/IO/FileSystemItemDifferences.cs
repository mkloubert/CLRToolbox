// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// List of differences between two file system items.
    /// </summary>
    [Flags]
    public enum FileSystemItemDifferences
    {
        /// <summary>
        /// No differences.
        /// </summary>
        None = 0,

        /// <summary>
        /// Source directory does not exist.
        /// </summary>
        SourceDoesNotExist = 1,

        /// <summary>
        /// Destination directory does not exist.
        /// </summary>
        DestionationDoesNotExist = 2,

        /// <summary>
        /// Exists in source but NOT in destination.
        /// </summary>
        IsMissing = 4,

        /// <summary>
        /// Exists in destination but NOT in source.
        /// </summary>
        IsExtra = 8,

        /// <summary>
        /// Last write time.
        /// </summary>
        LastWriteTime = 16,

        /// <summary>
        /// (File) Size.
        /// </summary>
        Size = 32,

        /// <summary>
        /// Content
        /// </summary>
        Content = 64,
    }
}
