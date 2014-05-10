// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.IO;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that uses an <see cref="IConsole" /> instance.
    /// </summary>
    public sealed class ConsoleLogger : LoggerFacadeBase
    {
        #region Fields (1)

        private readonly IConsole _CONSOLE;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger" /> class.
        /// </summary>
        /// <param name="console">
        /// The value for the <see cref="ConsoleLogger.Console" /> property.
        /// If the value is a <see langword="null" /> reference, the value that is provided by the
        /// <see cref="GlobalConsole.Current" /> property is used.
        /// </param>
        public ConsoleLogger(IConsole console)
            : base(false)
        {
            this._CONSOLE = console;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger" /> class.
        /// </summary>
        /// <remarks>The logger uses the instance that is provided by the <see cref="GlobalConsole.Current" /> property.</remarks>
        public ConsoleLogger()
            : this(null)
        {
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the console instance that should be used by that logger.
        /// </summary>
        public IConsole Console
        {
            get { return this._CONSOLE ?? GlobalConsole.Current; }
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            IConsole c = this.Console;
            if (c == null)
            {
                return;
            }

            ConsoleColor? oldForeColor = c.ForegroundColor;
            ConsoleColor? oldBgColor = c.BackgroundColor;

            try
            {
                ConsoleColor? foreColor = oldForeColor;
                ConsoleColor? bgColor = oldBgColor;

                IList<LoggerFacadeCategories> categories = msg.Categories;
                if (categories != null)
                {
                    bgColor = ConsoleColor.Black;
#if !DEBUG

                    if (categories.Contains(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.LoggerFacadeCategories.Debug))
                    {
                        return;
                    }
#endif

                    if (categories.Contains(LoggerFacadeCategories.FatalErrors))
                    {
                        foreColor = ConsoleColor.Yellow;
                        bgColor = ConsoleColor.Red;
                    }
                    else if (categories.Contains(LoggerFacadeCategories.Errors))
                    {
                        foreColor = ConsoleColor.Red;
                    }
                    else if (categories.Contains(LoggerFacadeCategories.Warnings))
                    {
                        foreColor = ConsoleColor.Yellow;
                    }
#if DEBUG
                    else if (categories.Contains(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.LoggerFacadeCategories.Assert))
                    {
                        foreColor = global::System.ConsoleColor.White;
                        bgColor = global::System.ConsoleColor.Blue;
                    }
#endif
                    else if (categories.Contains(LoggerFacadeCategories.Information))
                    {
                        foreColor = ConsoleColor.White;
                    }
                    else if (categories.Contains(LoggerFacadeCategories.Verbose))
                    {
                        foreColor = ConsoleColor.Gray;
                    }
                    else if (categories.Contains(LoggerFacadeCategories.Trace))
                    {
                        foreColor = ConsoleColor.DarkGray;
                    }

                    c.ForegroundColor = foreColor;
                    c.BackgroundColor = bgColor;

                    c.WriteLine("[{0}] {1}", msg.Time.ToString("yyyy-MM-dd HH:mm:ss.fff zzz")
                                           , msg.LogTag)
                     .WriteLine(StringHelper.AsString(msg.Message))
                     .WriteLine();
                }
            }
            finally
            {
                c.ForegroundColor = oldForeColor;
                c.BackgroundColor = oldBgColor;
            }
        }

        #endregion Methods
    }
}