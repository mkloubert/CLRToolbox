// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Scripting;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (5)

        // Public Methods (5) 

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed action.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor>(this TExecutor executor, IEnumerable<char> actionName, Action action)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, actionName, action);
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed action.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T">Type of the first argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T>(this TExecutor executor, IEnumerable<char> actionName, Action<T> action)
            where TExecutor : IScriptExecutor
        {
            ScriptHelper.SetAction<T>(executor, actionName, action);
            return executor;
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed action.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2> action)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, actionName, action);
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed action.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T3">Type of the third argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3> action)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, actionName, action);
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed action.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T3">Type of the third argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T4">Type of the fourth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4> action)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, actionName, action);
        }

        #endregion Methods
    }
}
