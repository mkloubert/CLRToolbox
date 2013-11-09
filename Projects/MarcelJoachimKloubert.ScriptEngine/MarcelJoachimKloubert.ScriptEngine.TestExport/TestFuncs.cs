using System;
using MarcelJoachimKloubert.CLRToolbox.Scripting.Export;

namespace MarcelJoachimKloubert.ScriptEngine.TestExport
{
    public class TestFuncs
    {
        #region Methods (6)

        // Public Methods (3) 

        [ExportScriptFunc("to_lower")]
        public string ToLower(string str)
        {
            return str.ToLower();
        }

        [ExportScriptFunc]
        public static string ToUpper(string str)
        {
            return str.ToUpper();
        }

        [ExportScriptFunc("write_line")]
        public static void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
        // Private Methods (2) 

        [ExportScriptFunc("trim")]
        private string Trim(string str)
        {
            return str.Trim();
        }

        [ExportScriptFunc]
        private static string TrimLeft(string str)
        {
            return str.TrimStart();
        }
        // Internal Methods (1) 

        [ExportScriptFunc]
        internal static void Write(string str)
        {
            Console.Write(str);
        }

        #endregion Methods
    }
}
