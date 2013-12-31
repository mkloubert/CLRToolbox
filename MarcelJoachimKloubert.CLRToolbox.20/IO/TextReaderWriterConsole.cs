// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A console that is based on <see cref="TextReader" /> and <see cref="TextWriter" /> objects.
    /// </summary>
    public sealed class TextReaderWriterConsole : ConsoleBase
    {
        #region Fields (5)

        private readonly ClearCallback _CLEAR_CALLBACK;
        private readonly TextReaderProvider _READER_PROVIDER;
        private readonly TextReaderUsedCallback _READER_USED_CALLBACK;
        private readonly TextWriterProvider _WRITER_PROVIDER;
        private readonly TextWriterUsedCallback _WRITER_USED_CALLBACK;

        #endregion Fields

        #region Constructors (18)

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="readerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextReader" /> has been used.
        /// </param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="writerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextWriter" /> has been used.
        /// </param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextReaderUsedCallback readerUsedCallback,
                                       TextWriterProvider writerProvider,
                                       TextWriterUsedCallback writerUsedCallback,
                                       ClearCallback clearCallback)
        {
            if (readerProvider == null)
            {
                throw new ArgumentNullException("readerProvider");
            }

            if (writerProvider == null)
            {
                throw new ArgumentNullException("writerProvider");
            }

            this._READER_PROVIDER = readerProvider;
            this._READER_USED_CALLBACK = readerUsedCallback;

            this._WRITER_PROVIDER = writerProvider;
            this._WRITER_USED_CALLBACK = writerUsedCallback;

            this._CLEAR_CALLBACK = clearCallback;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="readerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextReader" /> has been used.
        /// </param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="writerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextWriter" /> has been used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextReaderUsedCallback readerUsedCallback,
                                       TextWriterProvider writerProvider,
                                       TextWriterUsedCallback writerUsedCallback)
            : this(readerProvider, readerUsedCallback,
                   writerProvider, writerUsedCallback,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="readerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextReader" /> has been used.
        /// </param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextReaderUsedCallback readerUsedCallback,
                                       TextWriterProvider writerProvider,
                                       ClearCallback clearCallback)
            : this(readerProvider, readerUsedCallback,
                   writerProvider, (TextWriterUsedCallback)null,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="writerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextWriter" /> has been used.
        /// </param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextWriterProvider writerProvider,
                                       TextWriterUsedCallback writerUsedCallback,
                                       ClearCallback clearCallback)
            : this(readerProvider, (TextReaderUsedCallback)null,
                   writerProvider, writerUsedCallback,
                   clearCallback)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="readerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextReader" /> has been used.
        /// </param>
        /// <param name="writer">The writer to use.</param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextReaderUsedCallback readerUsedCallback,
                                       TextWriter writer,
                                       ClearCallback clearCallback)
            : this(readerProvider, readerUsedCallback,
                   ToProvider(writer), (TextWriterUsedCallback)null,
                   clearCallback)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="writerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextWriter" /> has been used.
        /// </param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReader reader,
                                       TextWriterProvider writerProvider,
                                       TextWriterUsedCallback writerUsedCallback,
                                       ClearCallback clearCallback)
            : this(ToProvider(reader), (TextReaderUsedCallback)null,
                   writerProvider, writerUsedCallback,
                   clearCallback)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="readerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextReader" /> has been used.
        /// </param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextReaderUsedCallback readerUsedCallback,
                                       TextWriterProvider writerProvider)
            : this(readerProvider, readerUsedCallback,
                   writerProvider,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="writerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextWriter" /> has been used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextWriterProvider writerProvider,
                                       TextWriterUsedCallback writerUsedCallback)
            : this(readerProvider,
                   writerProvider, writerUsedCallback,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextWriterProvider writerProvider,
                                       ClearCallback clearCallback)
            : this(readerProvider,
                   writerProvider, (TextWriterUsedCallback)null,
                   clearCallback)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="writer">The writer to use.</param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReader reader,
                                       TextWriter writer,
                                       ClearCallback clearCallback)
            : this(ToProvider(reader),
                   (TextReaderUsedCallback)null,
                   ToProvider(writer),
                   (TextWriterUsedCallback)null,
                   clearCallback)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="readerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextReader" /> has been used.
        /// </param>
        /// <param name="writer">The writer to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextReaderUsedCallback readerUsedCallback,
                                       TextWriter writer)
            : this(readerProvider,
                   readerUsedCallback,
                   writer,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="writer">The writer to use.</param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextWriter writer,
                                       ClearCallback clearCallback)
            : this(readerProvider,
                   (TextReaderUsedCallback)null,
                   writer,
                   clearCallback)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="writerUsedCallback">
        /// The optional logic that is invoked AFTER provided <see cref="TextWriter" /> has been used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReader reader,
                                       TextWriterProvider writerProvider,
                                       TextWriterUsedCallback writerUsedCallback)
            : this(reader,
                   writerProvider, writerUsedCallback,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <param name="clearCallback">The optional logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReader reader,
                                       TextWriterProvider writerProvider,
                                       ClearCallback clearCallback)
            : this(reader,
                   writerProvider, (TextWriterUsedCallback)null,
                   clearCallback)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextWriterProvider writerProvider)
            : this(readerProvider,
                   writerProvider,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="writer">The writer to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReader reader,
                                       TextWriter writer)
            : this(reader,
                   writer,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="readerProvider">The logic to provides the <see cref="TextReader" /> for read operations.</param>
        /// <param name="writer">The writer to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="readerProvider" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReaderProvider readerProvider,
                                       TextWriter writer)
            : this(readerProvider,
                   writer,
                   (ClearCallback)null)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextReaderWriterConsole" /> class.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="writerProvider">The logic to provides the <see cref="TextWriter" /> for write operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="writerProvider" /> are <see langword="null" />.
        /// </exception>
        public TextReaderWriterConsole(TextReader reader,
                                       TextWriterProvider writerProvider)
            : this(reader,
                   writerProvider,
                   (ClearCallback)null)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the unique object for sync operations.
        /// </summary>
        public object SyncRoot
        {
            get { return this._SYNC; }
        }

        #endregion Properties

        #region Delegates and Events (5)

        // Delegates (5) 

        /// <summary>
        /// Describes logic for <see cref="TextReaderWriterConsole.OnClear()" /> method.
        /// </summary>
        /// <param name="console">The underlying console.</param>
        public delegate void ClearCallback(TextReaderWriterConsole console);
        /// <summary>
        /// Describes logic for providing the <see cref="TextReader" /> for read operations.
        /// </summary>
        /// <param name="console">The underlying console.</param>
        /// <returns>The <see cref="TextReader" /> to use.</returns>
        public delegate TextReader TextReaderProvider(TextReaderWriterConsole console);
        /// <summary>
        /// Describes logic that is invoked AFTER a provided <see cref="TextReader" /> has been used.
        /// </summary>
        /// <param name="console">The underlying console.</param>
        /// <param name="reader">The reader that has been used.</param>
        /// <param name="ex">If defined, this stores the exception that has been thrown while using <paramref name="reader" />.</param>
        public delegate void TextReaderUsedCallback(TextReaderWriterConsole console, TextReader reader, Exception ex);
        /// <summary>
        /// Describes logic for providing the <see cref="TextWriter" /> for write operations.
        /// </summary>
        /// <param name="console">The underlying console.</param>
        /// <returns>The <see cref="TextWriter" /> to use.</returns>
        public delegate TextWriter TextWriterProvider(TextReaderWriterConsole console);
        /// <summary>
        /// Describes logic that is invoked AFTER a provided <see cref="TextWriter" /> has been used.
        /// </summary>
        /// <param name="console">The underlying console.</param>
        /// <param name="writer">The writer that has been used.</param>
        /// <param name="ex">If defined, this stores the exception that has been thrown while using <paramref name="writer" />.</param>
        public delegate void TextWriterUsedCallback(TextReaderWriterConsole console, TextWriter writer, Exception ex);

        #endregion Delegates and Events

        #region Methods (9)

        // Protected Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.OnClear()" />
        protected override void OnClear()
        {
            if (this._CLEAR_CALLBACK != null)
            {
                this._CLEAR_CALLBACK(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.OnReadLine(TextWriter)" />
        protected override void OnReadLine(TextWriter line)
        {
            this.InvokeForReader(delegate(TextReader reader, TextWriter l)
                {
                    l.WriteLine(reader.ReadLine());
                }, line);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.OnWrite(string)" />
        protected override void OnWrite(string text)
        {
            this.InvokeForWriter(delegate(TextWriter writer, string t)
                {
                    writer.Write(t);
                }, text);
        }
        // Private Methods (6) 

        private void InvokeForReader(Action<TextReader> action)
        {
            this.InvokeForReader<object>(delegate(TextReader reader, object state)
                {
                    action(reader);
                }, null);
        }

        private void InvokeForReader<S>(Action<TextReader, S> action, S state)
        {
            Exception occuredException = null;
            TextReader reader = this._READER_PROVIDER(this);
            try
            {
                action(reader, state);
            }
            catch (Exception ex)
            {
                occuredException = ex;
            }
            finally
            {
                if (this._READER_USED_CALLBACK != null)
                {
                    this._READER_USED_CALLBACK(this,
                                               reader,
                                               occuredException);
                }
                else
                {
                    if (occuredException != null)
                    {
                        throw occuredException;
                    }
                }
            }
        }

        private void InvokeForWriter(Action<TextWriter> action)
        {
            this.InvokeForWriter<object>(delegate(TextWriter writer, object state)
                {
                    action(writer);
                }, null);
        }

        private void InvokeForWriter<S>(Action<TextWriter, S> action, S state)
        {
            Exception occuredException = null;
            TextWriter writer = this._WRITER_PROVIDER(this);
            try
            {
                action(writer, state);
            }
            catch (Exception ex)
            {
                occuredException = ex;
            }
            finally
            {
                if (this._WRITER_USED_CALLBACK != null)
                {
                    this._WRITER_USED_CALLBACK(this,
                                               writer,
                                               occuredException);
                }
                else
                {
                    if (occuredException != null)
                    {
                        throw occuredException;
                    }
                }
            }
        }

        private static TextReaderProvider ToProvider(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            return delegate(TextReaderWriterConsole console)
                {
                    return reader;
                };
        }

        private static TextWriterProvider ToProvider(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            return delegate(TextReaderWriterConsole console)
                {
                    return writer;
                };
        }

        #endregion Methods
    }
}
