// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A basic stream that is only readable.
    /// </summary>
    public abstract class ReadOnlyStreamBase : Stream
    {
        #region Fields (2)

        private bool _isDisposed;
        /// <summary>
        /// An unique object for sync operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyStreamBase" /> class.
        /// </summary>
        /// <param name="sync">The value for the <see cref="ReadOnlyStreamBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="" />.
        /// </exception>
        protected ReadOnlyStreamBase(object sync)
        {
            if (sync == null)
            {
                throw new ArgumentNullException("sync");
            }

            this._SYNC = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyStreamBase" /> class.
        /// </summary>
        protected ReadOnlyStreamBase()
            : this(new object())
        {

        }

        #endregion Constructors

        #region Properties (7)

        /// <inheriteddoc />
        public override bool CanRead
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <inheriteddoc />
        public override bool CanTimeout
        {
            get { return false; }
        }

        /// <inheriteddoc />
        public override sealed bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Gets of that stream has been disposed or not.
        /// </summary>
        public bool IsDisposed
        {
            get { return this._isDisposed; }

            private set { this._isDisposed = value; }
        }

        /// <inheriteddoc />
        public override long Length
        {
            get
            {
                this.ThrowIfDisposed();

                return -1;
            }
        }

        /// <inheriteddoc />
        public override long Position
        {
            get
            {
                this.ThrowIfDisposed();

                return -1;
            }

            set { throw new NotSupportedException(); }
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (5) 

        /// <inheriteddoc />
        public override void Flush()
        {
            // dummy
        }

        /// <inheriteddoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (buffer.Length - offset < count)
            {
                throw new ArgumentException("buffer");
            }

            int result = count;

            lock (this._SYNC)
            {
                this.OnRead(buffer, offset, count, ref result);
            }

            return result;
        }

        /// <inheriteddoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <inheriteddoc />
        public override sealed void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <inheriteddoc />
        public override sealed void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        // Protected Methods (3) 

        /// <inheriteddoc />
        protected override void Dispose(bool disposing)
        {
            lock (this._SYNC)
            {
                base.Dispose(disposing);

                if (disposing)
                {
                    this.IsDisposed = true;
                }
            }
        }

        /// <summary>
        /// Stores the logic for the <see cref="ReadOnlyStreamBase.Read(byte[], int, int)" /> method.
        /// </summary>
        /// <param name="buffer">The buffer where to write the data to.</param>
        /// <param name="offset">The zero based offset inside <paramref name="buffer" />.</param>
        /// <param name="count">The number of data that should be read.</param>
        /// <param name="bytesRead">
        /// The number of bytes that were really read.
        /// That value is the same as <paramref name="count" /> by default.
        /// </param>
        protected abstract void OnRead(byte[] buffer, int offset, int count, ref int bytesRead);

        /// <summary>
        /// Throws an exception if that stream has been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Stream has been disposed.</exception>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        #endregion Methods
    }
}
