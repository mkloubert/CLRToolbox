// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    /// <summary>
    /// Describes the execution context of a <see cref="ListCloudDirectoryResult.SyncWithLocalDirectory(DirectoryInfo, bool, bool)" /> method.
    /// </summary>
    public interface ISyncWithLocalDirectoryExecutionContext
    {
        #region Data Members (6)

        /// <summary>
        /// Gets the list of errors occured while executing the process.
        /// </summary>
        IList<Exception> Errors { get; }

        /// <summary>
        /// Gets if the process is cancelling or not.
        /// </summary>
        bool IsCancelling { get; }

        /// <summary>
        /// Gets if the context is running or not.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets the path of the local directory.
        /// </summary>
        string LocalDirectory { get; }

        /// <summary>
        /// Gets the remote directory.
        /// </summary>
        ListCloudDirectoryResult RemoteDirectory { get; }

        /// <summary>
        /// Gets if the process is syncing recursively or not.
        /// </summary>
        bool SyncRecursively { get; }

        #endregion Data Members

        #region Delegates and Events (2)

        // Events (2) 

        /// <summary>
        /// Is invoked after process has been started.
        /// </summary>
        event EventHandler Started;

        /// <summary>
        /// Is invoked if progress has been changed.
        /// </summary>
        event SyncCloudDirectoryProgressEventHandler SyncProgress;

        #endregion Delegates and Events

        #region Operations (3)

        /// <summary>
        /// Cancels the process.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Cancels the process and waits until cancellation process has been done.
        /// </summary>
        void CancelAndWait();

        /// <summary>
        /// Starts the process.
        /// </summary>
        void Start();

        #endregion Operations
    }
}
