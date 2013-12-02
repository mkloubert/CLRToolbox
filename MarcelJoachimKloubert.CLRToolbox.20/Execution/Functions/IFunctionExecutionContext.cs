// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Functions
{
    #region DELEGATE: FunctionExecutionContextCallback

    /// <summary>
    /// Describes a callback for an <see cref="IFunctionExecutionContext" /> object.
    /// </summary>
    /// <param name="context">The underlying <see cref="IFunctionExecutionContext" /> object.</param>
    public delegate void FunctionExecutionContextCallback(IFunctionExecutionContext context);

    #endregion

    /// <summary>
    /// Describes an execution context of an <see cref="IFunction" />.
    /// </summary>
    public interface IFunctionExecutionContext : INotificationObject
    {
        #region Data Members (11)

        /// <summary>
        /// Gets or sets the callback that is executed after <see cref="IFunctionExecutionContext.FailedCallback" />
        /// and <see cref="IFunctionExecutionContext.SucceededCallback" />.
        /// </summary>
        FunctionExecutionContextCallback CompletedCallback { get; set; }

        /// <summary>
        /// Gets the list of errors that were occured by executing the context
        /// if available.
        /// </summary>
        IList<Exception> Errors { get; }

        /// <summary>
        /// Gets or sets the callback that is called when execution failed.
        /// </summary>
        FunctionExecutionContextCallback FailedCallback { get; set; }

        /// <summary>
        /// Gets the underlying function.
        /// </summary>
        IFunction Function { get; }

        /// <summary>
        /// Gets the list of input parameters.
        /// </summary>
        IReadOnlyDictionary<string, object> InputParameters { get; }

        /// <summary>
        /// Gets if the context is currently executing or not.
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Gets if the last execution was canceled or not.
        /// </summary>
        bool IsCanceled { get; }

        /// <summary>
        /// Gets the result code.
        /// </summary>
        int? ResultCode { get; }

        /// <summary>
        /// Gets the result message.
        /// </summary>
        string ResultMessage { get; }

        /// <summary>
        /// Gets the list of result parameters if available.
        /// </summary>
        IReadOnlyDictionary<string, object> ResultParameters { get; }

        /// <summary>
        /// Gets or sets the callback that is called when execution was successful.
        /// </summary>
        FunctionExecutionContextCallback SucceededCallback { get; set; }

        #endregion Data Members

        #region Operations (4)

        /// <summary>
        /// Cancels the current execution.
        /// </summary>
        /// <returns>Was cancelled or not because nothing is currently executed.</returns>
        /// <remarks>
        /// It is waited until <see cref="IFunctionExecutionContext.IsBusy" /> is
        /// <see langword="false" />.
        /// </remarks>
        bool Cancel();

        /// <summary>
        /// Cancels the current execution.
        /// </summary>
        /// <param name="wait">
        /// Wait until <see cref="IFunctionExecutionContext.IsBusy" /> is <see langword="false" /> or not.
        /// </param>
        /// <returns>Was cancelled or not because nothing is currently executed.</returns>
        bool Cancel(bool wait);

        /// <summary>
        /// Cancels the current execution.
        /// </summary>
        /// <param name="timeout">
        /// The time that is waiteduntil <see cref="IFunctionExecutionContext.IsBusy" /> is <see langword="false" />.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="timeout" /> is smaller than <see cref="TimeSpan.Zero" />.
        /// </exception>
        bool Cancel(TimeSpan timeout);

        /// <summary>
        /// Starts the execution.
        /// </summary>
        /// <exception cref="InvalidOperationException">Context is busy.</exception>
        void Start();

        #endregion Operations
    }
}
