// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    /// <summary>
    /// Describes a sync job.
    /// </summary>
    public interface ISyncJob : INotificationObject, IHasName, IRunnable
    {
        #region Data Members (5)

        /// <summary>
        /// Gets the destination directory.
        /// </summary>
        string DestinationDirectory { get; }

        /// <summary>
        /// Gets the current progress description.
        /// </summary>
        string ProgressDescription { get; }

        /// <summary>
        /// Gets the current progress value between 0 and 100 or <see langword="null" /> for no progress.
        /// </summary>
        int? ProgressValue { get; }

        /// <summary>
        /// Gets the source directory.
        /// </summary>
        string SourceDirectory { get; }

        /// <summary>
        /// Gets the current state.
        /// </summary>
        SyncJobState State { get; }

        #endregion Data Members

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// Is invoked if progress / status changed.
        /// </summary>
        event ProgressChangedEventHandler ProgressChanged;

        #endregion Delegates and Events
    }
}
