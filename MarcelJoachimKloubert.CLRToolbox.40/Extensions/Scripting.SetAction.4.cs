// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Scripting;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (12)

        // Public Methods (12) 

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed action.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T1">Type of the first argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T2">Type of the second argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T3">Type of the third argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T4">Type of the fourth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T14">Type of the forteenth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T14">Type of the forteenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T15">Type of the fifteenth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
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
        /// <typeparam name="T5">Type of the fifth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T6">Type of the sixth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T7">Type of the seventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T8">Type of the eighth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T9">Type of the nineth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T10">Type of the tenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T11">Type of the eleventh argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T12">Type of the twelfth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T13">Type of the thirteenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T14">Type of the forteenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T15">Type of the fifteenth argument of <paramref name="action" />.</typeparam>
        /// <typeparam name="T16">Type of the sixteenth argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetAction<TExecutor, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this TExecutor executor, IEnumerable<char> actionName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
            where TExecutor : IScriptExecutor
        {
            return SetScriptDelegate<TExecutor>(executor, actionName, action);
        }

        #endregion Methods
    }
}
