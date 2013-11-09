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
        /// Sets a simple function with a variable number of parameters and a variable result.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the executor.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="funcName">The name of the function in the script.</param>
        /// <param name="func">The function delegate to set.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static TExecutor SetSimpleFunc<TExecutor>(this TExecutor executor, IEnumerable<char> funcName, ScriptExecutorBase.SimpleFunc func)
               where TExecutor : IScriptExecutor
        {
            ScriptHelper.SetSimpleFunc(executor, funcName, func);
            return executor;
        }

        #endregion Methods
    }
}
