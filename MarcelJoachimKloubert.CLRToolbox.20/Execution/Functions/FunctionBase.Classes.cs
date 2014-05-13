// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Functions
{
    partial class FunctionBase
    {
        #region CLASS: ExecuteContext

        /// <summary>
        /// A context for a <see cref="FunctionBase.OnExecute(OnExecuteContext)" /> call.
        /// </summary>
        protected class OnExecuteContext
        {
            #region Fields (5)

            private bool _isCancellationRequested;
            private IReadOnlyDictionary<string, object> _inputParameters;
            private int _resultCode;
            private string _resultMessage;
            private IDictionary<string, object> _resultParameters;

            #endregion Fields

            #region Properties (5)

            /// <summary>
            /// Gets the list of input parameters.
            /// </summary>
            public IReadOnlyDictionary<string, object> InputParameters
            {
                get { return this._inputParameters; }

                internal set { this._inputParameters = value; }
            }

            /// <summary>
            /// Gets if cancellation is requested or not.
            /// </summary>
            public bool IsCancellationRequested
            {
                get { return this._isCancellationRequested; }

                internal set { this._isCancellationRequested = value; }
            }

            /// <summary>
            /// Gets or sets the result code.
            /// </summary>
            public int ResultCode
            {
                get { return this._resultCode; }

                set { this._resultCode = value; }
            }

            /// <summary>
            /// Gets or sets the result message.
            /// </summary>
            public string ResultMessage
            {
                get { return this._resultMessage; }

                set { this._resultMessage = value; }
            }

            /// <summary>
            /// Gets the collection where result parameters can be defined.
            /// </summary>
            public IDictionary<string, object> ResultParameters
            {
                get { return this._resultParameters; }

                internal set { this._resultParameters = value; }
            }

            #endregion Properties
        }

        #endregion

        #region CLASS: FunctionExecutionContext

        private sealed class FunctionExecutionContext : NotificationObjectBase, IFunctionExecutionContext
        {
            #region Fields (2)

            internal FunctionBase Function;
            internal IReadOnlyDictionary<string, object> InputParameters;

            #endregion Fields

            #region Properties (12)

            public bool IsCanceled
            {
                get { return this.Get<bool>("IsCanceled"); }

                private set { this.Set(value, "IsCanceled"); }
            }

            public FunctionExecutionContextCallback CompletedCallback
            {
                get { return this.Get<FunctionExecutionContextCallback>("CompletedCallback"); }

                set { this.Set(value, "CompletedCallback"); }
            }

            internal OnExecuteContext CurrentContext
            {
                get { return this.Get<OnExecuteContext>("CurrentContext"); }

                set { this.Set(value, "CurrentContext"); }
            }

            public IList<Exception> Errors
            {
                get { return this.Get<IList<Exception>>("Errors"); }

                internal set { this.Set(value, "Errors"); }
            }

            public FunctionExecutionContextCallback FailedCallback
            {
                get { return this.Get<FunctionExecutionContextCallback>("FailedCallback"); }

                set { this.Set(value, "FailedCallback"); }
            }

            [ReceiveNotificationFrom("CurrentContext")]
            public bool IsBusy
            {
                get { return this.CurrentContext != null; }
            }

            public int? ResultCode
            {
                get { return this.Get<int?>("ResultCode"); }

                private set { this.Set(value, "ResultCode"); }
            }

            public string ResultMessage
            {
                get { return this.Get<string>("ResultMessage"); }

                private set { this.Set(value, "ResultMessage"); }
            }

            public IReadOnlyDictionary<string, object> ResultParameters
            {
                get { return this.Get<IReadOnlyDictionary<string, object>>("ResultParameters"); }

                internal set { this.Set(value, "ResultParameters"); }
            }

            public FunctionExecutionContextCallback SucceededCallback
            {
                get { return this.Get<FunctionExecutionContextCallback>("SucceededCallback"); }

                set { this.Set(value, "SucceededCallback"); }
            }

            IFunction IFunctionExecutionContext.Function
            {
                get { return this.Function; }
            }

            IReadOnlyDictionary<string, object> IFunctionExecutionContext.InputParameters
            {
                get { return this.InputParameters; }
            }

            #endregion Properties

            #region Methods (5)

            public bool Cancel()
            {
                return this.Cancel(true);
            }

            public bool Cancel(bool wait)
            {
                return this.Cancel(wait, null);
            }

            public bool Cancel(TimeSpan timeout)
            {
                return this.Cancel(true, timeout);
            }

            private bool Cancel(bool wait, TimeSpan? timeout)
            {
                if (timeout < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException("timeout");
                }

                OnExecuteContext ctx = this.CurrentContext;
                if (ctx == null)
                {
                    return false;
                }

                bool result = true;

                ctx.IsCancellationRequested = true;
                if (wait)
                {
                    DateTimeOffset startTime = DateTimeOffset.Now;
                    while (this.IsBusy)
                    {
                        if (timeout.HasValue == false)
                        {
                            // no timeout defined
                            continue;
                        }

                        DateTimeOffset now = DateTimeOffset.Now;
                        if ((now - startTime) >= timeout.Value)
                        {
                            // timeout reached => cancellation failed
                            result = false;
                            break;
                        }
                    }
                }

                this.IsCanceled = result;
                return result;
            }

            public void Start()
            {
                if (this.IsBusy)
                {
                    throw new InvalidOperationException();
                }

                FunctionExecutionContextCallback execCallback = null;
                string resultMsg = null;
                try
                {
                    OnExecuteContext execCtx = new OnExecuteContext();
                    execCtx.InputParameters = this.InputParameters;
                    execCtx.ResultCode = this.Function.DefaultResultCodeForSuccessfulExecution;
                    execCtx.ResultMessage = StringHelper.AsString(this.Function.DefaultResultMessageForSuccessfulExecution);
                    execCtx.ResultParameters = new Dictionary<string, object>();

                    this.CurrentContext = execCtx;
                    this.IsCanceled = false;

                    this.ResultCode = null;
                    this.ResultMessage = null;
                    this.ResultParameters = null;
                    this.Errors = null;

                    this.Function
                        .OnExecute(execCtx);

                    // result parameters
                    IDictionary<string, object> paramsDict = this.Function.CreateEmptyParameterCollection();
                    foreach (KeyValuePair<string, object> item in execCtx.ResultParameters)
                    {
                        paramsDict.Add(item.Key ?? string.Empty,
                                       this.Function
                                           .ParseValueForParameter(item.Value));
                    }

                    this.ResultParameters = new TMReadOnlyDictionary<string, object>(paramsDict);
                    this.Errors = new Exception[0];

                    this.ResultCode = execCtx.ResultCode;
                    resultMsg = (execCtx.ResultMessage ?? string.Empty).Trim();

                    execCallback = this.SucceededCallback;
                }
                catch (Exception ex)
                {
                    execCallback = this.FailedCallback;

                    this.ResultCode = this.Function.ResultCodeForUncaughtException;
                    resultMsg = StringHelper.AsString(this.Function.GetResultMessageForException(ex));

                    AggregateException aggEx = ex as AggregateException;
                    if (aggEx == null)
                    {
                        aggEx = new AggregateException(ex);
                    }

                    this.Errors = aggEx.InnerExceptions;
                }
                finally
                {
                    if (StringHelper.IsNullOrWhiteSpace(resultMsg))
                    {
                        resultMsg = null;
                    }

                    this.ResultMessage = resultMsg;
                    this.CurrentContext = null;

                    try
                    {
                        if (execCallback != null)
                        {
                            execCallback(this);
                        }
                    }
                    finally
                    {
                        FunctionExecutionContextCallback cb = this.CompletedCallback;
                        if (cb != null)
                        {
                            cb(this);
                        }
                    }
                }
            }

            #endregion Methods
        }

        #endregion
    }
}