// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.FileSyncer.Jobs.Actions
{
    /// <summary>
    /// Describes a sync action.
    /// </summary>
    public interface ISyncJobAction : ITMObject
    {
        #region Operations (1)

        /// <summary>
        /// Executes the sync action.
        /// </summary>
        /// <param name="ctx">The underlying exeuction context.</param>
        /// <exception cref="AggregateException">At least one error occured.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctx" /> is <see langword="null" />.
        /// </exception>
        void Execute(ISyncJobExecutionContext ctx);

        #endregion Operations
    }
}
