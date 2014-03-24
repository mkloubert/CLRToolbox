// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    #region INTERFACE: IStreamCopyExecutionContext

    /// <summary>
    /// Describes an execution context of a <see cref="StreamCopier" />.
    /// </summary>
    public interface IStreamCopyExecutionContext
    {
        #region Data Members (5)

        /// <summary>
        /// Gets if the operation has been canceled or not.
        /// </summary>
        bool Canceled { get; }

        /// <summary>
        /// Gets the list of occured errors.
        /// </summary>
        IList<Exception> Errors { get; }

        /// <summary>
        /// Gets if the operation has been failed or not.
        /// </summary>
        bool Failed { get; }

        /// <summary>
        /// Gets if the process is cancelling or not.
        /// </summary>
        bool IsCancelling { get; }

        /// <summary>
        /// Gets if the copy process is currently running or not.
        /// </summary>
        bool IsRunning { get; }

        #endregion Data Members

        #region Delegates and Events (4)

        // Events (4) 

        /// <summary>
        /// Is invoked when copy progress has been completed.
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// Is invoked when progress changes.
        /// </summary>
        event EventHandler<StreamCopyProgressEventArgs> Progress;

        /// <summary>
        /// Is invoked when a progress step throws one or more errors.
        /// </summary>
        event EventHandler<StreamCopyProgressErrorEventArgs> ProgressError;

        /// <summary>
        /// Is invoked when copy progress has been started.
        /// </summary>
        event EventHandler Started;

        #endregion Delegates and Events

        #region Operations (2)

        /// <summary>
        /// Cancels the copy process.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Starts the copy process.
        /// </summary>
        void Start();

        #endregion Operations
    }

    #endregion

    #region INTERFACE: IStreamCopyExecutionContext<TState>

    /// <summary>
    /// Describes an execution context of a <see cref="StreamCopier" />.
    /// </summary>
    /// <typeparam name="TState">Type of the state object of <see cref="IStreamCopyExecutionContext{TState}.State" />.</typeparam>
    public interface IStreamCopyExecutionContext<TState> : IStreamCopyExecutionContext
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying and optional object that is linked with that context.
        /// </summary>
        TState State { get; }

        #endregion Data Members
    }

    #endregion
}
