// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// A job based on delegates.
    /// </summary>
    public sealed class DelegateJob : JobBase
    {
        #region Fields (2)

        private readonly ExecuteAction _EXECUTE_ACTION;
        private readonly CanExecuteAction _CAN_EXECUTE_ACTION;

        #endregion Fields

        #region Constructors (8)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecutePredicate">A predicate for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" /> and/or <paramref name="canExecutePredicate" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecutePredicate canExecutePredicate)
            : this(id, execAction, canExecutePredicate, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecutePredicate">A predicate for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <param name="syncRoot">The object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" />, <paramref name="canExecutePredicate" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecutePredicate canExecutePredicate, object syncRoot)
            : this(id, execAction, canExecutePredicate, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecutePredicate">A predicate for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <param name="isThreadSafe">Job schould work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" /> and/or <paramref name="canExecutePredicate" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecutePredicate canExecutePredicate, bool isThreadSafe)
            : this(id, execAction, canExecutePredicate, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecutePredicate">A predicate for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <param name="isThreadSafe">Job schould work thread safe or not.</param>
        /// <param name="syncRoot">The object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" />, <paramref name="canExecutePredicate" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecutePredicate canExecutePredicate, bool isThreadSafe, object syncRoot)
            : base(id, isThreadSafe, syncRoot)
        {
            if (execAction == null)
            {
                throw new ArgumentNullException("execAction");
            }

            if (canExecutePredicate == null)
            {
                throw new ArgumentNullException("canExecutePredicate");
            }

            this._EXECUTE_ACTION = execAction;
            this._CAN_EXECUTE_ACTION = delegate(DateTimeOffset time, ref bool canExecuteJob)
                {
                    canExecuteJob = canExecutePredicate(time);
                };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecuteAction">An action for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" /> and/or <paramref name="canExecuteAction" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecuteAction canExecuteAction)
            : this(id, execAction, canExecuteAction, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecuteAction">An action for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <param name="isThreadSafe">Job schould work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" /> and/or <paramref name="canExecuteAction" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecuteAction canExecuteAction, bool isThreadSafe)
            : this(id, execAction, canExecuteAction, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecuteAction">An action for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <param name="syncRoot">The object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" />, <paramref name="canExecuteAction" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecuteAction canExecuteAction, object syncRoot)
            : this(id, execAction, canExecuteAction, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateJob" /> class.
        /// </summary>
        /// <param name="id">The ID of the job.</param>
        /// <param name="execAction">An action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.</param>
        /// <param name="canExecuteAction">An action for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.</param>
        /// <param name="isThreadSafe">Job schould work thread safe or not.</param>
        /// <param name="syncRoot">The object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="execAction" />, <paramref name="canExecuteAction" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateJob(Guid id, ExecuteAction execAction, CanExecuteAction canExecuteAction, bool isThreadSafe, object syncRoot)
            : base(id, false)
        {
            if (execAction == null)
            {
                throw new ArgumentNullException("execAction");
            }

            if (canExecuteAction == null)
            {
                throw new ArgumentNullException("canExecuteAction");
            }

            this._EXECUTE_ACTION = execAction;
            this._CAN_EXECUTE_ACTION = canExecuteAction;
        }

        #endregion Constructors

        #region Delegates and events (3)

        /// <summary>
        /// Describes a predicate for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.
        /// </summary>
        /// <param name="time">The time value.</param>
        /// <returns>Can execute job or not.</returns>
        public delegate bool CanExecutePredicate(DateTimeOffset time);

        /// <summary>
        /// Describes an action for a <see cref="DelegateJob.OnCanExecute(DateTimeOffset, ref bool)" /> method.
        /// </summary>
        /// <param name="time">The time value.</param>
        /// <param name="canExecuteJob">The variable that defines if job can be executed or not.</param>
        public delegate void CanExecuteAction(DateTimeOffset time, ref bool canExecuteJob);

        /// <summary>
        /// Describes an action for a <see cref="DelegateJob.OnExecute(IJobExecutionContext)" /> method.
        /// </summary>
        public delegate void ExecuteAction(IJobExecutionContext ctx);

        #endregion Delegates and events

        #region Methods (2)

        /// <inheriteddoc />
        protected override void OnCanExecute(DateTimeOffset time, ref bool canExecuteJob)
        {
            this._CAN_EXECUTE_ACTION(time,
                                     ref canExecuteJob);
        }

        /// <inheriteddoc />
        protected override void OnExecute(IJobExecutionContext ctx)
        {
            this._EXECUTE_ACTION(ctx);
        }

        #endregion Methods
    }
}