// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A class that copies data from one stream to another.
    /// </summary>
    public sealed partial class StreamCopier : DisposableBase
    {
        #region Fields (5)

        private readonly int _BUFFER_SIZE;
        private readonly Stream _DEST;
        private readonly bool _OWNS_DEST;
        private readonly bool _OWNS_SOURCE;
        private readonly Stream _SOURCE;

        #endregion Fields

        #region Constructors (8)

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="ownsSource">
        /// Owns <paramref name="src" /> or not what means if stream should also be disposed if
        /// that object is disposed.
        /// </param>
        /// <param name="dest">The dest.</param>
        /// <param name="ownsDest">
        /// Owns <paramref name="dest" /> or not what means if stream should also be disposed if
        /// that object is disposed.</param>
        /// <param name="bufferSize">The buffer size for the copy process to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        public StreamCopier(Stream src, bool ownsSource, Stream dest, bool ownsDest, int bufferSize)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (dest == null)
            {
                throw new ArgumentNullException("dest");
            }

            if (bufferSize < 1)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }

            this._SOURCE = src;
            this._OWNS_SOURCE = ownsSource;

            this._DEST = dest;
            this._OWNS_DEST = ownsDest;

            this._BUFFER_SIZE = bufferSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="ownsSource">
        /// Owns <paramref name="src" /> or not what means if stream should also be disposed if
        /// that object is disposed.
        /// </param>
        /// <param name="dest">The dest.</param>
        /// <param name="ownsDest">
        /// Owns <paramref name="dest" /> or not what means if stream should also be disposed if
        /// that object is disposed.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        public StreamCopier(Stream src, bool ownsSource, Stream dest, bool ownsDest)
            : this(src, ownsSource, dest, ownsDest, 81920)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="ownsSource">
        /// Owns <paramref name="src" /> or not what means if stream should also be disposed if
        /// that object is disposed.
        /// </param>
        /// <param name="dest">The dest.</param>
        /// <param name="bufferSize">The buffer size for the copy process to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        public StreamCopier(Stream src, bool ownsSource, Stream dest, int bufferSize)
            : this(src, ownsSource, dest, false, bufferSize)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="ownsDest">
        /// Owns <paramref name="dest" /> or not what means if stream should also be disposed if
        /// that object is disposed.</param>
        /// <param name="bufferSize">The buffer size for the copy process to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        public StreamCopier(Stream src, Stream dest, bool ownsDest, int bufferSize)
            : this(src, false, dest, ownsDest, bufferSize)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="ownsDest">
        /// Owns <paramref name="dest" /> or not what means if stream should also be disposed if
        /// that object is disposed.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        public StreamCopier(Stream src, Stream dest, bool ownsDest)
            : this(src, false, dest, ownsDest)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="ownsSource">
        /// Owns <paramref name="src" /> or not what means if stream should also be disposed if
        /// that object is disposed.
        /// </param>
        /// <param name="dest">The dest.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        public StreamCopier(Stream src, bool ownsSource, Stream dest)
            : this(src, ownsSource, dest, false)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="bufferSize">The buffer size for the copy process to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        public StreamCopier(Stream src, Stream dest, int bufferSize)
            : this(src, false, dest, false, bufferSize)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopier"/> class.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The dest.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" /> references.
        /// </exception>
        public StreamCopier(Stream src, Stream dest)
            : this(src, false, dest, false)
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the destination stream.
        /// </summary>
        public Stream Destination
        {
            get { return this._DEST; }
        }

        /// <summary>
        /// Gets the source stream.
        /// </summary>
        public Stream Source
        {
            get { return this._SOURCE; }
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (7) 

        /// <summary>
        /// Closes the destination stream.
        /// </summary>
        public void Close()
        {
            this.Destination.Close();
        }

        /// <summary>
        /// Creates a new execution context WITHOUT starting the copy process.
        /// </summary>
        /// <returns>The created context.</returns>
        public IStreamCopyExecutionContext CreateContext()
        {
            return this.CreateContext<object>(null);
        }

        /// <summary>
        /// Creates a new execution context WITHOUT starting the copy process.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <returns>The created context.</returns>
        public IStreamCopyExecutionContext<T> CreateContext<T>(T state)
        {
            return new StreamCopyExecutionContext<T>(this, state);
        }

        /// <summary>
        /// Starts a new copy process.
        /// </summary>
        /// <returns>The underlying context.</returns>
        public IStreamCopyExecutionContext Execute()
        {
            return this.Execute<object>(null);
        }

        /// <summary>
        /// Starts a new copy process.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <returns>The underlying context.</returns>
        public IStreamCopyExecutionContext<T> Execute<T>(T state)
        {
            IStreamCopyExecutionContext<T> result = this.CreateContext<T>(state);
            result.Start();

            return result;
        }

        /// <summary>
        /// Flushes the destination stream.
        /// </summary>
        public void Flush()
        {
            this.Destination.Flush();
        }

        /// <summary>
        /// Flushes and closes the destination stream.
        /// </summary>
        public void FlushAndClose()
        {
            this.Flush();
            this.Close();
        }
        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (this._OWNS_SOURCE)
                    {
                        this.Source.Dispose();
                    }
                }
                finally
                {
                    if (this._OWNS_DEST)
                    {
                        this.Destination.Dispose();
                    }
                }
            }
        }

        #endregion Methods

#if !NET2 && !NET20 && !NET35 && !WINDOWS_PHONE && !MONO2 && !MONO20
        /// <summary>
        /// Creates a task that executes a copy process async.
        /// </summary>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IStreamCopyExecutionContext> CreateContextAsync()
        {
            return new global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IStreamCopyExecutionContext>(() =>
                {
                    return this.Execute();
                });
        }

        /// <summary>
        /// Creates a task that executes a copy process async.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IStreamCopyExecutionContext<T>> CreateContextAsync<T>(T state)
        {
            return new global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IStreamCopyExecutionContext<T>>(() =>
                {
                    return this.Execute(state);
                });
        }

        /// <summary>
        /// Executes <see cref="StreamCopier.Execute()" /> method async.
        /// </summary>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IStreamCopyExecutionContext> ExecuteAsync()
        {
            var task = this.CreateContextAsync();
            task.Start();

            return task;
        }

        /// <summary>
        /// Executes <see cref="StreamCopier.Execute{T}(T)" /> method async.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IStreamCopyExecutionContext<T>> ExecuteAsync<T>(T state)
        {
            var task = this.CreateContextAsync<T>(state);
            task.Start();

            return task;
        }
#endif
    }
}
