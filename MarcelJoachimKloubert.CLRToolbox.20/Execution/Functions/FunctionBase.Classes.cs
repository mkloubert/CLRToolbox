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

        private sealed class FunctionExecutionContext : NotificationObjectBase,
                                                        IFunctionExecutionContext
        {
            #region Fields (11)

            private bool _isCanceled;
            private FunctionExecutionContextCallback _completedCallback;
            private OnExecuteContext _currentContext;
            private IList<Exception> _errors;
            private FunctionExecutionContextCallback _failedCallback;
            private int? _resultCode;
            private string _resultMessage;
            private IReadOnlyDictionary<string, object> _resultParameters;
            private FunctionExecutionContextCallback _succeededCallback;
            internal FunctionBase Function;
            internal IReadOnlyDictionary<string, object> InputParameters;

            #endregion Fields

            #region Properties (12)

            public bool IsCanceled
            {
                get { return this._isCanceled; }

                private set
                {
                    if (value != this._isCanceled)
                    {
                        this.OnPropertyChanging("IsCanceled");
                        this._isCanceled = value;
                        this.OnPropertyChanged("IsCanceled");
                    }
                }
            }

            public FunctionExecutionContextCallback CompletedCallback
            {
                get { return this._completedCallback; }

                set
                {
                    if (!object.Equals(value, this._completedCallback))
                    {
                        this.OnPropertyChanging("CompletedCallback");
                        this._completedCallback = value;
                        this.OnPropertyChanged("CompletedCallback");
                    }
                }
            }

            internal OnExecuteContext CurrentContext
            {
                get { return this._currentContext; }

                set
                {
                    if (!object.Equals(this._currentContext, value))
                    {
                        this.OnPropertyChanging("CurrentContext");
                        this.OnPropertyChanging("IsBusy");

                        this._currentContext = value;

                        this.OnPropertyChanged("CurrentContext");
                        this.OnPropertyChanged("IsBusy");
                    }
                }
            }

            public IList<Exception> Errors
            {
                get { return this._errors; }

                internal set
                {
                    if (!object.Equals(this._errors, value))
                    {
                        this.OnPropertyChanging("Errors");
                        this._errors = value;
                        this.OnPropertyChanged("Errors");
                    }
                }
            }

            public FunctionExecutionContextCallback FailedCallback
            {
                get { return this._failedCallback; }

                set
                {
                    if (!object.Equals(value, this._failedCallback))
                    {
                        this.OnPropertyChanging("FailedCallback");
                        this._failedCallback = value;
                        this.OnPropertyChanged("FailedCallback");
                    }
                }
            }

            public bool IsBusy
            {
                get { return this.CurrentContext != null; }
            }

            public int? ResultCode
            {
                get { return this._resultCode; }

                private set
                {
                    if (value != this._resultCode)
                    {
                        this.OnPropertyChanging("ResultCode");
                        this._resultCode = value;
                        this.OnPropertyChanged("ResultCode");
                    }
                }
            }

            public string ResultMessage
            {
                get { return this._resultMessage; }

                private set
                {
                    if (value != this._resultMessage)
                    {
                        this.OnPropertyChanging("ResultMessage");
                        this._resultMessage = value;
                        this.OnPropertyChanged("ResultMessage");
                    }
                }
            }

            public IReadOnlyDictionary<string, object> ResultParameters
            {
                get { return this._resultParameters; }

                internal set
                {
                    if (!object.Equals(this._resultParameters, value))
                    {
                        this.OnPropertyChanging("ResultParameters");
                        this._resultParameters = value;
                        this.OnPropertyChanged("ResultParameters");
                    }
                }
            }

            public FunctionExecutionContextCallback SucceededCallback
            {
                get { return this._succeededCallback; }

                set
                {
                    if (!object.Equals(value, this._succeededCallback))
                    {
                        this.OnPropertyChanging("SucceededCallback");
                        this._succeededCallback = value;
                        this.OnPropertyChanged("SucceededCallback");
                    }
                }
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