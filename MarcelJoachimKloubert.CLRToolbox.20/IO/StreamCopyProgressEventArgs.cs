// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Stores the arguments of a progress event for a stream copy operation.
    /// </summary>
    public class StreamCopyProgressEventArgs : EventArgs
    {
        #region Fields (7)

        private readonly long _BYTES_COPIED;
        private bool _cancel;
        private readonly long? _DESTIONATION_LENGTH;
        private readonly long? _DESTIONATION_POSITION;
        private readonly long? _SOURCE_LENGTH;
        private readonly long? _SOURCE_POSITION;
        private readonly long _TOTAL_BYTES_COPIED;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyProgressEventArgs"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <param name="bytesCopied">The bytes copied.</param>
        /// <param name="totalBytesCopies">The total bytes copies.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bytesCopied" /> and/or <paramref name="totalBytesCopies" /> are invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public StreamCopyProgressEventArgs(Stream src, Stream dest, long bytesCopied, long totalBytesCopies)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (dest == null)
            {
                throw new ArgumentNullException("dest");
            }

            if (bytesCopied < 0)
            {
                throw new ArgumentOutOfRangeException("bytesCopied");
            }

            if (totalBytesCopies < 0)
            {
                throw new ArgumentOutOfRangeException("totalBytesCopies");
            }

            this._SOURCE_LENGTH = TryGetPositiveStreamValue(src, delegate(Stream s)
            {
                return s.Length;
            });

            this._SOURCE_POSITION = TryGetPositiveStreamValue(src, delegate(Stream s)
            {
                return s.Position;
            });

            this._DESTIONATION_LENGTH = TryGetPositiveStreamValue(dest, delegate(Stream s)
            {
                return s.Length;
            });

            this._DESTIONATION_POSITION = TryGetPositiveStreamValue(dest, delegate(Stream s)
            {
                return s.Position;
            });

            this._BYTES_COPIED = bytesCopied;
            this._TOTAL_BYTES_COPIED = totalBytesCopies;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyProgressEventArgs"/> class.
        /// </summary>
        /// <param name="copier">The copier.</param>
        /// <param name="bytesCopied">The bytes copied.</param>
        /// <param name="totalBytesCopies">The total bytes copies.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bytesCopied" /> and/or <paramref name="totalBytesCopies" /> are invalid.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="copier" /> is <see langword="null" />.
        /// </exception>
        public StreamCopyProgressEventArgs(StreamCopier copier, long bytesCopied, long totalBytesCopies)
            : this(copier.Source, copier.Destination, bytesCopied, totalBytesCopies)
        {

        }

        #endregion Constructors

        #region Properties (7)

        /// <summary>
        /// Gets the number of bytes that were copied in the last step.
        /// </summary>
        public long BytesCopied
        {
            get { return this._BYTES_COPIED; }
        }

        /// <summary>
        /// Gets or sets if the copy progress should be canceled or not.
        /// </summary>
        public bool Cancel
        {
            get { return this._cancel; }

            set { this._cancel = value; }
        }

        /// <summary>
        /// Gets the length of the destionation (stream).
        /// </summary>
        public long? DestionationLength
        {
            get { return this._DESTIONATION_LENGTH; }
        }

        /// <summary>
        /// Gets the current position of the destionation (stream).
        /// </summary>
        public long? DestionationPosition
        {
            get { return this._DESTIONATION_POSITION; }
        }

        /// <summary>
        /// Gets the length of the source (stream).
        /// </summary>
        public long? SourceLength
        {
            get { return this._SOURCE_LENGTH; }
        }

        /// <summary>
        /// Gets the current position of the source (stream).
        /// </summary>
        public long? SourcePosition
        {
            get { return this._SOURCE_POSITION; }
        }

        /// <summary>
        /// Gets the number of bytes that were totally copied in the whole copy progress until now.
        /// </summary>
        public long TotalBytesCopied
        {
            get { return this._TOTAL_BYTES_COPIED; }
        }

        #endregion Properties

        #region Methods (2)

        // Private Methods (2) 

        private static long? TryGetPositiveStreamValue(Stream stream, Func<Stream, long?> func)
        {
            long? result = TryGetStreamValue<long>(stream, func);

            return result < 0 ? null : result;
        }

        private static T? TryGetStreamValue<T>(Stream stream, Func<Stream, T?> func) where T : struct
        {
            try
            {
                return func(stream);
            }
            catch
            {
                return null;
            }
        }

        #endregion Methods
    }
}
