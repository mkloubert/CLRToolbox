// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    #region CLASS:CommonScriptFunctionSetup<TExecutor>

    /// <summary>
    /// A helper class for setup a <see cref="IScriptExecutor" /> with common functions.
    /// </summary>
    /// <typeparam name="TExecutor">Type of the underlying executor.</typeparam>
    public class CommonScriptFunctionSetup<TExecutor>
        where TExecutor : global::MarcelJoachimKloubert.CLRToolbox.Scripting.IScriptExecutor
    {
        #region Fields (6)

        private TExecutor _executor;

        /// <summary>
        /// The common name of a function that shows an alert message at the standard output.
        /// </summary>
        public const string COMMON_FUNCNAME_ALERT = "alert";

        /// <summary>
        /// The common name of a function that clears the standard output.
        /// </summary>
        public const string COMMON_FUNCNAME_CLEAR_SCREEN = "cls";

        /// <summary>
        /// The common name of a function that reads a text line from the standard input.
        /// </summary>
        public const string COMMON_FUNCNAME_READ_LINE = "readline";

        /// <summary>
        /// The common name of a function that writes to the standard output.
        /// </summary>
        public const string COMMON_FUNCNAME_WRITE = "write";

        /// <summary>
        /// The common name of a function that writes to the standard output with adding a new line expression.
        /// </summary>
        public const string COMMON_FUNCNAME_WRITE_LINE = "writeline";

        #endregion Fields

        #region Constructors (1)

        private CommonScriptFunctionSetup()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying script executor.
        /// </summary>
        public TExecutor Executor
        {
            get { return this._executor; }
        }

        #endregion Properties

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// Describes an action without parameters.
        /// </summary>
        public delegate void NoParamAction();

        /// <summary>
        /// Describes a function without parameters.
        /// </summary>
        /// <returns>The result of the function.</returns>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        public delegate TResult NoParamFunc<TResult>();

        #endregion Delegates and Events

        #region Methods (8)

        // Public Methods (8) 

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="executor">The value for <see cref="CommonScriptFunctionSetup{TExecutor}.Executor" /> property.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// <see cref="CommonScriptFunctionSetup{TExecutor}.SetupAll()" /> method is directly called.
        /// </remarks>
        public static CommonScriptFunctionSetup<TExecutor> Create(TExecutor executor)
        {
            return Create(executor, true);
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="executor">The value for <see cref="CommonScriptFunctionSetup{TExecutor}.Executor" /> property.</param>
        /// <param name="setupAll">
        /// Directly call <see cref="CommonScriptFunctionSetup{TExecutor}.SetupAll()" /> method or not.
        /// </param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static CommonScriptFunctionSetup<TExecutor> Create(TExecutor executor,
                                                                  bool setupAll)
        {
            if (executor == null)
            {
                throw new ArgumentNullException("executor");
            }

            CommonScriptFunctionSetup<TExecutor> result = new CommonScriptFunctionSetup<TExecutor>();
            result._executor = executor;

            if (setupAll)
            {
                result.SetupAll();
            }

            return result;
        }

        /// <summary>
        /// Sets the logic to clear the standard output.
        /// </summary>
        /// <returns>That instance.</returns>
        public virtual CommonScriptFunctionSetup<TExecutor> SetAlertFunc()
        {
            this.Executor
                .SetFunction(COMMON_FUNCNAME_ALERT,
                             new ScriptExecutorBase.SimpleAction(delegate(object[] args)
                             {
                                 if (args == null)
                                 {
                                     return;
                                 }

                                 foreach (object a in args)
                                 {
                                     ConsoleColor? oldTxtColor = GlobalConsole.Current.ForegroundColor;
                                     ConsoleColor? oldBgColor = GlobalConsole.Current.BackgroundColor;

                                     try
                                     {
                                         GlobalConsole.Current.ForegroundColor = ConsoleColor.White;
                                         GlobalConsole.Current.BackgroundColor = ConsoleColor.Black;

                                         GlobalConsole.Current.WriteLine(a);
                                     }
                                     finally
                                     {
                                         GlobalConsole.Current.BackgroundColor = oldBgColor;
                                         GlobalConsole.Current.ForegroundColor = oldTxtColor;
                                     }
                                 }
                             }));

            return this;
        }

        /// <summary>
        /// Sets the logic to clear the standard output.
        /// </summary>
        /// <returns>That instance.</returns>
        public virtual CommonScriptFunctionSetup<TExecutor> SetClearScreenFunc()
        {
            this.Executor
                .SetFunction(COMMON_FUNCNAME_CLEAR_SCREEN,
                             new NoParamAction(delegate()
                             {
                                 GlobalConsole.Current.Clear();
                             }));

            return this;
        }

        /// <summary>
        /// Sets the logic to read a text line from the standard input.
        /// </summary>
        /// <returns>That instance.</returns>
        public virtual CommonScriptFunctionSetup<TExecutor> SetReadLineFunc()
        {
            this.Executor
                .SetFunction(COMMON_FUNCNAME_READ_LINE,
                             new NoParamFunc<string>(delegate()
                             {
                                 return GlobalConsole.Current.ReadLine();
                             }));

            return this;
        }

        /// <summary>
        /// Sets up the underlying executor with all command functions.
        /// </summary>
        /// <returns>That instance.</returns>
        public virtual CommonScriptFunctionSetup<TExecutor> SetupAll()
        {
            return this.SetAlertFunc()
                       .SetClearScreenFunc()
                       .SetReadLineFunc()
                       .SetWriteFunc()
                       .SetWriteLineFunc();
        }

        /// <summary>
        /// Sets the logic to write to the standard output.
        /// </summary>
        /// <returns>That instance.</returns>
        public virtual CommonScriptFunctionSetup<TExecutor> SetWriteFunc()
        {
            this.Executor
                .SetFunction(COMMON_FUNCNAME_WRITE,
                             new ScriptExecutorBase.SimpleAction(delegate(object[] args)
                             {
                                 if (args == null)
                                 {
                                     return;
                                 }

                                 if (args.Length == 1)
                                 {
                                     GlobalConsole.Current.Write(args[0]);
                                 }
                                 else if (args.Length > 1)
                                 {
                                     string format = StringHelper.AsString(args[0], true);

                                     object[] formatArgs = new object[args.Length - 1];
                                     Array.Copy(args, 1, formatArgs, 0, formatArgs.Length);

                                     GlobalConsole.Current.Write(format, formatArgs);
                                 }
                             }));

            return this;
        }

        /// <summary>
        /// Sets the logic to write to the standard output with adding a new line expression.
        /// </summary>
        /// <returns>That instance.</returns>
        public virtual CommonScriptFunctionSetup<TExecutor> SetWriteLineFunc()
        {
            this.Executor
                .SetFunction(COMMON_FUNCNAME_WRITE_LINE,
                             new ScriptExecutorBase.SimpleAction(delegate(object[] args)
                             {
                                 if (args == null)
                                 {
                                     return;
                                 }

                                 if (args.Length < 1)
                                 {
                                     GlobalConsole.Current.WriteLine();
                                 }
                                 else if (args.Length == 1)
                                 {
                                     GlobalConsole.Current.WriteLine(args[0]);
                                 }
                                 else if (args.Length > 1)
                                 {
                                     string format = StringHelper.AsString(args[0], true);

                                     object[] formatArgs = new object[args.Length - 1];
                                     Array.Copy(args, 1, formatArgs, 0, formatArgs.Length);

                                     GlobalConsole.Current.WriteLine(format, formatArgs);
                                 }
                             }));

            return this;
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: CommonScriptFunctionSetup

    /// <summary>
    /// Factory class for generating <see cref="CommonScriptFunctionSetup{TExecutor}" /> instances.
    /// </summary>
    public static class CommonScriptFunctionSetup
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CommonScriptFunctionSetup{TExecutor}.Create(TExecutor)" />
        public static CommonScriptFunctionSetup<TExecutor> Create<TExecutor>(TExecutor executor)
            where TExecutor : global::MarcelJoachimKloubert.CLRToolbox.Scripting.IScriptExecutor
        {
            return CommonScriptFunctionSetup<TExecutor>.Create(executor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CommonScriptFunctionSetup{TExecutor}.Create(TExecutor, bool)" />
        public static CommonScriptFunctionSetup<TExecutor> Create<TExecutor>(TExecutor executor,
                                                                             bool setupAll)
            where TExecutor : global::MarcelJoachimKloubert.CLRToolbox.Scripting.IScriptExecutor
        {
            return CommonScriptFunctionSetup<TExecutor>.Create(executor,
                                                               setupAll);
        }

        #endregion Methods
    }

    #endregion
}
