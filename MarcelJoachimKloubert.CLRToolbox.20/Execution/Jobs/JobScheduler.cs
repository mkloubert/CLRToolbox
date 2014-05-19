// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Sessions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// A class for handling scheduled jobs.
    /// </summary>
    public partial class JobScheduler : NotificationObjectBase, IJobScheduler
    {
        #region Fields (2)

        /// <summary>
        /// Stores the function / method that provides available jobs.
        /// </summary>
        protected readonly JobProvider _PROVIDER;

        private Timer _timer;

        #endregion Fields

        #region Constructors (3)

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

        /// <summary>
        /// Finalizes the current instance of the <see cref="JobScheduler" /> class.
        /// </summary>
        ~JobScheduler()
        {
            this.DisposeInner(false);
        }

        #endregion Constructors

        #region Events and delegates (6)

        /// <inheriteddoc />
        public event EventHandler Disposed;

        /// <inheriteddoc />
        public event EventHandler Disposing;

        /// <inheriteddoc />
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

        #endregion Events and delegates

        #region Methods (18)

        // Public Methods (5) 

        /// <inheriteddoc />
        public void Dispose()
        {
            this.DisposeInner(true);
            GC.SuppressFinalize(this);
        }

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

                this.OnStop();
                this.OnStart();
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

                this.OnStart();
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

                this.OnStop();
            }
        }

        // Protected Methods (10) 

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
                execCtx.ResultVars = new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, true));
                execCtx.SyncRoot = new object();
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
                    occuredErrors.AddRange(CollectionHelper.OfType<Exception>(aggEx.InnerExceptions));
                }
                else
                {
                    occuredErrors.Add(ex);
                }
            }

            JobExecutionResult result = new JobExecutionResult();
            result.Context = execCtx;
            result.Errors = occuredErrors.ToArray();
            result.Vars = new TMReadOnlyDictionary<string, object>(execCtx.ResultVars);
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

        /// <summary>
        /// The logic for the <see cref="JobScheduler.Dispose()" /> method
        /// and the finalizer.
        /// </summary>
        /// <param name="disposing">
        /// Is called from <see cref="JobScheduler.Dispose()" /> method (<see langword="true" />)
        /// or the finalizer (<see langword="false" />).
        /// </param>
        protected virtual void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeTimer();
            }
            else
            {
                this.StopTimer();
            }

            this.Session = null;
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
            this._timer = new Timer(this.Timer_Elapsed, null,
                                    750, Timeout.Infinite);
        }

        /// <summary>
        /// Stops the underlying timer.
        /// </summary>
        protected virtual void StopTimer()
        {
            this.DisposeTimer();
        }

        /// <summary>
        /// Throws an exception if that object has already been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">That object has already been disposed.</exception>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
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

        // Private Methods (4) 

        private void DisposeInner(bool disposing)
        {
            lock (this._SYNC)
            {
                lock (this._SYNC)
                {
                    if (disposing && this.IsDisposed)
                    {
                        return;
                    }

                    if (disposing)
                    {
                        this.RaiseEventHandler(this.Disposing);
                    }

                    this.OnDispose(disposing);

                    if (disposing)
                    {
                        this.RaiseEventHandler(this.Disposed);
                        this.IsDisposed = true;
                    }
                }
            }
        }

        private void OnStart()
        {
            if (this.IsRunning)
            {
                return;
            }

            try
            {
                SimpleSession<IJobScheduler> newSession = new SimpleSession<IJobScheduler>();
                newSession.Id = Guid.NewGuid();
                newSession.Parent = this;
                newSession.Time = AppTime.Now;

                this.Session = newSession;
                this.StartTimer();

                this.RaiseEventHandler(this.Started);
            }
            catch
            {
                this.Session = null;
                this.RaiseEventHandler(this.Stopped);

                throw;
            }
        }

        private void OnStop()
        {
            if (this.IsRunning == false)
            {
                return;
            }

            this.StopTimer();
            this.Session = null;

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

        #region Properties (8)

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
        public bool IsDisposed
        {
            get { return this.Get<bool>("IsDisposed"); }

            private set { this.Set(value, "IsDisposed"); }
        }

        /// <inheriteddoc />
        public bool IsInitialized
        {
            get { return this.Get<bool>("IsInitialized"); }

            private set { this.Set(value, "IsInitialized"); }
        }

        /// <inheriteddoc />
        [ReceiveNotificationFrom("Session")]
        public bool IsRunning
        {
            get { return this.Session != null; }
        }

        /// <inheriteddoc />
        public ISession<IJobScheduler> Session
        {
            get { return this.Get<ISession<IJobScheduler>>("Session"); }

            private set { this.Set(value, "Session"); }
        }

        /// <summary>
        /// Gets the start time or <see langword="null" /> if not running.
        /// </summary>
        [ReceiveNotificationFrom("Session")]
        public DateTimeOffset? StartTime
        {
            get
            {
                ISession<IJobScheduler> session = this.Session;

                return session == null ? (DateTimeOffset?)null : session.Time;
            }
        }

        #endregion Properties
    }
}