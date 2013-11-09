// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Scripting;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    partial class ScriptingExtensionMethods
    {
        #region Methods (12)

        // Public Methods (12) 

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed func.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T3">Type of the third argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T4">Type of the fourth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T14">Type of the forteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T14">Type of the forteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T15">Type of the fifteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T14">Type of the forteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T15">Type of the fifteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="T16">Type of the sixteenth argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The func delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetFunc<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this TExecutor executor, IEnumerable<char> funcName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, funcName, func);
        }

        #endregion Methods
    }
}
