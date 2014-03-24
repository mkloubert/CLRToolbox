// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Arguments for a progress error event.
    /// </summary>
    public sealed class StreamCopyProgressErrorEventArgs : StreamCopyProgressEventArgs
    {
        #region Fields (3)

        private readonly AggregateException _ERRORS;
        private bool _handled;
        private bool _logErrors;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyProgressErrorEventArgs"/> class.
        /// </summary>
        /// <param name="ex">The occured exception(s).</param>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <param name="bytesCopied">The bytes copied.</param>
        /// <param name="totalBytesCopies">The total bytes copies.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bytesCopied" /> and/or <paramref name="bytesCopied" /> are invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ex" />, <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public StreamCopyProgressErrorEventArgs(Exception ex, Stream src, Stream dest, long bytesCopied, long totalBytesCopies)
            : base(src, dest, bytesCopied, totalBytesCopies)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            this._ERRORS = ex as AggregateException;
            if (this._ERRORS == null)
            {
                this._ERRORS = new AggregateException(ex);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyProgressErrorEventArgs"/> class.
        /// </summary>
        /// <param name="ex">The occured exception(s).</param>
        /// <param name="copier">The copier.</param>
        /// <param name="bytesCopied">The bytes copied.</param>
        /// <param name="totalBytesCopies">The total bytes copies.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ex" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bytesCopied" /> and/or <paramref name="bytesCopied" /> are invalid.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="copier" /> is <see langword="null" />.
        /// </exception>
        public StreamCopyProgressErrorEventArgs(Exception ex, StreamCopier copier, long bytesCopied, long totalBytesCopies)
            : this(ex, copier.Source, copier.Destination, bytesCopied, totalBytesCopies)
        {

        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the list of occured errors.
        /// </summary>
        public AggregateException Errors
        {
            get { return this._ERRORS; }
        }

        /// <summary>
        /// Gets or sets if <see cref="StreamCopyProgressErrorEventArgs.Errors" /> have been handled or not.
        /// If not <see cref="StreamCopyProgressErrorEventArgs.Errors" /> is (re-)thrown.
        /// </summary>
        public bool Handled
        {
            get { return this._handled; }

            set { this._handled = value; }
        }

        /// <summary>
        /// Gets or sets if <see cref="StreamCopyProgressErrorEventArgs.Errors" /> should be logged or not.
        /// </summary>
        public bool LogErrors
        {
            get { return this._logErrors; }

            set { this._logErrors = value; }
        }

        #endregion Properties
    }
}
