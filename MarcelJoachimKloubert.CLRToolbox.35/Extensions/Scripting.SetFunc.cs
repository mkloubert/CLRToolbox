// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Scripting;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    partial class ScriptingExtensionMethods
    {
        #region Methods (5)

        // Public Methods (5) 

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed func.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<TResult> func)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, funcName, func);
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed func.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T">Type of the first argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T, TResult> func)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, funcName, func);
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed func.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, TResult> func)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, funcName, func);
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed func.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T3">Type of the third argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, TResult> func)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, funcName, func);
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed func.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T3">Type of the third argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T4">Type of the fourth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, TResult> func)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, funcName, func);
        }

        #endregion Methods
    }
}
