// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    #region DELEGATE: ScriptExecutionCompletedHandler

    /// <summary>
    /// Describes a function or method that is invoked after execution of a script has been completed.
    /// </summary>
    /// <param name="context">The underlying execution context.</param>
    public delegate void ScriptExecutionCompletedHandler(IScriptExecutionContext context);

    #endregion

    #region DELEGATE: ScriptExecutionFailedHandler

    /// <summary>
    /// Describes a function or method that is when execution of a script has been failed.
    /// </summary>
    /// <param name="context">The underlying execution context.</param>
    public delegate void ScriptExecutionFailedHandler(IScriptExecutionContext context);

    #endregion

    #region DELEGATE: ScriptExecutionSucceedHandler

    /// <summary>
    /// Describes a function or method that is when execution of a script has been succeeded.
    /// </summary>
    /// <param name="context">The underlying execution context.</param>
    public delegate void ScriptExecutionSucceedHandler(IScriptExecutionContext context);

    #endregion

    #region INTERFACE:IScriptExecutionContext

    /// <summary>
    /// Describes a script execution context.
    /// </summary>
    public interface IScriptExecutionContext
    {
        #region Data Members (11)

        /// <summary>
        /// Gets the list of occured exceptions or <see langword="null" /> if execution has not been finished yet.
        /// </summary>
        IList<Exception> Exceptions { get; }

        /// <summary>
        /// Gets the underlying executor.
        /// </summary>
        IScriptExecutor Executor { get; }

        /// <summary>
        /// Gets if the execution has failed or not.
        /// </summary>
        bool HasFailed { get; }

        /// <summary>
        /// Gets if in debug mode or not.
        /// </summary>
        bool IsDebug { get; }

        /// <summary>
        /// Gets if the script is currently executing or not.
        /// </summary>
        bool IsExecuting { get; }

        /// <summary>
        /// Gets or sets the handler that is called if execution was completed.
        /// </summary>
        ScriptExecutionCompletedHandler OnCompleted { get; set; }

        /// <summary>
        /// Gets or sets the handler that is called if execution was has been failed.
        /// </summary>
        ScriptExecutionFailedHandler OnFailed { get; set; }

        /// <summary>
        /// Gets or sets the handler that is called if execution was succeeded.
        /// </summary>
        ScriptExecutionSucceedHandler OnSucceed { get; set; }

        /// <summary>
        /// Gets the script result.
        /// </summary>
        object Result { get; }

        /// <summary>
        /// Gets the underlying script source.
        /// </summary>
        string Source { get; }

        /// <summary>
        /// Gets the start time if available.
        /// </summary>
        DateTimeOffset? StartTime { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Starts the execution.
        /// </summary>
        void Start();

        #endregion Operations
    }

    #endregion
}
