// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    /// <summary>
    /// Extension methods for scripting operations.
    /// </summary>
    public static partial class ScriptingExtensionMethods
    {
        #region Methods (1)

        // Private Methods (1) 

        private static TExecutor SetScriptDelegate<TExecutor>(TExecutor executor, IEnumerable<char> actionName, Delegate actionDelegate)
            where TExecutor : global::MarcelJoachimKloubert.CLRToolbox.Scripting.IScriptExecutor
        {
            if (executor == null)
            {
                throw new ArgumentNullException("executor");
            }

            executor.SetFunction(actionName, actionDelegate);
            return executor;
        }

        #endregion Methods
    }
}
