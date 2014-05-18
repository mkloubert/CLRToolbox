// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// A class for handling scheduled jobs.
    /// </summary>
    public partial class JobScheduler : DisposableBase, IRunnable, IInitializable
    {
        #region Fields (5)

        private bool _isInitialized;
        private bool _isRunning;

        /// <summary>
        /// Stores the function / method that provides available jobs.
        /// </summary>
        protected readonly JobProvider _PROVIDER;

        private DateTimeOffset? _startTime;
        private Timer _timer;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="JobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public JobScheduler(JobProvider provider, object syncRoot)
            : base(syncRoot)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this._PROVIDER = provider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public JobScheduler(JobProvider provider)
            : this(provider, new object())
        {
        }

        #endregion Constructors

        #region Events and delegates (5)

        /// <summary>
        /// Describes a function that provides available jobs.
        /// </summary>
        /// <param name="scheduler">The underlying scheduler.</param>
        /// <returns>The list of available jobs.</returns>
        public delegate IEnumerable<IJob> JobProvider(JobScheduler scheduler);

        /// <summary>
        /// Is invoked when a job has been executed.
        /// </summary>
        public event EventHandler<JobExecutionResultEventArgs> Executed;

        /// <inheriteddoc />
        public event EventHandler Initialized;

        /// <summary>
        /// Is invoked when that scheduler has been started.
        /// </summary>
        public event EventHandler Started;
        
        /// <summary>
        /// Is invoked when that scheduler has been stopped.
        /// </summary>
        public event EventHandler Stopped;

        #endregion Methods

        #region Methods (16)

        // Public Methods (4) 

        /// <inheriteddoc />
        public void Initialize()
        {
            lock (this._SYNC)
            {
                if (this.IsInitialized)
                {
                    throw new InvalidOperationException();
                }

                this.OnInitialize();
                this.IsInitialized = true;

                this.RaiseEventHandler(this.Initialized);
            }
        }

        /// <inheriteddoc />
        public void Restart()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();
                this.ThrowIfNotInitialized();

                if (this.CanRestart == false)
                {
                    throw new InvalidOperationException();
                }

                this.OnStop(true);
                this.OnStart(true);
            }
        }

        /// <inheriteddoc />
        public void Start()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();
                this.ThrowIfNotInitialized();

                if (this.CanStart == false)
                {
                    throw new InvalidOperationException();
                }

                this.OnStart(false);
            }
        }

        /// <inheriteddoc />
        public void Stop()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();
                this.ThrowIfNotInitialized();

                if (this.CanStop == false)
                {
                    throw new InvalidOperationException();
                }

                this.OnStop(false);
            }
        }

        // Protected Methods (9) 

        /// <summary>
        /// Disposes the underlying timer.
        /// </summary>
        protected virtual void DisposeTimer()
        {
            using (Timer t = this._timer)
            {
                this._timer = null;
            }
        }

        /// <summary>
        /// Handles a job item.
        /// </summary>
        /// <param name="ctx">The underlying item context.</param>
        protected virtual void HandleJobItem(IForAllItemExecutionContext<IJob, DateTimeOffset> ctx)
        {
            List<Exception> occuredErrors = new List<Exception>();

            DateTimeOffset completedAt;
            JobExecutionContext execCtx = new JobExecutionContext();
            try
            {
                execCtx.Job = ctx.Item;
                execCtx.Result = new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, true));
                execCtx.Time = ctx.State;

                execCtx.Job
                       .Execute(execCtx);

                completedAt = AppTime.Now;
            }
            catch (Exception ex)
            {
                completedAt = AppTime.Now;

                AggregateException aggEx = ex as AggregateException;
                if (aggEx != null)
                {
                    occuredErrors.AddRange(aggEx.InnerExceptions);
                }
                else
                {
                    occuredErrors.Add(ex);
                }
            }

            JobExecutionResult result = new JobExecutionResult();
            result.Context = execCtx;
            result.Errors = occuredErrors.ToArray();
            result.Result = new TMReadOnlyDictionary<string, object>(execCtx.Result);
            result.Time = completedAt;

            this.RaiseEventHandler(this.Executed,
                                   new JobExecutionResultEventArgs(result));
        }

        /// <summary>
        /// Returns all jobs that should be executed at a specific time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>The jobs.</returns>
        protected IEnumerable<IJob> GetJobsToExecute(DateTimeOffset time)
        {
            IEnumerable<IJob> allJobs = this._PROVIDER(this) ?? CollectionHelper.Empty<IJob>();

            IEnumerable<IJob> normalizedJobs = CollectionHelper.OfType<IJob>(allJobs);

            return CollectionHelper.Where(normalizedJobs,
                                          delegate(IJob j)
                                          {
                                              return j.CanExecute(time) &&
                                                     this.IsRunning;
                                          });
        }

        /// <summary>
        /// Handles jobs that should be executed at a specific time.
        /// </summary>
        /// <param name="time">The time.</param>
        protected virtual void HandleJobs(DateTimeOffset time)
        {
            CollectionHelper.ForAll(this.GetJobsToExecute(time),
                                    this.HandleJobItem,
                                    time);
        }

        /// <inheriteddoc />
        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeTimer();
            }
            else
            {
                this.StopTimer();
            }
        }

        /// <summary>
        /// The logic for the <see cref="JobScheduler.Initialize()" /> method.
        /// </summary>
        protected virtual void OnInitialize()
        {
            // dummy
        }

        /// <summary>
        /// Starts the underlying timer.
        /// </summary>
        protected virtual void StartTimer()
        {
            this._timer = new Timer(this.Timer_Elapsed, null, 750, Timeout.Infinite);
        }

        /// <summary>
        /// Stops the underlying timer.
        /// </summary>
        protected virtual void StopTimer()
        {
            this.DisposeTimer();
        }

        /// <summary>
        /// Throws an exception if that object has not been initiaized yet.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// That object has not been initiaized yet.
        /// </exception>
        protected void ThrowIfNotInitialized()
        {
            if (this.IsInitialized == false)
            {
                throw new InvalidOperationException();
            }
        }

        // Private Methods (3) 

        private void OnStart(bool isRestarting)
        {
            if (this.IsRunning)
            {
                return;
            }

            try
            {
                this.StartTime = AppTime.Now;
                this.IsRunning = true;

                this.StartTimer();

                this.RaiseEventHandler(this.Started);
            }
            catch
            {
                this.StartTime = null;
                this.IsRunning = false;

                this.RaiseEventHandler(this.Stopped);

                throw;
            }
        }

        private void OnStop(bool isRestarting)
        {
            if (this.IsRunning == false)
            {
                return;
            }

            this.StopTimer();
            
            this.StartTime = null;
            this.IsRunning = false;

            this.RaiseEventHandler(this.Stopped);
        }

        private void Timer_Elapsed(object state)
        {
            lock (this._SYNC)
            {
                try
                {
                    DateTimeOffset now = AppTime.Now;

                    this.HandleJobs(now);
                }
                catch
                {
                }
                finally
                {
                    if (this.IsRunning)
                    {
                        this.StartTimer();
                    }
                }
            }
        }

        #endregion Methods

        #region Properties (6)

        /// <inheriteddoc />
        public virtual bool CanRestart
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public virtual bool CanStart
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public virtual bool CanStop
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public bool IsInitialized
        {
            get { return this._isInitialized; }

            private set { this._isInitialized = value; }
        }

        /// <inheriteddoc />
        public bool IsRunning
        {
            get { return this._isRunning; }

            private set { this._isRunning = value; }
        }
        
        /// <summary>
        /// Gets the start time or <see langword="null" /> if not running.
        /// </summary>
        public DateTimeOffset? StartTime
        {
            get { return this._startTime; }

            private set { this._startTime = value; }
        }

        #endregion Properties
    }
}