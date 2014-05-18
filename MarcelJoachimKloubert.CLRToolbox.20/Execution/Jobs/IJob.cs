// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// Describes a job.
    /// </summary>
    public interface IJob : IIdentifiable
    {
        #region Methods (1)

        /// <summary>
        /// Checks if that job can be executed at a specific time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>
        /// Can execute at <paramref name="time" /> or not.
        /// </returns>
        bool CanExecute(DateTimeOffset time);

        #endregion Methods
    }
}