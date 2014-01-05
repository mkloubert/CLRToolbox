// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Threading;
using MarcelJoachimKloubert.FileSyncer.Diagnostics;
using SyncJobActionQueue = System.Collections.Concurrent.ConcurrentQueue<MarcelJoachimKloubert.FileSyncer.Jobs.Actions.ISyncJobAction>;

namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    /// <summary>
    /// Describes the execution context of a sync job.
    /// </summary>
    public interface ISyncJobExecutionContext
    {
        #region Data Members (7)

        /// <summary>
        /// Gets the cancel source.
        /// </summary>
        CancellationTokenSource CancelSource { get; }

        /// <summary>
        /// Gets the cancel token.
        /// </summary>
        CancellationToken CancelToken { get; }

        /// <summary>
        /// Gets the destination directory.
        /// </summary>
        string DestionationDirectory { get; }

        /// <summary>
        /// Gets the underlying job.
        /// </summary>
        ISyncJob Job { get; }

        /// <summary>
        /// Gets the queue.
        /// </summary>
        SyncJobActionQueue Queue { get; }

        /// <summary>
        /// Gets the source directory.
        /// </summary>
        string SourceDirectory { get; }

        /// <summary>
        /// Gets the object for thread safe operations.
        /// </summary>
        object SyncRoot { get; }

        #endregion Data Members

        #region Operations (2)

        /// <summary>
        /// Starts logging.
        /// </summary>
        /// <param name="msg">The message to log.</param>
        /// <param name="tag">The log tag / category.</param>
        /// <param name="type">The log type / icon.</param>
        void Log(object msg,
                 IEnumerable<char> tag = null,
                 SyncLogType? type = null);

        /// <summary>
        /// Raises an event that teels that progress has been changed.
        /// </summary>
        /// <param name="text">The new status text.</param>
        /// <param name="percentage">The new percentage value.</param>
        void RaiseProgressChanged(IEnumerable<char> text = null,
                                  int? percentage = null);

        #endregion Operations
    }
}
