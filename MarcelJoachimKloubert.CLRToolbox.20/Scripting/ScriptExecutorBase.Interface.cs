// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    partial class ScriptExecutorBase
    {
        #region Methods (6)

        // Private Methods (6) 

        IScriptExecutor IScriptExecutor.ExposeType<T>()
        {
            return this.ExposeType<T>();
        }

        IScriptExecutor IScriptExecutor.ExposeType<T>(IEnumerable<char> alias)
        {
            return this.ExposeType<T>(alias);
        }

        IScriptExecutor IScriptExecutor.ExposeType(Type type)
        {
            return this.ExposeType(type);
        }

        IScriptExecutor IScriptExecutor.ExposeType(Type type, IEnumerable<char> alias)
        {
            return this.ExposeType(type, alias);
        }

        IScriptExecutor IScriptExecutor.SetFunction(IEnumerable<char> funcName, Delegate func)
        {
            return this.SetFunction(funcName, func);
        }

        IScriptExecutor IScriptExecutor.SetVariable(IEnumerable<char> varName, object value)
        {
            return this.SetVariable(varName, value);
        }

        #endregion Methods
    }
}
