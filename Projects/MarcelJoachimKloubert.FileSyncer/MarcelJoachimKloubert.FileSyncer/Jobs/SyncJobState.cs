// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    /// <summary>
    /// List of jobs.
    /// </summary>
    public enum SyncJobState
    {
        /// <summary>
        /// Not running / stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// Running
        /// </summary>
        Running,

        /// <summary>
        /// Canceled
        /// </summary>
        Canceled,

        /// <summary>
        /// Finished
        /// </summary>
        Finished,

        /// <summary>
        /// Faulted / failed
        /// </summary>
        Faulted,
    }
}
