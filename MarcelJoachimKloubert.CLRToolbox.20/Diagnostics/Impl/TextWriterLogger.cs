// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    #region CLASS: TextWriterLogger<W>

    /// <summary>
    /// A logger that is based on a <see cref="TextWriter" />.
    /// </summary>
    /// <typeparam name="W">Type of the writer.</typeparam>
    public class TextWriterLogger<W> : LoggerFacadeBase where W : global::System.IO.TextWriter
    {
        #region Fields (2)

        private readonly TextWriterAction _DISPOSER;
        private readonly TextWriterProvider _PROVIDER;

        #endregion Fields

        #region Constructors (8)

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider, TextWriterAction disposer, bool isThreadSafe, object syncRoot)
            : base(isThreadSafe, syncRoot)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this._DISPOSER = disposer;
            this._PROVIDER = provider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider, bool isThreadSafe, object syncRoot)
            : this(provider, null, isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider, TextWriterAction disposer, object syncRoot)
            : this(provider, disposer, true, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider, TextWriterAction disposer, bool isThreadSafe)
            : this(provider, disposer, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider, TextWriterAction disposer)
            : this(provider, disposer, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider, object syncRoot)
            : this(provider, true, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider, bool isThreadSafe)
            : this(provider, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public TextWriterLogger(TextWriterProvider provider)
            : this(provider, (TextWriterAction)null)
        {
        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// Describes an action for a <see cref="TextWriter" />.
        /// </summary>
        /// <param name="logger">The underlying logger.</param>
        /// <param name="writer">The underlying writer.</param>
        public delegate void TextWriterAction(TextWriterLogger<W> logger, W writer);

        /// <summary>
        /// A provider that returns a <see cref="TextWriter" />.
        /// </summary>
        /// <param name="logger">The underlying logger.</param>
        /// <returns>The text writer to use.</returns>
        public delegate W TextWriterProvider(TextWriterLogger<W> logger);

        #endregion Delegates and Events

        #region Properties (2)

        /// <summary>
        /// Gets the optional disposer for the result of <see cref="TextWriterLogger{W}.Provider" />.
        /// </summary>
        public TextWriterAction Disposer
        {
            get { return this._DISPOSER; }
        }

        /// <summary>
        /// The provider for the text writer that should be used by <see cref="TextWriterLogger{W}.OnLog(ILogMessage)" /> method.
        /// </summary>
        public TextWriterProvider Provider
        {
            get { return this._PROVIDER; }
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            W writer = this._PROVIDER(this);
            if (writer == null)
            {
                return;
            }

            try
            {
                //TODO
            }
            finally
            {
                if (this._DISPOSER != null)
                {
                    this._DISPOSER(this, writer);
                }
            }
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: TextWriterLogger

    /// <summary>
    /// Factory class for <see cref="TextWriterLogger{W}" />.
    /// </summary>
    public static class TextWriterLogger
    {
        #region Methods (12)

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider, TextWriterLogger<TextWriter>.TextWriterAction disposer, bool isThreadSafe, object syncRoot)
        {
            return new TextWriterLogger<TextWriter>(provider, disposer, isThreadSafe, syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider, bool isThreadSafe, object syncRoot)
        {
            return new TextWriterLogger<TextWriter>(provider, isThreadSafe, syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider, TextWriterLogger<TextWriter>.TextWriterAction disposer, bool isThreadSafe)
        {
            return new TextWriterLogger<TextWriter>(provider, disposer, isThreadSafe);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider, TextWriterLogger<TextWriter>.TextWriterAction disposer, object syncRoot)
        {
            return new TextWriterLogger<TextWriter>(provider, disposer, syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="disposer">The optional disposer for the instance of the result of <paramref name="provider" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider, TextWriterLogger<TextWriter>.TextWriterAction disposer)
        {
            return new TextWriterLogger<TextWriter>(provider, disposer);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider, bool isThreadSafe)
        {
            return new TextWriterLogger<TextWriter>(provider, isThreadSafe);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider, object syncRoot)
        {
            return new TextWriterLogger<TextWriter>(provider, syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <param name="provider">The function that returns the <see cref="TextWriter" /> instance that should be used for logging operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<TextWriter> Create(TextWriterLogger<TextWriter>.TextWriterProvider provider)
        {
            return new TextWriterLogger<TextWriter>(provider);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <typeparam name="W">Type of the writer.</typeparam>
        /// <param name="writer">The text writer to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<W> Create<W>(W writer) where W : global::System.IO.TextWriter
        {
            return Create<W>(writer, true);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <typeparam name="W">Type of the writer.</typeparam>
        /// <param name="writer">The text writer to use.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<W> Create<W>(W writer, object syncRoot) where W : global::System.IO.TextWriter
        {
            return Create<W>(writer, true, syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <typeparam name="W">Type of the writer.</typeparam>
        /// <param name="writer">The text writer to use.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<W> Create<W>(W writer, bool isThreadSafe) where W : global::System.IO.TextWriter
        {
            return Create<W>(writer, isThreadSafe, new object());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextWriterLogger" /> class.
        /// </summary>
        /// <typeparam name="W">Type of the writer.</typeparam>
        /// <param name="writer">The text writer to use.</param>
        /// <param name="isThreadSafe">Logger should be thread safe or not.</param>
        /// <param name="syncRoot">The unique object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static TextWriterLogger<W> Create<W>(W writer, bool isThreadSafe, object syncRoot) where W : global::System.IO.TextWriter
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            return new TextWriterLogger<W>(delegate(TextWriterLogger<W> logger)
                                           {
                                               return writer;
                                           }, null
                                            , isThreadSafe
                                            , syncRoot);
        }

        #endregion Methods
    }

    #endregion
}