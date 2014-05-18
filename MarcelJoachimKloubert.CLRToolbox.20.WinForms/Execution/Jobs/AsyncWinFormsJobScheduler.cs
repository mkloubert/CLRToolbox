// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Execution.Jobs;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.ComponentModel;

namespace MarcelJoachimKloubert.CLRToolbox.WinForms.Execution.Jobs
{
    /// <summary>
    /// An extension of <see cref="WinFormsJobScheduler" /> that executes each job in an own thread.
    /// </summary>
    public class AsyncWinFormsJobScheduler : WinFormsJobScheduler
    {
        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncWinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public AsyncWinFormsJobScheduler(JobProvider provider)
            : base(provider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncWinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AsyncWinFormsJobScheduler(JobProvider provider, object syncRoot)
            : base(provider, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncWinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="containerProvider">The function that provides the optional container for the underlying timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="containerProvider" /> are <see langword="null" />.
        /// </exception>
        public AsyncWinFormsJobScheduler(JobProvider provider, ContainerProvider containerProvider)
            : base(provider, containerProvider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncWinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="containerProvider">The function that provides the optional container for the underlying timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="containerProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AsyncWinFormsJobScheduler(JobProvider provider, ContainerProvider containerProvider, object syncRoot)
            : base(provider, containerProvider, syncRoot)
        {
        }

        #endregion Constructors

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncWinFormsJobScheduler" /> for a specific container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="provider">The job provider.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="container" /> are <see langword="null" />.
        /// </exception>
        public static new AsyncWinFormsJobScheduler Create(IContainer container, JobProvider provider)
        {
            return Create(container, provider, new object());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncWinFormsJobScheduler" /> for a specific container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="container" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static new AsyncWinFormsJobScheduler Create(IContainer container, JobProvider provider, object syncRoot)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return new AsyncWinFormsJobScheduler(provider,
                                                 delegate(WinFormsJobScheduler scheduler)
                                                 {
                                                     return container;
                                                 },
                                                 syncRoot);
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