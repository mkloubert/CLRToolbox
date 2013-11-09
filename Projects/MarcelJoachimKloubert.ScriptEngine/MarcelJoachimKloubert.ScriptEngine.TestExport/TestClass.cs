using System;
using MarcelJoachimKloubert.CLRToolbox.Scripting.Export;

namespace MarcelJoachimKloubert.ScriptEngine.TestExport
{
    [ExportScriptType]
    public class TestClass
    {
        #region Methods (1)

        // Public Methods (1) 

        public static DateTimeOffset now()
        {
            return DateTimeOffset.Now;
        }

        #endregion Methods
    }
}
