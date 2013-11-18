// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// A class for own / virtual console handling.
    /// </summary>
    public static partial class TMConsole
    {
        #region Fields (9)

        private static ClearScreenAction _clearAction;
        private static GetConsoleColorFunc _getBackgroundColorProvider;
        private static GetConsoleColorFunc _getForegroundColorProvider;
        private static NewLineFunc _newLineProvider;
        private static WriteToConsoleHandler _out;
        private static ReadLineFunc _readLineProvider;
        private static SetConsoleColorHandler _setBackgroundColorProvider;
        private static SetConsoleColorHandler _setForegroundColorProvider;
        private static ToFormatArrayFunc _toFormatArray;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes the <see cref="TMConsole" /> class.
        /// </summary>
        static TMConsole()
        {
            Out = new WriteToConsoleHandler(DefaultOut);
            NewLineProvider = new NewLineFunc(DefaultNewLineProvider);

            GetForegroundColorProvider = new GetConsoleColorFunc(DefaultGetForegroundColorProvider);
            SetForegroundColorProvider = new SetConsoleColorHandler(DefaultSetForegroundColorProvider);

            GetBackgroundColorProvider = new GetConsoleColorFunc(DefaultGetBackgroundColorProvider);
            SetBackgroundColorProvider = new SetConsoleColorHandler(DefaultSetBackgroundColorProvider);

            ToFormatArray = new ToFormatArrayFunc(DefaultToFormatArray);

            ReadLineProvider = new ReadLineFunc(DefaultReadLine);

            ClearAction = new ClearScreenAction(DefaultClear);
        }

        #endregion Constructors

        #region Properties (12)

        /// <summary>
        /// Gets or sets the current console background color.
        /// </summary>
        public static ConsoleColor? BackgroundColor
        {
            get
            {
                GetConsoleColorFunc handler = GetBackgroundColorProvider;
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
        /// Gets or sets the action that clears the console.
        /// </summary>
        public static ClearScreenAction ClearAction
        {
            get { return _clearAction; }

            set { _clearAction = value; }
        }

        /// <summary>
        /// Gets or sets the current console text color.
        /// </summary>
        public static ConsoleColor? ForegroundColor
        {
            get
            {
                GetConsoleColorFunc handler = GetForegroundColorProvider;
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
        public static GetConsoleColorFunc GetBackgroundColorProvider
        {
            get { return _getBackgroundColorProvider; }

            set { _getBackgroundColorProvider = value; }
        }

        /// <summary>
        /// Gets or sets the logic to receive the text console color.
        /// </summary>
        public static GetConsoleColorFunc GetForegroundColorProvider
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
                NewLineFunc provider = NewLineProvider;
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
        public static NewLineFunc NewLineProvider
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
        /// Gets or sets the function to read a line from user inpu.
        /// </summary>
        public static ReadLineFunc ReadLineProvider
        {
            get { return _readLineProvider; }

            set { _readLineProvider = value; }
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

        /// <summary>
        /// Gets the logic that converts an object array and its values
        /// to values that should be used in a format string as parameters.
        /// </summary>
        public static ToFormatArrayFunc ToFormatArray
        {
            get { return _toFormatArray; }

            set { _toFormatArray = value; }
        }

        #endregion Properties

        #region Delegates and Events (11)

        // Delegates (11) 

        /// <summary>
        /// Describes an action that clears the console.
        /// </summary>
        public delegate void ClearScreenAction();

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
        public delegate ConsoleColor? GetConsoleColorFunc();

        /// <summary>
        /// Describes a function or method that returns the value for <see cref="TMConsole.NewLine" /> property.
        /// </summary>
        /// <returns>The new line value.</returns>
        public delegate IEnumerable<char> NewLineFunc();

        /// <summary>
        /// Describes logic for reading a line from user input.
        /// </summary>
        /// <returns>The read line.</returns>
        public delegate IEnumerable<char> ReadLineFunc();

        /// <summary>
        /// Describes a function or method to set a console color.
        /// </summary>
        /// <param name="newColor">The new value.</param>
        public delegate void SetConsoleColorHandler(ConsoleColor? newColor);

        /// <summary>
        /// Describes the method or function that converts an object array and its values
        /// to values that should be used in a format string as parameters.
        /// </summary>
        /// <param name="args">The input arguments.</param>
        /// <returns>The converted items of <paramref name="args" />.</returns>
        public delegate IEnumerable ToFormatArrayFunc(object[] args);

        /// <summary>
        /// Describes a function or method that handles writing text to a console.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="textColor">The text color. A <see langword="null" /> reference means that no color is defined.</param>
        /// <param name="backgroundColor">The background color. A <see langword="null" /> reference means that no color is defined.</param>
        public delegate void WriteToConsoleHandler(string text, ConsoleColor? textColor, ConsoleColor? backgroundColor);

        #endregion Delegates and Events

        #region Methods (33)

        // Public Methods (31) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.Clear()" />
        public static void Clear()
        {
            ClearScreenAction action = ClearAction;
            if (action != null)
            {
                action();
            }
        }

        /// <summary>
        /// Default action to clear the console.
        /// </summary>
        public static void DefaultClear()
        {
            Console.Clear();
        }

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
        /// Default logic to read a line from the user input.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<char> DefaultReadLine()
        {
            return Console.ReadLine();
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
        /// The default handler for <see cref="TMConsole.ToFormatArray" /> property.
        /// </summary>
        /// <param name="args">The input arguments.</param>
        /// <returns>The converted arguments.</returns>
        public static IEnumerable DefaultToFormatArray(object[] args)
        {
            if (args == null)
            {
                return null;
            }

            List<object> result = new List<object>();
            foreach (var a in args)
            {
                result.Add(StringHelper.AsString(a, true));
            }

            return result.ToArray();
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
        /// <param name="fgColor">The foreground color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole(ConsoleAction action,
                                           ConsoleColor? fgColor)
        {
            InvokeOnConsole(action,
                            fgColor,
                            BackgroundColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="fgColor">The foreground color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<R>(ConsoleFunc<R> func,
                                           ConsoleColor? fgColor)
        {
            return InvokeOnConsole<R>(func,
                                      fgColor,
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
        /// <param name="fgColor">The foreground color to set.</param>
        /// <param name="bgColor">The background color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole(ConsoleAction action,
                                           ConsoleColor? fgColor,
                                           ConsoleColor? bgColor)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            InvokeOnConsole<object>(new ConsoleActionWithState<object>(delegate(object state)
                {
                    action();
                }), null
                  , fgColor, bgColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="fgColor">The foreground color to set.</param>
        /// <param name="bgColor">The background color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<R>(ConsoleFunc<R> func,
                                           ConsoleColor? fgColor,
                                           ConsoleColor? bgColor)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return InvokeOnConsole<object, R>(new ConsoleFuncWithState<object, R>(delegate(object state)
                {
                    return func();
                }), null
                  , fgColor, bgColor);
        }

        /// <summary>
        /// Invokes an action for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="action" />.</param>
        /// <param name="fgColor">The foreground color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole<S>(ConsoleActionWithState<S> action, S state,
                                              ConsoleColor? fgColor)
        {
            InvokeOnConsole<S>(action, state,
                               fgColor,
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
        /// <param name="fgColor">The foreground color to set.</param>
        /// <param name="bgColor">The background color to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        public static void InvokeOnConsole<S>(ConsoleActionWithState<S> action, S state,
                                              ConsoleColor? fgColor,
                                              ConsoleColor? bgColor)
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
                  , fgColor, bgColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="func" />.</param>
        /// <param name="fgColor">The foreground color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<S, R>(ConsoleFuncWithState<S, R> func, S state,
                                              ConsoleColor? fgColor)
        {
            return InvokeOnConsole<S, R>(func, state,
                                         fgColor,
                                         BackgroundColor);
        }

        /// <summary>
        /// Invokes a function for a specific console color. After invokation the old color values are restored.
        /// </summary>
        /// <typeparam name="S">Type of <paramref name="state" />.</typeparam>
        /// <typeparam name="R">The result type.</typeparam>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="state">The value for the first parameter of <paramref name="func" />.</param>
        /// <param name="fgColor">The foreground color to set.</param>
        /// <param name="bgColor">The background color to set.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <see langword="null" />.</exception>
        public static R InvokeOnConsole<S, R>(ConsoleFuncWithState<S, R> func, S state,
                                              ConsoleColor? fgColor,
                                              ConsoleColor? bgColor)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            ConsoleColor? oldTextColor = ForegroundColor;
            ConsoleColor? oldBgColor = BackgroundColor;

            try
            {
                ForegroundColor = fgColor;
                BackgroundColor = bgColor;

                return func(state);
            }
            finally
            {
                BackgroundColor = oldBgColor;
                ForegroundColor = oldTextColor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.ReadLine()" />
        public static string ReadLine()
        {
            ReadLineFunc provider = ReadLineProvider;
            if (provider != null)
            {
                return StringHelper.AsString(provider());
            }

            return null;
        }

        /// <summary>
        /// Sets the value for <see cref="TMConsole.NewLine" /> property.
        /// </summary>
        /// <param name="value">The new value.</param>
        public static void SetNewLine(IEnumerable<char> value)
        {
            NewLineProvider = new NewLineFunc(delegate()
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
            Write("{0}", new object[] { obj });
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
                                ConvertToFormatArray(args ?? new object[] { null })));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.WriteLine()" />
        public static void WriteLine()
        {
            Write(NewLine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Console.WriteLine(object)" />
        public static void WriteLine(object obj)
        {
            WriteLine("{0}", new object[] { obj });
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
                                    ConvertToFormatArray(args ?? new object[] { null })));
        }
        // Private Methods (2) 

        private static object[] ConvertToFormatArray(object[] input)
        {
            ToFormatArrayFunc handler = ToFormatArray;
            if (handler == null)
            {
                return null;
            }

            IEnumerable result = handler(input);
            if (result == null)
            {
                return null;
            }

            object[] resultArray = result as object[];
            if (resultArray == null)
            {
                List<object> temp = new List<object>();
                foreach (object a in result)
                {
                    temp.Add(a);
                }
            }

            return resultArray;
        }

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
