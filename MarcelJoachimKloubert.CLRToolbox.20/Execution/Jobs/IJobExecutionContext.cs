// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// Describes an execution context for a job.
    /// </summary>
    public interface IJobExecutionContext : IIdentifiable
    {
        #region Methods (3)

        /// <summary>
        /// Gets the underlying job.
        /// </summary>
        IJob Job { get; }

        /// <summary>
        /// Gets the dictionary that stores the result parameters / values.
        /// </summary>
        IDictionary<string, object> Result { get; }

        /// <summary>
        /// Gets the execuction time.
        /// </summary>
        DateTimeOffset Time { get; }

        #endregion Methods
    }
}