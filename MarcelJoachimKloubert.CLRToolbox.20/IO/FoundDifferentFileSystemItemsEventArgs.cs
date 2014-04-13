// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Arguments for events that tell that two <see cref="FileSystemInfo" /> objects are different.
    /// </summary>
    public sealed class FoundDifferentFileSystemItemsEventArgs : EventArgs
    {
        #region Fields (3)

        private readonly FileSystemInfo _DESTINATION;
        private readonly FileSystemItemDifferences? _DIFFERENCES;
        private readonly FileSystemInfo _SOURCE;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="FoundDifferentFileSystemItemsEventArgs" /> class.
        /// </summary>
        /// <param name="src">The value for the <see cref="CompareFileSystemItemsEventArgs.Source" /> property.</param>
        /// <param name="dest">The value for the <see cref="CompareFileSystemItemsEventArgs.Destination" /> property.</param>
        /// <param name="diffs">The value for the <see cref="CompareFileSystemItemsEventArgs.Differences" /> property.</param>
        public FoundDifferentFileSystemItemsEventArgs(FileSystemInfo src, FileSystemInfo dest, FileSystemItemDifferences? diffs)
        {
            this._SOURCE = src;
            this._DESTINATION = dest;
            this._DIFFERENCES = diffs;
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the destination object.
        /// </summary>
        public FileSystemInfo Destination
        {
            get { return this._DESTINATION; }
        }

        /// <summary>
        /// Gets the differences between <see cref="CompareFileSystemItemsEventArgs.Source" />
        /// and <see cref="CompareFileSystemItemsEventArgs.Destination" />.
        /// </summary>
        public FileSystemItemDifferences? Differences
        {
            get { return this._DIFFERENCES; }
        }

        /// <summary>
        /// Gets the source object.
        /// </summary>
        public FileSystemInfo Source
        {
            get { return this._SOURCE; }
        }

        #endregion Properties
    }
}
