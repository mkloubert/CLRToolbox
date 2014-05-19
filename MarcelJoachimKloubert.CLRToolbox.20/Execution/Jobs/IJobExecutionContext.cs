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
        #region Properties (4)

        /// <summary>
        /// Gets the underlying job.
        /// </summary>
        IJob Job { get; }

        /// <summary>
        /// Gets the dictionary that stores the parameters / values for the result.
        /// </summary>
        IDictionary<string, object> ResultVars { get; }

        /// <summary>
        /// Gets the object thread safe operations.
        /// </summary>
        object SyncRoot { get; }

        /// <summary>
        /// Gets the execution / start time.
        /// </summary>
        DateTimeOffset Time { get; }

        #endregion Properties
    }
}