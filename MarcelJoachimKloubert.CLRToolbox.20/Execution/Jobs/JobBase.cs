// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// A basic job.
    /// </summary>
    public abstract class JobBase : TMObject, IJob
    {
        #region Fields (4)

        private readonly Func<DateTimeOffset, bool> _CAN_EXECUTE_ACTION;
        private readonly Action<IJobExecutionContext> _EXECUTE_ACTION;
        private readonly Guid _ID;
        private readonly bool _IS_THREAD_SAFE;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="JobBase" /> class.
        /// </summary>
        /// <param name="id">The value for <see cref="JobBase.Id" /> property.</param>
        /// <param name="isThreadSafe">Job should handle thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected JobBase(Guid id, bool isThreadSafe, object syncRoot)
            : base(syncRoot)
        {
            this._ID = id;
            this._IS_THREAD_SAFE = isThreadSafe;

            if (this._IS_THREAD_SAFE)
            {
                this._CAN_EXECUTE_ACTION = this.OnCanExecute_ThreadSafe;
                this._EXECUTE_ACTION = this.OnExecute_ThreadSafe;
            }
            else
            {
                this._CAN_EXECUTE_ACTION = this.OnCanExecute_NonThreadSafe;
                this._EXECUTE_ACTION = this.OnExecute;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobBase" /> class.
        /// </summary>
        /// <param name="id">The value for <see cref="JobBase.Id" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected JobBase(Guid id, object syncRoot)
            : this(id, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobBase" /> class.
        /// </summary>
        /// <param name="id">The value for <see cref="JobBase.Id" /> property.</param>
        /// <param name="isThreadSafe">Job should handle thread safe or not.</param>
        protected JobBase(Guid id, bool isThreadSafe)
            : this(id, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobBase" /> class.
        /// </summary>
        /// <param name="id">The value for <see cref="JobBase.Id" /> property.</param>
        protected JobBase(Guid id)
            : this(id, false)
        {
        }

        #endregion Constructors

        #region Properties (1)

        /// <inheriteddoc />
        public Guid Id
        {
            get { return this._ID; }
        }

        #endregion Properties

        #region Methods (11)

        // Public Methods (6) 

        /// <inheriteddoc />
        public bool CanExecute(DateTimeOffset time)
        {
            return this._CAN_EXECUTE_ACTION(time);
        }

        /// <inheriteddoc />
        public void Execute(IJobExecutionContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException("ctx");
            }

            this._EXECUTE_ACTION(ctx);
        }

        /// <inheriteddoc />
        public bool Equals(IIdentifiable other)
        {
            return other != null ? this.Equals(other.Id) : false;
        }

        /// <inheriteddoc />
        public bool Equals(Guid other)
        {
            return this.Id == other;
        }

        /// <inheriteddoc />
        public override bool Equals(object other)
        {
            if (other is IIdentifiable)
            {
                return this.Equals((IIdentifiable)other);
            }

            if (other is Guid)
            {
                return this.Equals((Guid)other);
            }

            return base.Equals(other);
        }

        /// <inheriteddoc />
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        // Protected Methods (2) 

        /// <summary>
        /// The logic for the <see cref="JobBase.CanExecute(DateTimeOffset)" /> method.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="canExecuteJob">
        /// The variable where to write the result for <see cref="JobBase.CanExecute(DateTimeOffset)" /> method.
        /// This value is <see langword="false" /> by default.
        /// </param>
        protected abstract void OnCanExecute(DateTimeOffset time, ref bool canExecuteJob);

        /// <summary>
        /// The logic for the <see cref="JobBase.Execute(IJobExecutionContext)" /> method.
        /// </summary>
        /// <param name="ctx">The current execution context.</param>
        protected abstract void OnExecute(IJobExecutionContext ctx);

        // Private Methods (3) 

        private bool OnCanExecute_NonThreadSafe(DateTimeOffset time)
        {
            bool result = false;
            this.OnCanExecute(time, ref result);

            return result;
        }

        private bool OnCanExecute_ThreadSafe(DateTimeOffset time)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this.OnCanExecute_NonThreadSafe(time);
            }

            return result;
        }

        private void OnExecute_ThreadSafe(IJobExecutionContext ctx)
        {
            lock (this._SYNC)
            {
                this.OnExecute(ctx);
            }
        }

        #endregion Properties
    }
}