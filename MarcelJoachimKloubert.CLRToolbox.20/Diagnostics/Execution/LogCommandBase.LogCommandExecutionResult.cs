// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution
{
    partial class LogCommandBase
    {
        #region Nested Classes (1)

        private sealed class LogCommandExecutionResult : ILogCommandExecutionResult
        {
            #region Fields (5)

            private ILogCommand _command;
            private bool _doLogMessage;
            private IList<Exception> _errors;
            private ILogMessage _message;
            private object _messageValueToLog;

            #endregion Fields

            #region Properties (6)

            public ILogCommand Command
            {
                get { return this._command; }

                internal set { this._command = value; }
            }

            public bool DoLogMessage
            {
                get { return this._doLogMessage; }

                internal set { this._doLogMessage = value; }
            }

            public IList<Exception> Errors
            {
                get { return this._errors; }

                internal set { this._errors = value; }
            }

            public bool HasFailed
            {
                get
                {
                    IList<Exception> errs = this.Errors;
                    return errs != null &&
                           CollectionHelper.Any(errs,
                                                delegate(Exception ex, long index)
                                                {
                                                    return ex != null;
                                                });
                }
            }

            public ILogMessage Message
            {
                get { return this._message; }

                internal set { this._message = value; }
            }

            public object MessageValueToLog
            {
                get { return this._messageValueToLog; }

                internal set { this._messageValueToLog = value; }
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}