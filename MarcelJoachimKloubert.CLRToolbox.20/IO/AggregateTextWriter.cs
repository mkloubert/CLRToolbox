// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A thread safe <see cref="TextWriter" /> that writes to a list of inner text writers.
    /// </summary>
    public partial class AggregateTextWriter : TextWriterWrapperBase
    {
        #region Fields (1)

        private readonly List<TextWriterEntry> _ENTRIES = new List<TextWriterEntry>();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateTextWriter" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public AggregateTextWriter(object syncRoot)
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateTextWriter" /> class.
        /// </summary>
        public AggregateTextWriter()
            : base()
        {
        }

        #endregion Constructors

        #region Properties (1)

        /// <inheriteddoc />
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        #endregion Properties

        #region Methods (9)

        // Public Methods (7) 

        /// <summary>
        /// Stores a text writer to that instance.
        /// </summary>
        /// <param name="writer">The writer to store.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        public void AddWriter(TextWriter writer)
        {
            this.AddWriter(writer, false);
        }

        /// <summary>
        /// Stores a text writer to that instance.
        /// </summary>
        /// <param name="writer">The writer to store.</param>
        /// <param name="close">
        /// If <see cref="AggregateTextWriter.Close()" /> is called, also close <paramref name="writer" /> or not.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        public void AddWriter(TextWriter writer, bool close)
        {
            this.AddWriter(writer, close, false);
        }

        /// <summary>
        /// Stores a text writer to that instance.
        /// </summary>
        /// <param name="writer">The writer to store.</param>
        /// <param name="close">
        /// If <see cref="AggregateTextWriter.Close()" /> is called, also close <paramref name="writer" /> or not.
        /// </param>
        /// <param name="dispose">
        /// If <see cref="AggregateTextWriter.Dispose(bool)" /> is called, also dispose <paramref name="writer" /> or not.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        public void AddWriter(TextWriter writer, bool close, bool dispose)
        {
            lock (this._SYNC)
            {
                if (writer == null)
                {
                    throw new ArgumentNullException("writer");
                }

                TextWriterEntry newEntry = new TextWriterEntry(writer, close, dispose);

                this._ENTRIES.Add(newEntry);
            }
        }

        /// <summary>
        /// Clears the current list of stored text writers.
        /// </summary>
        public void ClearWriters()
        {
            lock (this._SYNC)
            {
                this._ENTRIES.Clear();
            }
        }

        /// <inheriteddoc />
        public override void Close()
        {
            lock (this._SYNC)
            {
                base.Close();

                IEnumerable<TextWriterEntry> entriesToClose =
                        CollectionHelper.Where(this._ENTRIES,
                                               delegate(TextWriterEntry entry)
                                               {
                                                   return entry.CLOSE;
                                               });

                CollectionHelper.ForAll(entriesToClose,
                                        delegate(IForAllItemExecutionContext<TextWriterEntry> ctx)
                                        {
                                            TextWriter obj = ctx.Item.WRITER;

                                            obj.Close();
                                        }, true);
            }
        }

        /// <inheriteddoc />
        public override void Flush()
        {
            this.InvokeActionForWriter(delegate(TextWriter writer)
                {
                    writer.Flush();
                });
        }

        /// <summary>
        /// Returns a new list of all currently stored <see cref="TextWriter" /> objects.
        /// </summary>
        /// <returns>The list of all currently stored <see cref="TextWriter" /> objects.</returns>
        public List<TextWriter> GetWriters()
        {
            List<TextWriter> result;

            lock (this._SYNC)
            {
                result = new List<TextWriter>(CollectionHelper.Select(this._ENTRIES,
                                                                      delegate(TextWriterEntry entry)
                                                                      {
                                                                          return entry.WRITER;
                                                                      }));
            }

            return result;
        }

        // Protected Methods (2) 

        /// <inheriteddoc />
        protected override void Dispose(bool disposing)
        {
            lock (this._SYNC)
            {
                base.Dispose(disposing);

                if (disposing)
                {
                    IEnumerable<TextWriterEntry> entriesToDispose =
                        CollectionHelper.Where(this._ENTRIES,
                                               delegate(TextWriterEntry entry)
                                               {
                                                   return entry.DISPOSE;
                                               });

                    CollectionHelper.ForAll(entriesToDispose,
                                            delegate(IForAllItemExecutionContext<TextWriterEntry> ctx)
                                            {
                                                TextWriter obj = ctx.Item.WRITER;
                                                bool doDispose = true;

                                                ITMDisposable tmDispObj = obj as ITMDisposable;
                                                if (tmDispObj != null)
                                                {
                                                    doDispose = tmDispObj.IsDisposed == false;
                                                }

                                                if (doDispose)
                                                {
                                                    obj.Dispose();
                                                }
                                            }, true);
                }
                else
                {
                    this._ENTRIES.Clear();
                }
            }
        }

        /// <inheriteddoc />
        protected override void InvokeActionForWriter<T1, T2, T3, T4>(Action<TextWriter, T1, T2, T3, T4> action,
                                                                      T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            lock (this._SYNC)
            {
                CollectionHelper.ForAll(this._ENTRIES,
                                        delegate(IForAllItemExecutionContext<TextWriterEntry, Tuple<T1, T2, T3, T4>> ctx)
                                        {
                                            action(ctx.Item.WRITER,
                                                   ctx.State.Item1,
                                                   ctx.State.Item2,
                                                   ctx.State.Item3,
                                                   ctx.State.Item4);
                                        }, Tuple.Create<T1, T2, T3, T4>(arg1, arg2, arg3, arg4));
            }
        }

        #endregion Methods
    }
}