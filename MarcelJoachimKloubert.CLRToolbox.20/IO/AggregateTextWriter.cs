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
    public partial class AggregateTextWriter : TextWriter
    {
        #region Fields (2)

        private readonly List<TextWriterEntry> _ENTRIES = new List<TextWriterEntry>();
        private readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateTextWriter" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AggregateTextWriter(object syncRoot)
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            this._SYNC = syncRoot;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateTextWriter" /> class.
        /// </summary>
        protected AggregateTextWriter()
            : this(new object())
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

        #region Methods (48)

        // Public Methods (42) 

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
            this.InvokeActionForWriters(delegate(TextWriter writer)
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

        /// <inheriteddoc />
        public override void Write(bool value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, bool v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(char value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, char v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(char[] buffer)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, char[] b)
                {
                    writer.Write(b);
                }, buffer);
        }

        /// <inheriteddoc />
        public override void Write(decimal value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, decimal v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(double value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, double v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(float value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, float v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(int value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, int v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(long value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, long v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(object value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, object v)
                {
                    writer.Write(v);
                }, ParseObject(value));
        }

        /// <inheriteddoc />
        public override void Write(string value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, string v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(uint value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, uint v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(ulong value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, ulong v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(string format, object arg0)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, string f, object a)
                {
                    writer.Write(f, a);
                }, format
                 , ParseObject(arg0));
        }

        /// <inheriteddoc />
        public override void Write(string format, params object[] arg)
        {
            IEnumerable<object> parsedArg = null;
            if (arg != null)
            {
                parsedArg = CollectionHelper.Select(arg,
                                                    delegate(object i)
                                                    {
                                                        return ParseObject(i);
                                                    });
            }

            this.InvokeActionForWriters(delegate(TextWriter writer, string f, object[] a)
                {
                    writer.Write(f, a);
                }, format
                 , CollectionHelper.AsArray(parsedArg) ?? new object[0]);
        }

        /// <inheriteddoc />
        public override void Write(char[] buffer, int index, int count)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, char[] b, int i, int c)
                {
                    writer.Write(b, i, c);
                }, buffer
                 , index
                 , count);
        }

        /// <inheriteddoc />
        public override void Write(string format, object arg0, object arg1)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, string f, object a0, object a1)
                {
                    writer.Write(f, a0, a1);
                }, format
                 , ParseObject(arg0)
                 , ParseObject(arg1));
        }

#if !WINDOWS_PHONE

        /// <inheriteddoc />
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            this.InvokeActionForWriters(delegate(global::System.IO.TextWriter writer, string f, object a0, object a1, object a2)
                {
                    writer.Write(f, a0, a1, a2);
                }, format
                 , ParseObject(arg0)
                 , ParseObject(arg1)
                 , ParseObject(arg2));
        }

#endif


        /// <inheriteddoc />
        public override void WriteLine()
        {
            this.InvokeActionForWriters(delegate(TextWriter writer)
                {
                    writer.WriteLine();
                });
        }

        /// <inheriteddoc />
        public override void WriteLine(bool value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, bool v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(char value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, char v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(char[] buffer)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, char[] b)
                {
                    writer.WriteLine(b);
                }, buffer);
        }

        /// <inheriteddoc />
        public override void WriteLine(decimal value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, decimal v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(double value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, double v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(float value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, float v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(int value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, int v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(long value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, long v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(object value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, object v)
                {
                    writer.WriteLine(v);
                }, ParseObject(value));
        }

        /// <inheriteddoc />
        public override void WriteLine(string value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, string v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(uint value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, uint v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(ulong value)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, ulong v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(string format, object arg0)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, string f, object a)
                {
                    writer.WriteLine(f, a);
                }, format
                 , ParseObject(arg0));
        }

        /// <inheriteddoc />
        public override void WriteLine(string format, params object[] arg)
        {
            IEnumerable<object> parsedArg = null;
            if (arg != null)
            {
                parsedArg = CollectionHelper.Select(arg,
                                                    delegate(object i)
                                                    {
                                                        return ParseObject(i);
                                                    });
            }

            this.InvokeActionForWriters(delegate(TextWriter writer, string f, object[] a)
                {
                    writer.WriteLine(f, a);
                }, format
                 , CollectionHelper.AsArray(parsedArg) ?? new object[0]);
        }

        /// <inheriteddoc />
        public override void WriteLine(char[] buffer, int index, int count)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, char[] b, int i, int c)
                {
                    writer.WriteLine(b, i, c);
                }, buffer, index, count);
        }

        /// <inheriteddoc />
        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.InvokeActionForWriters(delegate(TextWriter writer, string f, object a0, object a1)
                {
                    writer.WriteLine(f, a0, a1);
                }, format
                 , ParseObject(arg0)
                 , ParseObject(arg1));
        }

#if !WINDOWS_PHONE

        /// <inheriteddoc />
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this.InvokeActionForWriters(delegate(global::System.IO.TextWriter writer, string f, object a0, object a1, object a2)
                {
                    writer.WriteLine(f, a0, a1, a2);
                }, format
                 , ParseObject(arg0)
                 , ParseObject(arg1)
                 , ParseObject(arg2));
        }

#endif

        // Protected Methods (1) 

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

        // Private Methods (6) 

        private void InvokeActionForWriters(Action<TextWriter> action)
        {
            this.InvokeActionForWriters<object>(delegate(TextWriter writer, object value)
                                                {
                                                    action(writer);
                                                }, null);
        }

        private void InvokeActionForWriters<T>(Action<TextWriter, T> action, T value)
        {
            this.InvokeActionForWriters<T, object>(delegate(TextWriter w, T a1, object a2)
                                                   {
                                                       action(w, a1);
                                                   }, value, null);
        }

        private void InvokeActionForWriters<T1, T2>(Action<TextWriter, T1, T2> action, T1 arg1, T2 arg2)
        {
            this.InvokeActionForWriters<T1, T2, object>(delegate(TextWriter w, T1 a1, T2 a2, object a3)
                                                        {
                                                            action(w, a1, a2);
                                                        }, arg1, arg2, null);
        }

        private void InvokeActionForWriters<T1, T2, T3>(Action<TextWriter, T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            this.InvokeActionForWriters<T1, T2, T3, object>(delegate(TextWriter w, T1 a1, T2 a2, T3 a3, object a4)
                                                           {
                                                               action(w, a1, a2, a3);
                                                           }, arg1, arg2, arg3, null);
        }

        private void InvokeActionForWriters<T1, T2, T3, T4>(Action<TextWriter, T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            lock (this._SYNC)
            {
                CollectionHelper.ForAll(this._ENTRIES,
                                        delegate(IForAllItemExecutionContext<TextWriterEntry> ctx)
                                        {
                                            action(ctx.Item.WRITER, arg1, arg2, arg3, arg4);
                                        });
            }
        }

        private static object ParseObject(object obj)
        {
            object result = obj;

            if (result is IEnumerable<char>)
            {
                // keep sure to have a "real" string
                result = StringHelper.AsString(result);
            }

            return result;
        }

        #endregion Methods
    }
}