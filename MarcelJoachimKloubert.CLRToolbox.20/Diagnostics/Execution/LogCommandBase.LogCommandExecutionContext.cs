// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution
{
    partial class LogCommandBase
    {
        #region Nested Classes (1)

        private sealed class LogCommandExecutionContext : ILogCommandExecutionContext
        {
            #region Fields (5)

            private object[] _arguments;
            private ILogCommand _command;
            private bool _doLogMessage;
            private ILogMessage _message;
            private object _messageValueToLog;

            #endregion Fields

            #region Properties (5)

            public object[] Arguments
            {
                get { return this._arguments; }

                set { this._arguments = value; }
            }

            public ILogCommand Command
            {
                get { return this._command; }

                set { this._command = value; }
            }

            public bool DoLogMessage
            {
                get { return this._doLogMessage; }

                set { this._doLogMessage = value; }
            }

            public ILogMessage Message
            {
                get { return this._message; }

                set { this._message = value; }
            }

            public object MessageValueToLog
            {
                get { return this._messageValueToLog; }

                set { this._messageValueToLog = value; }
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}