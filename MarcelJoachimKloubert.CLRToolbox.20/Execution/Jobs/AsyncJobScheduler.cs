// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// A job scheduler that executes each job in an own thread.
    /// </summary>
    public class AsyncJobScheduler : JobScheduler
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AsyncJobScheduler(JobProvider provider, object syncRoot)
            : base(provider, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public AsyncJobScheduler(JobProvider provider)
            : base(provider)
        {
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <inheriteddoc />
        protected override void HandleJobs(DateTimeOffset time)
        {
            CollectionHelper.ForAllAsync(this.GetJobsToExecute(time),
                                         this.HandleJobItem,
                                         time);
        }

        #endregion Methods
    }
}