// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    /// <summary>
    /// Describes an object that executes a script.
    /// </summary>
    public interface IScriptExecutor : ITMDisposable
    {
        #region Operations (7)

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="src">The source code of the script.</param>
        /// <param name="autoStart">Start script execution automatically or not.</param>
        /// <param name="debug">Enbale debug mode.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ObjectDisposedException">Object has already been disposed.</exception>
        IScriptExecutionContext Execute(IEnumerable<char> src,
                                        bool autoStart = true,
                                        bool debug = false);

        /// <summary>
        /// Exposes a type.
        /// </summary>
        /// <typeparam name="T">Type to expose.</typeparam>
        /// <returns>That instance.</returns>
        /// <exception cref="ObjectDisposedException">Object has already been disposed.</exception>
        IScriptExecutor ExposeType<T>();

        /// <summary>
        /// Exposes a type.
        /// </summary>
        /// <typeparam name="T">Type to expose.</typeparam>
        /// <param name="alias">The name of the type to use in the script.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ObjectDisposedException">Object has already been disposed.</exception>
        IScriptExecutor ExposeType<T>(IEnumerable<char> alias);

        /// <summary>
        /// Exposes a type.
        /// </summary>
        /// <param name="type">Type to expose.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
        /// <exception cref="ObjectDisposedException">Object has already been disposed.</exception>
        IScriptExecutor ExposeType(Type type);

        /// <summary>
        /// Exposes a type.
        /// </summary>
        /// <param name="type">Type to expose.</param>
        /// <param name="alias">he name of the type to use in the script.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
        /// <exception cref="ObjectDisposedException">Object has already been disposed.</exception>
        IScriptExecutor ExposeType(Type type, IEnumerable<char> alias);

        /// <summary>
        /// Sets a global function.
        /// </summary>
        /// <param name="funcName">The name of the function.</param>
        /// <param name="func">The (new) function.</param>
        /// <returns>That instance.</returns>
        IScriptExecutor SetFunction(IEnumerable<char> funcName, Delegate func);

        /// <summary>
        /// Sets a global variable.
        /// </summary>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="value">The (new) value for the variable.</param>
        /// <returns>That instance.</returns>
        IScriptExecutor SetVariable(IEnumerable<char> varName, object value);

        #endregion Operations
    }
}
