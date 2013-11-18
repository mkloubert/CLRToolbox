// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    partial class ScriptExecutorBase
    {
        #region Nested Classes (1)

        /// <summary>
        /// Simple implementation of <see cref="IScriptExecutionContext" /> interface.
        /// </summary>
        protected sealed class ScriptExecutionContext : IScriptExecutionContext
        {
            #region Fields (12)

            private DateTimeOffset? _endTime;
            private IList<Exception> _exceptions;
            private ScriptExecutorBase _executor;
            private bool _isDebug;
            private bool _isExecuting;
            private ScriptExecutionCompletedHandler _onCompleted;
            private ScriptExecutionFailedHandler _onFailed;
            private ScriptExecutionSucceedHandler _onSucceeded;
            private object _result;
            private string _source;
            private StartActionHandler _startAction;
            private DateTimeOffset? _startTime;

            #endregion Fields

            #region Properties (13)

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.Exceptions" />
            public DateTimeOffset? EndTime
            {
                get { return this._endTime; }

                private set { this._endTime = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.Exceptions" />
            public IList<Exception> Exceptions
            {
                get { return this._exceptions; }

                private set { this._exceptions = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.Executor" />
            public ScriptExecutorBase Executor
            {
                get { return this._executor; }

                internal set { this._executor = value; }
            }

            IScriptExecutor IScriptExecutionContext.Executor
            {
                get { return this.Executor; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.HasFailed" />
            public bool HasFailed
            {
                get
                {
                    IList<Exception> exList = this.Exceptions;

                    return exList != null &&
                           exList.Count > 0;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.IsDebug" />
            public bool IsDebug
            {
                get { return this._isDebug; }

                internal set { this._isDebug = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.IsExecuting" />
            public bool IsExecuting
            {
                get { return this._isExecuting; }

                private set { this._isExecuting = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.OnCompleted" />
            public ScriptExecutionCompletedHandler OnCompleted
            {
                get { return this._onCompleted; }

                set { this._onCompleted = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.OnFailed" />
            public ScriptExecutionFailedHandler OnFailed
            {
                get { return this._onFailed; }

                set { this._onFailed = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.OnSucceed" />
            public ScriptExecutionSucceedHandler OnSucceed
            {
                get { return this._onSucceeded; }

                set { this._onSucceeded = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.Result" />
            public object Result
            {
                get { return this._result; }

                internal set { this._result = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.Source" />
            public string Source
            {
                get { return this._source; }

                set { this._source = value; }
            }

            /// <summary>
            /// Gets or sets the logic for <see cref="ScriptExecutionContext.Start()" /> method.
            /// </summary>
            public StartActionHandler StartAction
            {
                get { return this._startAction; }

                set { this._startAction = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.StartTime" />
            public DateTimeOffset? StartTime
            {
                get { return this._startTime; }

                private set { this._startTime = value; }
            }

            #endregion Properties

            #region Delegates and Events (1)

            // Delegates (1) 

            /// <summary>
            /// Describes a handler for the <see cref="ScriptExecutionContext.Start()" /> method.
            /// </summary>
            public delegate void StartActionHandler();

            #endregion Delegates and Events

            #region Methods (1)

            // Public Methods (1) 

            /// <summary>
            /// 
            /// </summary>
            /// <see cref="IScriptExecutionContext.Start()" />
            public void Start()
            {
                try
                {
                    this.StartTime = DateTimeOffset.Now;
                    this.IsExecuting = true;

                    StartActionHandler action = this.StartAction;
                    if (action != null)
                    {
                        action();
                    }

                    this.Exceptions = null;

                    ScriptExecutionSucceedHandler handler = this.OnSucceed;
                    if (handler != null)
                    {
                        handler(this);
                    }
                }
                catch (Exception ex)
                {
                    this.Exceptions = new Exception[] { ex };

                    ScriptExecutionFailedHandler handler = this.OnFailed;
                    if (handler != null)
                    {
                        handler(this);
                    }
                }
                finally
                {
                    this.EndTime = DateTimeOffset.Now;
                    this.IsExecuting = false;

                    ScriptExecutionCompletedHandler handler = this.OnCompleted;
                    if (handler != null)
                    {
                        handler(this);
                    }
                }
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
