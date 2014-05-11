// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution
{
    #region INTERFACE: IAsyncExecutionResult

    /// <summary>
    /// Describes an object that stores the result of an async execution.
    /// </summary>
    public interface IAsyncExecutionResult
    {
        #region Data Members (3)

        /// <summary>
        /// Gets the list of all occured errors.
        /// </summary>
        IList<Exception> Errors { get; }

        /// <summary>
        /// Gets if the execution has been failed or not.
        /// </summary>
        bool HasFailed { get; }

        /// <summary>
        /// Gets the result of the execution.
        /// </summary>
        object Result { get; }

        #endregion Data Members

        #region Methods (1)

        /// <summary>
        /// Gets the value of <see cref="IAsyncExecutionResult.Result" /> property strong typed.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <returns>The strong typed version of <see cref="IAsyncExecutionResult.Result" /> property.</returns>
        T GetResult<T>();

        #endregion Methods
    }

    #endregion

    #region INTERFACE: IAsyncExecutionResult<TState>

    /// <summary>
    /// Describes an object that stores the result of an async execution.
    /// </summary>
    /// <typeparam name="TState">Type of the underlying state object.</typeparam>
    public interface IAsyncExecutionResult<TState> : IAsyncExecutionResult
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying state object.
        /// </summary>
        TState State { get; }

        #endregion Data Members
    }

    #endregion
}