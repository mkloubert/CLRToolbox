﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Scripting;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class ScriptHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> by using a typed action.
        /// </summary>
        /// <typeparam name="T">Type of the first argument of <paramref name="action" />.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the function in the script.</param>
        /// <param name="action">The action delegate to set.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> is <see langword="null" />.
        /// </exception>
        public static void SetAction<T>(IScriptExecutor executor, IEnumerable<char> actionName, Action<T> action)
        {
            SetScriptDelegate(executor, actionName, action);
        }

        #endregion Methods
    }
}
