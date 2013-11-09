// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    /// <summary>
    /// Helper class for script operations.
    /// </summary>
    public static partial class ScriptHelper
    {
        #region Methods (1)

        // Private Methods (1) 

        private static void SetScriptDelegate(global::MarcelJoachimKloubert.CLRToolbox.Scripting.IScriptExecutor executor,
                                              IEnumerable<char> name,
                                              Delegate @delegate)
        {
            if (executor == null)
            {
                throw new ArgumentNullException("executor");
            }

            executor.SetFunction(name, @delegate);
        }

        #endregion Methods
    }
}
