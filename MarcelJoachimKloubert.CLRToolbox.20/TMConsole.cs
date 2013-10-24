// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// A class for own / virtual console handling.
    /// </summary>
    public static partial class TMConsole
    {
        #region Fields (6)

        private static GetConsoleColorHandler _getBackgroundColorProvider;
        private static GetConsoleColorHandler _getForegroundColorProvider;
        private static NewLineHandler _newLineProvider;
        private static WriteToConsoleHandler _out;
        private static SetConsoleColorHandler _setBackgroundColorProvider;
        private static SetConsoleColorHandler _setForegroundColorProvider;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes the <see cref="TMConsole" /> class.
        /// </summary>
        static TMConsole()
        {
            Out = new WriteToConsoleHandler(DefaultOut);
            NewLineProvider = DefaultNewLineProvider;

            GetForegroundColorProvider = DefaultGetForegroundColorProvider;
            SetForegroundColorProvider = DefaultSetForegroundColorProvider;

            GetBackgroundColorProvider = DefaultGetBackgroundColorProvider;
            SetBackgroundColorProvider = DefaultSetBackgroundColorProvider;
        }

        #endregion Constructors

        #region Properties (9)

        /// <summary>
        /// Gets or sets the current console background color.
        /// </summary>
        public static ConsoleColor? BackgroundColor
        {
            get
            {
                GetConsoleColorHandler handler = GetBackgroundColorProvider;
                if (handler != null)
                {
                    return handler();
                }

                return null;
            }

            set
            {
                SetConsoleColorHandler handler = SetBackgroundColorProvider;
                if (handler != null)
                {
                    handler(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current console text color.
        /// </summary>
        public static ConsoleColor? ForegroundColor
        {
            get
            {
                GetConsoleColorHandler handler = GetForegroundColorProvider;
                if (handler != null)
                {
                    return handler();
                }

                return null;
            }

            set
            {
                SetConsoleColorHandler handler = SetForegroundColorProvider;
                if (handler != null)
                {
                    handler(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the logic to receive the background console color.
        /// </summary>
        public static GetConsoleColorHandler GetBackgroundColorProvider
        {
            get { return _getBackgroundColorProvider; }

            set { _getBackgroundColorProvider = value; }
        }

        /// <summary>
        /// Gets or sets the logic to receive the text console color.
        /// </summary>
        public static GetConsoleColorHandler GetForegroundColorProvider
        {
            get { return _getForegroundColorProvider; }

            set { _getForegroundColorProvider = value; }
        }

        /// <summary>
        /// Gets the expression for the new line.
        /// </summary>
        public static string NewLine
        {
            get
            {
                NewLineHandler provider = NewLineProvider;
                if (provider != null)
                {
                    return StringHelper.AsString(provider());
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the provider that return the value for <see cref="TMConsole.NewLine" /> property.
        /// </summary>
        public static NewLineHandler NewLineProvider
        {
            get { return _newLineProvider; }

            set { _newLineProvider = value; }
        }

        /// <summary>
        /// Gets or sets the handler 
        /// </summary>
        public static WriteToConsoleHandler Out
        {
            get { return _out; }

            set { _out = value; }
        }

        /// <summary>
        /// Gets or sets the logic to set the background console color.
        /// </summary>
        public static SetConsoleColorHandler SetBackgroundColorProvider
        {
            get { return _setBackgroundColorProvider; }

            set { _setBackgroundColorProvider = value; }
        }

        /// <summary>
        /// Gets or sets the logic to set the text console color.
        /// </summary>
        public static SetConsoleColorHandler SetForegroundColorProvider
        {
            get { return _setForegroundColorProvider; }

            set { _setForegroundColorProvider = value; }
        }

        #endregion Properties

        #region Delegates and Events (8)

        // Delegates (8) 

        /// <summary>
        /// Describes an action for invokation in console context.
        /// </summary>
        public delegate void ConsoleAction();
        /// <summary>
        /// Describes an action for invokation in console context.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <param name="state">The additional object for the action.</param>
        public delegate void ConsoleActionWithState<S>(S state);
        /// <summary>
        /// Describes a function for invokation in console context.
        /// </summary>
        /// <typeparam name="R">Type of the result.</typeparam>
        /// <returns>The result of the function.</returns>
        public delegate R ConsoleFunc<R>();
        /// <summary>
        /// Describes an action for invokation in console context.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <typeparam name="R">Type of the result.</typeparam>
        /// <param name="state">The additional object for the action.</param>
        /// <returns>The result of the function.</returns>
        public delegate R ConsoleFuncWithState<S, R>(S state);
        /// <summary>
        /// Describes a function or method to receive a console color.
        /// </summary>
        /// <returns>The console color.</returns>
        public delegate ConsoleColor? GetConsoleColorHandler();
        /// <summary>
        /// Describes a function or method that returns the value for <see cref="TMConsole.NewLine" /> property.
        /// </summary>
        /// <returns>The new line value.</returns>
        public delegate IEnumerable<char> NewLineHandler();
        /// <summary>
        /// Describes a function or method to set a console color.
        /// </summary>
        /// <param name="newColor">The new value.</param>
        public delegate void SetConsoleColorHandler(ConsoleColor? newColor);
        /// <summary>
        /// Describes a function or method that handles writing text to a console.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="textColor">The text color. A <see langword="null" /> reference means that no color is defined.</param>
        /// <param name="backgroundColor">The background color. A <see langword="null" /> reference means that no color is defined.</param>
        public delegate void WriteToConsoleHandler(string text, ConsoleColor? textColor, ConsoleColor? backgroundColor);

        #endregion Delegates and Events

        #region Methods (26)

        // Public Methods (25) 

        /// <summary>
        /// Default logic to get the background color.
        /// </summary>
        /// <returns>The background color.</returns>
        public static ConsoleColor? DefaultGetBackgroundColorProvider()
        {
            return Console.BackgroundColor;
        }

        /// <summary>
        /// Default logic to get the text color.
        /// </summary>
        /// <returns>The text color.</returns>
        public static ConsoleColor? DefaultGetForegroundColorProvider()
        {
            return Console.ForegroundColor;
        }

        /// <summary>
        /// Gets the default logic for returing the value for <see cref="TMConsole.NewLine" />.
        /// </summary>
        /// <returns>The new line value.</returns>
        public static IEnumerable<char> DefaultNewLineProvider()
        {
            return Console.Out.NewLine;
        }

        /// <summary>
        /// The default logic for writing text to <see cref="Console.Out" />.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="textColor">The text color.</param>
        /// <param name="backgroundColor"></param>
        public static void DefaultOut(string text, ConsoleColor? textColor, ConsoleColor? backgroundColor)
        {
            if (textColor.HasValue)
            {
                Console.ForegroundColor = textColor.Value;
            }

            if (backgroundColor.HasValue)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }

            Console.Write(text);
        }

        /// <summary>
        /// Default logic to set the background color.
        /// </summary>
        /// <param name="newColor">The new value.</param>
        public static void DefaultSetBackgroundColorProvider(ConsoleColor? newColor)
        {
            if (newColor.HasValue)
            {
                Console.BackgroundColor = newColor.Value;
            }
        }

        /// <summary>
        /// Default logic to set the text color.
        /// </summary>
        /// <param name="newColor">The new value.</param>
        public static void DefaultSetForegroundColorProvider(ConsoleColor? newColor)
        {
            if (newColor.HasValue)
            {
                Console.ForegroundColor = newColor.Value;
            }
        }

        /// <summary>
        /// Invokes an action for the console.
        /// </summary>
        /// <param name="action">The logic to invoke.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole(ConsoleAction action)
        {
            InvokeOnConsole(action,
                            ForegroundColor,
                            BackgroundColor);
        }

        /// <summary>
        /// Invokes a function for athe console.
        /// </summary>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<R>(ConsoleFunc<R> func)
        {
            return InvokeOnConsole<R>(func,
                                      ForegroundColor,
                                      BackgroundColor);
        }

        /// <summary>
        /// Invokes an action for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole(ConsoleAction action,
                                           ConsoleColor? foregroundColor)
        {
            InvokeOnConsole(action,
                            foregroundColor,
                            BackgroundColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<R>(ConsoleFunc<R> func,
                                           ConsoleColor? foregroundColor)
        {
            return InvokeOnConsole<R>(func,
                                      foregroundColor,
                                      BackgroundColor);
        }

        /// <summary>
        /// Invokes a function for the console.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole<S>(ConsoleActionWithState<S> action, S state)
        {
            InvokeOnConsole<S>(action, state,
                               ForegroundColor,
                               BackgroundColor);
        }

        /// <summary>
        /// Invokes an action for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <param name="backgroundColor">The background color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole(ConsoleAction action,
                                           ConsoleColor? foregroundColor,
                                           ConsoleColor? backgroundColor)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            InvokeOnConsole<object>(new ConsoleActionWithState<object>(delegate(object state)
                {
                    action();
                }), null
                  , foregroundColor, backgroundColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <param name="backgroundColor">The background color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<R>(ConsoleFunc<R> func,
                                           ConsoleColor? foregroundColor,
                                           ConsoleColor? backgroundColor)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return InvokeOnConsole<object, R>(new ConsoleFuncWithState<object, R>(delegate(object state)
                {
                    return func();
                }), null
                  , foregroundColor, backgroundColor);
        }

        /// <summary>
        /// Invokes an action for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="action" />.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole<S>(ConsoleActionWithState<S> action, S state,
                                              ConsoleColor? foregroundColor)
        {
            InvokeOnConsole<S>(action, state,
                               foregroundColor,
                               BackgroundColor);
        }

        /// <summary>
        /// Invokes a function for the console.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<S, R>(ConsoleFuncWithState<S, R> func, S state)
        {
            return InvokeOnConsole<S, R>(func, state,
                                         ForegroundColor,
                                         BackgroundColor);
        }

        /// <summary>
        /// Invokes an action for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="action" />.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <param name="backgroundColor">The background color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole<S>(ConsoleActionWithState<S> action, S state,
                                              ConsoleColor? foregroundColor,
                                              ConsoleColor? backgroundColor)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            InvokeOnConsole<S, object>(new ConsoleFuncWithState<S, object>(delegate(S actionState)
                {
                    action(actionState);
                    return null;
                }), state
                  , foregroundColor, backgroundColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="func" />.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<S, R>(ConsoleFuncWithState<S, R> func, S state,
                                              ConsoleColor? foregroundColor)
        {
            return InvokeOnConsole<S, R>(func, state,
                                         foregroundColor,
                                         BackgroundColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="func" />.</param>
        /// <param name="foregroundColor">The foreground color to set.</param>
        /// <param name="backgroundColor">The background color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<S, R>(ConsoleFuncWithState<S, R> func, S state,
                                              ConsoleColor? foregroundColor,
                                              ConsoleColor? backgroundColor)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            ConsoleColor? oldTextColor = ForegroundColor;
            ConsoleColor? oldBgColor = BackgroundColor;

            try
            {
                ForegroundColor = foregroundColor;
                BackgroundColor = backgroundColor;

                return func(state);
            }
            finally
            {
                BackgroundColor = oldBgColor;
                ForegroundColor = oldTextColor;
            }
        }

        /// <summary>
        /// Sets the value for <see cref="TMConsole.NewLine" /> property.
        /// </summary>
        /// <param name="value">The new value.</param>
        public static void SetNewLine(IEnumerable<char> value)
        {
            NewLineProvider = new NewLineHandler(delegate()
                {
                    return value;
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.Write(object)" />
        public static void Write(object obj)
        {
            Write(string.Format("{0}",
                                obj is IEnumerable<char> ? StringHelper.AsString(obj) : obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.Write(string)" />
        public static void Write(IEnumerable<char> text)
        {
            WriteToConsole(StringHelper.AsString(text));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.Write(string, object[])" />
        public static void Write(IEnumerable<char> format, params object[] args)
        {
            Write(string.Format(StringHelper.AsString(format),
                                args ?? new object[] { null }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.WriteLine(object)" />
        public static void WriteLine(object obj)
        {
            Write((object)string.Format("{0}",
                                        obj is IEnumerable<char> ? StringHelper.AsString(obj) : obj,
                                        NewLine));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.WriteLine(string)" />
        public static void WriteLine(IEnumerable<char> text)
        {
            Write(string.Format("{0}{1}",
                                StringHelper.AsString(text),
                                NewLine));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.WriteLine(string, object[])" />
        public static void WriteLine(IEnumerable<char> format, params object[] args)
        {
            WriteLine(string.Format(StringHelper.AsString(format),
                                    args ?? new object[] { null }));
        }
        // Private Methods (1) 

        private static void WriteToConsole(string text)
        {
            WriteToConsoleHandler @out = Out;
            if (@out != null)
            {
                @out(text, ForegroundColor, BackgroundColor);
            }
        }

        #endregion Methods
    }
}
