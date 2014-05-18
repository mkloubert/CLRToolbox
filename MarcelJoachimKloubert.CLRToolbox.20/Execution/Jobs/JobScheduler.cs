// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Timers;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// A class for handling scheduled jobs.
    /// </summary>
    public class JobScheduler : DisposableBase, IRunnable, IInitializable
    {
        #region Fields (4)

        private bool _isInitialized;
        private bool _isRunning;

        /// <summary>
        /// Stores the function / method that provides available jobs.
        /// </summary>
        protected readonly JobProvider _PROVIDER;

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

        #region Events and delegates (2)

        /// <summary>
        /// Describes a function that provides available jobs.
        /// </summary>
        /// <param name="scheduler">The underlying scheduler.</param>
        /// <returns>The list of available jobs.</returns>
        public delegate IEnumerable<IJob> JobProvider(JobScheduler scheduler);

        /// <inheriteddoc />
        public event EventHandler Initialized;

        #endregion Methods

        #region Methods (15)

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

                this.OnStart(false);
            }
        }

        /// <inheriteddoc />
        public void Stop()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                this.OnStop(false);
            }
        }

        // Protected Methods (8) 

        /// <summary>
        /// Disposes the underlying timer.
        /// </summary>
        protected virtual void DisposeTimer()
        {
            Timer t = this._timer;
            if (t == null)
            {
                return;
            }

            t.Stop();
            t.Dispose();

            t.Elapsed -= this.Timer_Elapsed;
        }

        /// <summary>
        /// Handles a job item.
        /// </summary>
        /// <param name="context">The underlying item context.</param>
        protected virtual void HandleJobItem(IForAllItemExecutionContext<IJob, DateTimeOffset> ctx)
        {
            IJob job = ctx.Item;
            DateTimeOffset time = ctx.State;
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
                                              return j.CanExecute(time);
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
            this._timer = new Timer();
            this._timer.AutoReset = false;
            this._timer.Interval = 750;
            this._timer.Elapsed += this.Timer_Elapsed;
        }

        /// <summary>
        /// Starts the underlying timer.
        /// </summary>
        protected virtual void StartTimer()
        {
            Timer t = this._timer;
            if (t == null)
            {
                return;
            }

            t.Start();
        }

        /// <summary>
        /// Stops the underlying timer.
        /// </summary>
        protected virtual void StopTimer()
        {
            Timer t = this._timer;
            if (t == null)
            {
                return;
            }

            t.Stop();
        }

        // Private Methods (3) 

        private void OnStart(bool isRestarting)
        {
            if (this.IsRunning)
            {
                return;
            }

            this.StartTimer();
            this.IsRunning = true;
        }

        private void OnStop(bool isRestarting)
        {
            if (this.IsRunning == false)
            {
                return;
            }

            this.StopTimer();
            this.IsRunning = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (this._SYNC)
            {
                DateTimeOffset now = AppTime.Now;
                Timer t = (global::System.Timers.Timer)sender;

                try
                {
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

        #region Properties (7)

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

        #endregion Properties
    }
}