// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Windows.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Execution.Jobs
{
    /// <summary>
    /// An extension of <see cref="DispatcherJobScheduler" /> that executes each job in an own thread.
    /// </summary>
    public class AsyncDispatcherJobScheduler : DispatcherJobScheduler
    {
        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AsyncDispatcherJobScheduler(JobProvider provider, object syncRoot)
            : base(provider, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="dispProvider">The function that provides the underlying dispatcher for the timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AsyncDispatcherJobScheduler(JobProvider provider, DispatcherProvider dispProvider, object syncRoot)
            : base(provider, dispProvider, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="dispProvider">The function that provides the underlying dispatcher for the timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AsyncDispatcherJobScheduler(JobProvider provider, DispatcherPriority prio, DispatcherProvider dispProvider, object syncRoot)
            : base(provider, prio, dispProvider, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AsyncDispatcherJobScheduler(JobProvider provider, DispatcherPriority prio, object syncRoot)
            : base(provider, prio, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public AsyncDispatcherJobScheduler(JobProvider provider, DispatcherPriority prio)
            : base(provider, prio)
        {
        }

        #endregion Constructors

        #region Methods (7)

        // Public Methods (6) 

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="disp">The underlying dispatcher for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />and/or <paramref name="disp" /> are <see langword="null" />.
        /// </exception>
        public new static AsyncDispatcherJobScheduler Create(Dispatcher disp, JobProvider provider, DispatcherPriority prio)
        {
            return Create(disp,
                          provider,
                          prio,
                          new object());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="disp">The underlying dispatcher for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="disp" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public new static AsyncDispatcherJobScheduler Create(Dispatcher disp, JobProvider provider, object syncRoot)
        {
            return Create(disp,
                          provider,
                          DispatcherPriority.Background,
                          syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="disp">The underlying dispatcher for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="disp" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public new static AsyncDispatcherJobScheduler Create(Dispatcher disp, JobProvider provider, DispatcherPriority prio, object syncRoot)
        {
            if (disp == null)
            {
                throw new ArgumentNullException("disp");
            }

            return new AsyncDispatcherJobScheduler(provider,
                                                   prio,
                                                   delegate(DispatcherJobScheduler scheduler)
                                                   {
                                                       return disp;
                                                   }, syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="dispObj">The underlying dispatcher object for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="dispObj" /> are <see langword="null" />.
        /// </exception>
        public new static AsyncDispatcherJobScheduler Create(DispatcherObject dispObj, JobProvider provider, DispatcherPriority prio)
        {
            return Create(dispObj,
                          provider,
                          prio,
                          new object());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="dispObj">The underlying dispatcher object for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public new static AsyncDispatcherJobScheduler Create(DispatcherObject dispObj, JobProvider provider, object syncRoot)
        {
            return Create(dispObj,
                          provider,
                          DispatcherPriority.Background,
                          syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncDispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="dispObj">The underlying dispatcher object for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public new static AsyncDispatcherJobScheduler Create(DispatcherObject dispObj, JobProvider provider, DispatcherPriority prio, object syncRoot)
        {
            if (dispObj == null)
            {
                throw new ArgumentNullException("dispObj");
            }

            return new AsyncDispatcherJobScheduler(provider,
                                                   prio,
                                                   delegate(DispatcherJobScheduler scheduler)
                                                   {
                                                       return dispObj.Dispatcher;
                                                   }, syncRoot);
        }

        // Protected Methods (1) 

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