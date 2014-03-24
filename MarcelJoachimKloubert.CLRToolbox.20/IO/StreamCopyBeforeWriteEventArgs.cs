// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Arguments for an event that is invoked before write process starts.
    /// </summary>
    public sealed class StreamCopyBeforeWriteEventArgs : StreamCopyProgressEventArgs
    {
        #region Fields (2)

        private IEnumerable<byte> _data;
        private bool _skip;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyBeforeWriteEventArgs"/> class.
        /// </summary>
        /// <param name="dataToWrite">The data that should be written.</param>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <param name="totalBytesCopies">The total bytes copies.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="totalBytesCopies" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public StreamCopyBeforeWriteEventArgs(IEnumerable<byte> dataToWrite, Stream src, Stream dest, long totalBytesCopies)
            : base(src, dest, 0, totalBytesCopies)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyBeforeWriteEventArgs"/> class.
        /// </summary>
        /// <param name="dataToWrite">The data that should be written.</param>
        /// <param name="copier">The copier.</param>
        /// <param name="totalBytesCopies">The total bytes copies.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="copier" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="totalBytesCopies" /> is invalid.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="copier" /> is <see langword="null" />.
        /// </exception>
        public StreamCopyBeforeWriteEventArgs(IEnumerable<byte> dataToWrite, StreamCopier copier, long totalBytesCopies)
            : this(dataToWrite, copier.Source, copier.Destination, totalBytesCopies)
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets or sets the data that should be written.
        /// </summary>
        public IEnumerable<byte> Data
        {
            get { return this._data; }

            set { this._data = value; }
        }

        /// <summary>
        /// Gets or sets if write process should be skipped or not.
        /// </summary>
        public bool Skip
        {
            get { return this._skip; }

            set { this._skip = value; }
        }

        #endregion Properties
    }
}
