// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A basic console.
    /// </summary>
    public abstract partial class ConsoleBase : TMObject, IConsole
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected ConsoleBase(object syncRoot)
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleBase" /> class.
        /// </summary>
        protected ConsoleBase()
            : base()
        {
        }

        #endregion Constructors

        #region Methods (13)

        // Public Methods (8) 

        /// <inheriteddoc />
        public string ReadLine()
        {
            using (StringWriter writer = new StringWriter())
            {
                this.OnReadLine(writer);

                return writer.ToString();
            }
        }

        /// <inheriteddoc />
        public ConsoleBase Write(object obj)
        {
            return this.Write(this.ToConsoleString(obj));
        }

        /// <inheriteddoc />
        public ConsoleBase Write(IEnumerable<char> chars)
        {
            this.OnWrite(StringHelper.AsString(chars));

            return this;
        }

        /// <inheriteddoc />
        public ConsoleBase Write(IEnumerable<char> format, params object[] args)
        {
            return this.Write(string.Format(StringHelper.AsString(format) ?? string.Empty,
                                            CollectionHelper.AsArray(this.ToConsoleArguments(args))));
        }

        /// <inheriteddoc />
        public ConsoleBase WriteLine()
        {
            return this.Write(this.GetNewLineForOutput());
        }

        /// <inheriteddoc />
        public ConsoleBase WriteLine(object obj)
        {
            return this.Write(string.Format("{0}{1}",
                                            this.ToConsoleString(obj),
                                            this.GetNewLineForOutput()));
        }

        /// <inheriteddoc />
        public ConsoleBase WriteLine(IEnumerable<char> chars)
        {
            return this.Write(string.Format("{0}{1}",
                                            StringHelper.AsString(chars),
                                            this.GetNewLineForOutput()));
        }

        /// <inheriteddoc />
        public ConsoleBase WriteLine(IEnumerable<char> format, params object[] args)
        {
            return this.Write(string.Format("{0}{1}",
                                            string.Format(StringHelper.AsString(format) ?? string.Empty,
                                                          CollectionHelper.AsArray(this.ToConsoleArguments(args))),
                                            this.GetNewLineForOutput()));
        }

        // Protected Methods (5) 

        /// <summary>
        /// Returns the expression that represents a new line for console output.
        /// </summary>
        /// <returns>Th expression that represents a new line.</returns>
        protected virtual string GetNewLineForOutput()
        {
            return Environment.NewLine;
        }

        /// <summary>
        /// The logic for <see cref="ConsoleBase.ReadLine()" /> method.
        /// </summary>
        /// <param name="line">The target where to write the read line data to.</param>
        protected abstract void OnReadLine(TextWriter line);

        /// <summary>
        /// The logic for <see cref="ConsoleBase.Write(IEnumerable{char})" /> method.
        /// </summary>
        /// <param name="text">The text to write.</param>
        protected abstract void OnWrite(string text);

        /// <summary>
        /// Converts the items of a sequence of objects to a sequence of console arguments.
        /// </summary>
        /// <param name="args">The input arguments.</param>
        /// <returns>The parsed argument list.</returns>
        protected virtual IEnumerable ToConsoleArguments(IEnumerable args)
        {
            if (args == null)
            {
                return new object[] { null };
            }

            List<object> result = new List<object>();
            CollectionHelper.AddRange(result,
                                      CollectionHelper.Select(CollectionHelper.Cast<object>(args),
                                                              delegate(object a)
                                                              {
                                                                  return (object)StringHelper.AsString(this.ToConsoleString(a));
                                                              }));

            return result.ToArray();
        }

        /// <summary>
        /// Converts an object to a char sequence for console.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <returns>The converted object.</returns>
        protected virtual IEnumerable<char> ToConsoleString(object obj)
        {
            return StringHelper.AsString(obj, true);
        }

        #endregion Methods
    }
}