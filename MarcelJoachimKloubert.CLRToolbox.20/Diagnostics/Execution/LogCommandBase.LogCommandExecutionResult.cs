// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution
{
    partial class LogCommandBase
    {
        #region Nested Classes (1)

        private sealed class LogCommandExecutionResult : ILogCommandExecutionResult
        {
            #region Properties (6)

            public ILogCommand Command
            {
                get;
                internal set;
            }

            public bool DoLogMessage
            {
                get;
                internal set;
            }

            public IList<Exception> Errors
            {
                get;
                internal set;
            }

            public bool HasFailed
            {
                get { return this.Errors.Count > 0; }
            }

            public ILogMessage Message
            {
                get;
                internal set;
            }

            public object MessageValueToLog
            {
                get;
                internal set;
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}
