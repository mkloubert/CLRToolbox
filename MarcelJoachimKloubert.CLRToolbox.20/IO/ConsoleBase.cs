// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A basic console.
    /// </summary>
    public abstract partial class ConsoleBase : TMObject, IConsole
    {
        #region Fields (2)

        private ConsoleColor? _backgroundColor;
        private ConsoleColor? _foregroundColor;

        #endregion Fields

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

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.BackgroundColor" />
        public virtual ConsoleColor? BackgroundColor
        {
            get { return this._backgroundColor; }

            set { this._backgroundColor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.ForegroundColor" />
        public virtual ConsoleColor? ForegroundColor
        {
            get { return this._foregroundColor; }

            set { this._foregroundColor = value; }
        }

        #endregion Properties

        #region Methods (15)

        // Public Methods (9) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.Clear()" />
        public ConsoleBase Clear()
        {
            lock (this._SYNC)
            {
                this.OnClear();
                return this;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.ReadLine()" />
        public string ReadLine()
        {
            lock (this._SYNC)
            {
                using (var writer = new StringWriter())
                {
                    this.OnReadLine(writer);

                    return writer.ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.Write(object)" />
        public ConsoleBase Write(object obj)
        {
            return this.Write(this.ToConsoleString(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.Write(IEnumerable{char})" />
        public ConsoleBase Write(IEnumerable<char> chars)
        {
            lock (this._SYNC)
            {
                this.OnWrite(StringHelper.AsString(chars));

                return this;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.Write(IEnumerable{char}, object[])" />
        public ConsoleBase Write(IEnumerable<char> format, params object[] args)
        {
            return this.Write(string.Format(StringHelper.AsString(format) ?? string.Empty,
                                            this.ToConsoleArguments(CollectionHelper.AsArray(args))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.WriteLine()" />
        public ConsoleBase WriteLine()
        {
            return this.Write(this.GetNewLineForOutput());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.WriteLine(object)" />
        public ConsoleBase WriteLine(object obj)
        {
            return this.Write(string.Format("{0}{1}",
                                            this.ToConsoleString(obj),
                                            this.GetNewLineForOutput()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.WriteLine(IEnumerable{char})" />
        public ConsoleBase WriteLine(IEnumerable<char> chars)
        {
            return this.Write(string.Format("{0}{1}",
                                            StringHelper.AsString(chars),
                                            this.GetNewLineForOutput()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.WriteLine(IEnumerable{char}, object[])" />
        public ConsoleBase WriteLine(IEnumerable<char> format, params object[] args)
        {
            return this.Write(string.Format("{0}{1}",
                                            string.Format(StringHelper.AsString(format) ?? string.Empty,
                                                          this.ToConsoleArguments(CollectionHelper.AsArray(args))),
                                            this.GetNewLineForOutput()));
        }
        // Protected Methods (6) 

        /// <summary>
        /// Returns the expression that represents a new line for console output.
        /// </summary>
        /// <returns>Th expression that represents a new line.</returns>
        protected virtual string GetNewLineForOutput()
        {
            return Environment.NewLine;
        }

        /// <summary>
        /// The logic for <see cref="ConsoleBase.Clear()" /> method.
        /// </summary>
        protected abstract void OnClear();

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
            foreach (object a in args)
            {
                result.Add(this.ToConsoleString(a));
            }

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
