// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Execution.Jobs;
using System;
using System.Windows;
using System.Windows.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Execution.Jobs
{
    /// <summary>
    /// A job scheduler that uses a <see cref="DispatcherTimer" /> instead.
    /// </summary>
    public class DispatcherJobScheduler : JobScheduler
    {
        #region Fields (3)

        private readonly DispatcherPriority _PRIORITY;
        private readonly DispatcherProvider _DISP_PROVIDER;
        private DispatcherTimer _timer;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DispatcherJobScheduler(JobProvider provider, object syncRoot)
            : this(provider, DispatcherPriority.Background, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="dispProvider">The function that provides the underlying dispatcher for the timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DispatcherJobScheduler(JobProvider provider, DispatcherProvider dispProvider, object syncRoot)
            : this(provider, DispatcherPriority.Background, dispProvider, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="dispProvider">The function that provides the underlying dispatcher for the timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DispatcherJobScheduler(JobProvider provider, DispatcherPriority prio, DispatcherProvider dispProvider, object syncRoot)
            : base(provider, syncRoot)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this._PRIORITY = prio;
            this._DISP_PROVIDER = dispProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DispatcherJobScheduler(JobProvider provider, DispatcherPriority prio, object syncRoot)
            : this(provider, prio, GetAppDispatcher, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public DispatcherJobScheduler(JobProvider provider, DispatcherPriority prio)
            : this(provider, prio, new object())
        {
        }

        #endregion Constructors

        #region Events and delegates (1)

        /// <summary>
        /// Describes a function or method that provides the <see cref="Dispatcher" /> for the
        /// dispatcher timer of that class.
        /// </summary>
        /// <param name="scheduler">The underlying scheduler.</param>
        /// <returns>The dispatcher to use.</returns>
        public delegate Dispatcher DispatcherProvider(DispatcherJobScheduler scheduler);

        #endregion Events and delegates

        #region Properties (1)

        /// <summary>
        /// Gets the underlying dispatcher.
        /// </summary>
        public Dispatcher Dispatcher
        {
            get
            {
                var t = this._timer;

                return t != null ? t.Dispatcher : null;
            }
        }

        #endregion Properties

        #region Methods (12)

        // Public Methods (6) 
        
        /// <summary>
        /// Creates a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="disp">The underlying dispatcher for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />and/or <paramref name="disp" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherJobScheduler Create(Dispatcher disp, JobProvider provider, DispatcherPriority prio)
        {
            return Create(disp,
                          provider,
                          prio,
                          new object());
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="disp">The underlying dispatcher for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="disp" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherJobScheduler Create(Dispatcher disp, JobProvider provider, object syncRoot)
        {
            return Create(disp,
                          provider,
                          DispatcherPriority.Background,
                          syncRoot);
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="disp">The underlying dispatcher for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="disp" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherJobScheduler Create(Dispatcher disp, JobProvider provider, DispatcherPriority prio, object syncRoot)
        {
            if (disp == null)
            {
                throw new ArgumentNullException("disp");
            }

            return new DispatcherJobScheduler(provider,
                                              prio,
                                              delegate(DispatcherJobScheduler scheduler)
                                              {
                                                  return disp;
                                              }, syncRoot);
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="dispObj">The underlying dispatcher object for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="dispObj" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherJobScheduler Create(DispatcherObject dispObj, JobProvider provider, DispatcherPriority prio)
        {
            return Create(dispObj,
                          provider,
                          prio,
                          new object());
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="dispObj">The underlying dispatcher object for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherJobScheduler Create(DispatcherObject dispObj, JobProvider provider, object syncRoot)
        {
            return Create(dispObj,
                          provider,
                          DispatcherPriority.Background,
                          syncRoot);
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="DispatcherJobScheduler" /> class.
        /// </summary>
        /// <param name="dispObj">The underlying dispatcher object for the timer.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="prio">The priority for the dispatcher timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="dispObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherJobScheduler Create(DispatcherObject dispObj, JobProvider provider, DispatcherPriority prio, object syncRoot)
        {
            if (dispObj == null)
            {
                throw new ArgumentNullException("dispObj");
            }

            return new DispatcherJobScheduler(provider,
                                              prio,
                                              delegate(DispatcherJobScheduler scheduler)
                                              {
                                                  return dispObj.Dispatcher;
                                              }, syncRoot);
        }

        // Protected Methods (4) 
        
        /// <inheriteddoc />
        protected override void DisposeTimer()
        {
            try
            {
                this.StopTimer();
            }
            finally
            {
                var t = this._timer;
                if (t != null)
                {
                    t.Tick -= this.Timer_Tick;
                }
            }
        }

        /// <inheriteddoc />
        protected override void OnInitialize()
        {
            Dispatcher dispatcher = this._DISP_PROVIDER(this);
            if (dispatcher == null)
            {
                this._timer = new DispatcherTimer(this._PRIORITY);
            }
            else
            {
                this._timer = new DispatcherTimer(this._PRIORITY, dispatcher);
            }

            this._timer.Interval = TimeSpan.FromMilliseconds(750);
            this._timer.Tick += this.Timer_Tick;
        }

        /// <inheriteddoc />
        protected override void StartTimer()
        {
            var t = this._timer;
            if (t == null)
            {
                return;
            }

            t.Start();
        }

        /// <inheriteddoc />
        protected override void StopTimer()
        {
            var t = this._timer;
            if (t == null)
            {
                return;
            }

            t.Stop();
        }

        // Private Methods (2) 

        private static Dispatcher GetAppDispatcher(DispatcherJobScheduler scheduler)
        {
            return Application.Current
                              .Dispatcher;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (this._SYNC)
            {
                DateTimeOffset now = AppTime.Now;
                DispatcherTimer t = (DispatcherTimer)sender;

                try
                {
                    this.StopTimer();

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
    }
}