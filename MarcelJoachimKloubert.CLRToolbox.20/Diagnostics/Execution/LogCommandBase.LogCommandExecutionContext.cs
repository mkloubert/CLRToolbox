// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution
{
    partial class LogCommandBase
    {
        #region Nested Classes (1)

        private sealed class LogCommandExecutionContext : ILogCommandExecutionContext
        {
            #region Properties (5)

            public object[] Arguments
            {
                get;
                internal set;
            }

            public ILogCommand Command
            {
                get;
                internal set;
            }

            public bool DoLogMessage
            {
                get;
                set;
            }

            public ILogMessage Message
            {
                get;
                set;
            }

            public object MessageValueToLog
            {
                get;
                set;
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}
