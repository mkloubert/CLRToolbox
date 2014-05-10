// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A basic wrapper for one or more <see cref="TextWriter" />.
    /// </summary>
    public abstract class TextWriterWrapperBase : TextWriter
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterWrapperBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TextWriterWrapperBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected TextWriterWrapperBase(object syncRoot)
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            this._SYNC = syncRoot;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterWrapperBase" /> class.
        /// </summary>
        protected TextWriterWrapperBase()
            : this(new object())
        {
        }

        #endregion Constructors

        #region Methods (39)

        // Public Methods (33) 

        /// <inheriteddoc />
        public override void Write(bool value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, bool v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(char value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, char v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(char[] buffer)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, char[] b)
                {
                    writer.Write(b);
                }, buffer);
        }

        /// <inheriteddoc />
        public override void Write(decimal value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, decimal v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(double value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, double v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(float value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, float v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(int value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, int v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(long value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, long v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(object value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, object v)
                {
                    writer.Write(v);
                }, this.ParseObject(value));
        }

        /// <inheriteddoc />
        public override void Write(string value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, string v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(uint value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, uint v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(ulong value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, ulong v)
                {
                    writer.Write(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void Write(string format, object arg0)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, string f, object a)
                {
                    writer.Write(f, a);
                }, format
                 , this.ParseObject(arg0));
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
                                                        return this.ParseObject(i);
                                                    });
            }

            this.InvokeActionForWriter(delegate(TextWriter writer, string f, object[] a)
                {
                    writer.Write(f, a);
                }, format
                 , CollectionHelper.AsArray(parsedArg) ?? new object[0]);
        }

        /// <inheriteddoc />
        public override void Write(char[] buffer, int index, int count)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, char[] b, int i, int c)
                {
                    writer.Write(b, i, c);
                }, buffer
                 , index
                 , count);
        }

        /// <inheriteddoc />
        public override void Write(string format, object arg0, object arg1)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, string f, object a0, object a1)
                {
                    writer.Write(f, a0, a1);
                }, format
                 , this.ParseObject(arg0)
                 , this.ParseObject(arg1));
        }

        /// <inheriteddoc />
        public override void WriteLine()
        {
            this.InvokeActionForWriter(delegate(TextWriter writer)
                {
                    writer.WriteLine();
                });
        }

        /// <inheriteddoc />
        public override void WriteLine(bool value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, bool v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(char value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, char v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(char[] buffer)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, char[] b)
                {
                    writer.WriteLine(b);
                }, buffer);
        }

        /// <inheriteddoc />
        public override void WriteLine(decimal value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, decimal v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(double value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, double v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(float value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, float v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(int value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, int v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(long value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, long v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(object value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, object v)
                {
                    writer.WriteLine(v);
                }, this.ParseObject(value));
        }

        /// <inheriteddoc />
        public override void WriteLine(string value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, string v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(uint value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, uint v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(ulong value)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, ulong v)
                {
                    writer.WriteLine(v);
                }, value);
        }

        /// <inheriteddoc />
        public override void WriteLine(string format, object arg0)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, string f, object a)
                {
                    writer.WriteLine(f, a);
                }, format
                 , this.ParseObject(arg0));
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
                                                        return this.ParseObject(i);
                                                    });
            }

            this.InvokeActionForWriter(delegate(TextWriter writer, string f, object[] a)
                {
                    writer.WriteLine(f, a);
                }, format
                 , CollectionHelper.AsArray(parsedArg) ?? new object[0]);
        }

        /// <inheriteddoc />
        public override void WriteLine(char[] buffer, int index, int count)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, char[] b, int i, int c)
                {
                    writer.WriteLine(b, i, c);
                }, buffer, index, count);
        }

        /// <inheriteddoc />
        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.InvokeActionForWriter(delegate(TextWriter writer, string f, object a0, object a1)
                {
                    writer.WriteLine(f, a0, a1);
                }, format
                 , this.ParseObject(arg0)
                 , this.ParseObject(arg1));
        }

        // Protected Methods (6) 

        /// <summary>
        /// Invokes an action for one or more wrapped text writers.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void InvokeActionForWriter(Action<TextWriter> action)
        {
            this.InvokeActionForWriter<object>(delegate(TextWriter writer, object value)
                                               {
                                                   action(writer);
                                               }, null);
        }

        /// <summary>
        /// Invokes an action for one or more wrapped text writers.
        /// </summary>
        /// <typeparam name="T">Type of the first additional argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="value">The first additional argument.</param>
        protected void InvokeActionForWriter<T>(Action<TextWriter, T> action, T value)
        {
            this.InvokeActionForWriter<T, object>(delegate(TextWriter w, T a1, object a2)
                                                  {
                                                      action(w, a1);
                                                  }, value, null);
        }

        /// <summary>
        /// Invokes an action for one or more wrapped text writers.
        /// </summary>
        /// <typeparam name="T1">Type of the first additional argument.</typeparam>
        /// <typeparam name="T2">Type of the second additional argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first additional argument.</param>
        /// <param name="arg2">The second additional argument.</param>
        protected void InvokeActionForWriter<T1, T2>(Action<TextWriter, T1, T2> action, T1 arg1, T2 arg2)
        {
            this.InvokeActionForWriter<T1, T2, object>(delegate(TextWriter w, T1 a1, T2 a2, object a3)
                                                       {
                                                           action(w, a1, a2);
                                                       }, arg1, arg2, null);
        }

        /// <summary>
        /// Invokes an action for one or more wrapped text writers.
        /// </summary>
        /// <typeparam name="T1">Type of the first additional argument.</typeparam>
        /// <typeparam name="T2">Type of the second additional argument.</typeparam>
        /// <typeparam name="T3">Type of the third additional argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first additional argument.</param>
        /// <param name="arg2">The second additional argument.</param>
        /// <param name="arg3">The third additional argument.</param>
        protected void InvokeActionForWriter<T1, T2, T3>(Action<TextWriter, T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            this.InvokeActionForWriter<T1, T2, T3, object>(delegate(TextWriter w, T1 a1, T2 a2, T3 a3, object a4)
                                                          {
                                                              action(w, a1, a2, a3);
                                                          }, arg1, arg2, arg3, null);
        }

        /// <summary>
        /// Invokes an action for one or more wrapped text writers.
        /// </summary>
        /// <typeparam name="T1">Type of the first additional argument.</typeparam>
        /// <typeparam name="T2">Type of the second additional argument.</typeparam>
        /// <typeparam name="T3">Type of the third additional argument.</typeparam>
        /// <typeparam name="T4">Type of the fourth additional argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first additional argument.</param>
        /// <param name="arg2">The second additional argument.</param>
        /// <param name="arg3">The third additional argument.</param>
        /// <param name="arg4">The fourth additional argument.</param>
        protected abstract void InvokeActionForWriter<T1, T2, T3, T4>(Action<TextWriter, T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Parses an object to its "real" value.
        /// </summary>
        /// <param name="obj">The object to parse.</param>
        /// <returns>The parsed object.</returns>
        protected virtual object ParseObject(object obj)
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

#if !WINDOWS_PHONE

        /// <inheriteddoc />
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            this.InvokeActionForWriter(delegate(global::System.IO.TextWriter writer, string f, object a0, object a1, object a2)
                {
                    writer.Write(f, a0, a1, a2);
                }, format
                 , this.ParseObject(arg0)
                 , this.ParseObject(arg1)
                 , this.ParseObject(arg2));
        }

#endif
#if !WINDOWS_PHONE

        /// <inheriteddoc />
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this.InvokeActionForWriter(delegate(global::System.IO.TextWriter writer, string f, object a0, object a1, object a2)
                {
                    writer.WriteLine(f, a0, a1, a2);
                }, format
                 , this.ParseObject(arg0)
                 , this.ParseObject(arg1)
                 , this.ParseObject(arg2));
        }

#endif
    }
}