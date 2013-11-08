// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    partial class ScriptExecutorBase
    {
        #region Nested Classes (1)

        /// <summary>
        /// Stores the data of a context for a <see cref="ScriptExecutorBase.OnExecute(OnExecuteContext)" /> method.
        /// </summary>
        protected sealed class OnExecuteContext
        {
            #region Fields (4)

            private bool _isDebug;
            private object _scriptResult;
            private string _source;
            private DateTimeOffset _startTime;

            #endregion Fields

            #region Properties (4)

            /// <summary>
            /// Gets if script execution is in debug mode or not.
            /// </summary>
            public bool IsDebug
            {
                get { return this._isDebug; }

                internal set { this._isDebug = value; }
            }

            /// <summary>
            /// Gets or sets the script result.
            /// </summary>
            public object ScriptResult
            {
                get { return this._scriptResult; }

                set { this._scriptResult = value; }
            }

            /// <summary>
            /// Gets the underlying source code.
            /// </summary>
            public string Source
            {
                get { return this._source; }

                internal set { this._source = value; }
            }

            /// <summary>
            /// Gets the start time.
            /// </summary>
            public DateTimeOffset StartTime
            {
                get { return this._startTime; }

                internal set { this._startTime = value; }
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}
