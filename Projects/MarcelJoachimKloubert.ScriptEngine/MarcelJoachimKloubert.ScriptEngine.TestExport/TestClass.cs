using System;
using MarcelJoachimKloubert.CLRToolbox.Scripting.Export;

namespace MarcelJoachimKloubert.ScriptEngine.TestExport
{
    [ExportScriptType]
    public class TestClass
    {
        #region Methods (1)

        // Public Methods (1) 

        public DateTime now()
        {
            return DateTime.Now;
        }

        #endregion Methods
    }

    [ExportScriptType("MyTestClass2")]
    public class TestClass2 : TestClass
    {
        #region Methods (1)

        // Public Methods (1) 

        public DateTime yesterday()
        {
            return DateTime.Now.Subtract(TimeSpan.FromDays(1));
        }

        #endregion Methods
    }
}
