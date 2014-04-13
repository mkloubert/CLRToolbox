// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Arguments for events that compare two <see cref="FileSystemInfo" /> object.
    /// </summary>
    public sealed class CompareFileSystemItemsEventArgs : EventArgs
    {
        #region Fields (3)

        private readonly FileSystemInfo _DESTINATION;
        private FileSystemItemDifferences _differences;
        private readonly FileSystemInfo _SOURCE;
        private bool _handled;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="CompareFileSystemItemsEventArgs" /> class.
        /// </summary>
        /// <param name="src">The value for the <see cref="CompareFileSystemItemsEventArgs.Source" /> property.</param>
        /// <param name="dest">The value for the <see cref="CompareFileSystemItemsEventArgs.Destination" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public CompareFileSystemItemsEventArgs(FileSystemInfo src, FileSystemInfo dest)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (dest == null)
            {
                throw new ArgumentNullException("dest");
            }

            this._SOURCE = src;
            this._DESTINATION = dest;
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
        /// Gets or sets the differences between <see cref="CompareFileSystemItemsEventArgs.Source" />
        /// and <see cref="CompareFileSystemItemsEventArgs.Destination" />.
        /// </summary>
        public FileSystemItemDifferences Differences
        {
            get { return this._differences; }

            set { this._differences = value; }
        }

        /// <summary>
        /// Gets or if the comparison operation has been handled or not.
        /// </summary>
        public bool Handled
        {
            get { return this._handled; }

            set { this._handled = value; }
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
