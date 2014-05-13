// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    #region INTERFACE: IDirectoryComparerExecutionContext

    /// <summary>
    /// Execution context of a <see cref="DirectoryComparer" />.
    /// </summary>
    public interface IDirectoryComparerExecutionContext
    {
        #region Data Members (7)

        /// <summary>
        /// Gets the duration of the execution.
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// Gets the end time of execution.
        /// </summary>
        DateTimeOffset? EndTime { get; }

        /// <summary>
        /// Gets the list of errors that were occured while execution.
        /// Is <see langword="null" /> while comparison is in progress.
        /// </summary>
        IList<Exception> Errors { get; }

        /// <summary>
        /// Gets if the execution is currently cancelling or not.
        /// </summary>
        bool IsCanceling { get; }

        /// <summary>
        /// Gets if comparison is in progress or not.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets if the sub directories also should be compared or not.
        /// </summary>
        bool Recursive { get; }

        /// <summary>
        /// Gets the start time of execution.
        /// </summary>
        DateTimeOffset? StartTime { get; }

        #endregion

        #region Delegates and Events (4)

        // Events (4) 

        /// <summary>
        /// Is invoked when two items are compared.
        /// </summary>
        event EventHandler<CompareFileSystemItemsEventArgs> ComparingItems;

        /// <summary>
        /// Is invoked when copy progress has been completed.
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// Is invoked if different items were found.
        /// </summary>
        event EventHandler<FoundDifferentFileSystemItemsEventArgs> DifferentItemsFound;

        /// <summary>
        /// Is invoked when copy progress has been started.
        /// </summary>
        event EventHandler Started;

        #endregion Delegates and Events

        #region Operations (3)

        /// <summary>
        /// Cancels the current execution.
        /// </summary>
        /// <remarks>It is waited until operation has been completed.</remarks>
        void Cancel();

        /// <summary>
        /// Cancels the current execution.
        /// </summary>
        /// <param name="wait">Wait until operation has been completed or not.</param>
        void Cancel(bool wait);

        /// <summary>
        /// Starts the execution.
        /// </summary>
        void Start();

        #endregion Operations
    }

    #endregion

    #region INTERFACE: IDirectoryComparerExecutionContext<TState>

    /// <summary>
    /// Extension of <see cref="IDirectoryComparerExecutionContext" /> with a state object.
    /// </summary>
    /// <typeparam name="TState">Type of the linked state object.</typeparam>
    public interface IDirectoryComparerExecutionContext<TState> : IDirectoryComparerExecutionContext
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the state object that is linked with that context.
        /// </summary>
        TState State { get; }

        #endregion Data Members
    }

    #endregion
}