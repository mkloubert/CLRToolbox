// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Scripting;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    partial class ScriptingExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed predicate.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <typeparam name="T">Type of the first argument of <paramref name="predicate" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="predicateName">The name of the predicate in the script.</param>
        /// <param name="predicate">The predicate delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetPredicate<TExecutor, T>(this TExecutor executor, IEnumerable<char> predicateName, Predicate<T> predicate)
               where TExecutor : IScriptExecutor
        {
            ScriptHelper.SetPredicate<T>(executor, predicateName, predicate);
            return executor;
        }

        #endregion Methods
    }
}
