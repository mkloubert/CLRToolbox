// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    #region INTERFACE: JobProvider

    /// <summary>
    /// Describes a function that provides available jobs.
    /// </summary>
    /// <param name="scheduler">The underlying scheduler.</param>
    /// <returns>The list of available jobs.</returns>
    public delegate IEnumerable<IJob> JobProvider(IJobScheduler scheduler);

    #endregion INTERFACE: JobProvider

    #region INTERFACE: IJobScheduler

    /// <summary>
    /// Describes an object that handles scheduled jobs.
    /// </summary>
    public interface IJobScheduler : ITMDisposable, IRunnable, IInitializable, INotificationObject
    {
        #region Events and delegates (1)

        /// <summary>
        /// Is invoked when a job has been executed.
        /// </summary>
        event EventHandler<JobExecutionResultEventArgs> Executed;

        #endregion Events and delegates
    }

    #endregion INTERFACE: IJobScheduler
}