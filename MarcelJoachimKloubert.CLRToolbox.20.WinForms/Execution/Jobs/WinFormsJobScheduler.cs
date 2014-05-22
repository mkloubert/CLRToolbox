// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Execution.Jobs;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CLRToolbox.WinForms.Execution.Jobs
{
    /// <summary>
    /// A job scheduler that uses WinForms timer.
    /// </summary>
    public class WinFormsJobScheduler : JobScheduler
    {
        #region Fields (2)

        private readonly ContainerProvider _CONTAINER_PROVIDER;
        private Timer _timer;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public WinFormsJobScheduler(JobProvider provider)
            : this(provider, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public WinFormsJobScheduler(JobProvider provider, object syncRoot)
            : this(provider, GetNullContainer, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="containerProvider">The function that provides the optional container for the underlying timer.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="containerProvider" /> are <see langword="null" />.
        /// </exception>
        public WinFormsJobScheduler(JobProvider provider, ContainerProvider containerProvider)
            : this(provider, containerProvider, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsJobScheduler" />.
        /// </summary>
        /// <param name="provider">The job provider.</param>
        /// <param name="containerProvider">The function that provides the optional container for the underlying timer.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="containerProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public WinFormsJobScheduler(JobProvider provider, ContainerProvider containerProvider, object syncRoot)
            : base(provider, syncRoot)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (containerProvider == null)
            {
                throw new ArgumentNullException("containerProvider");
            }

            this._CONTAINER_PROVIDER = containerProvider;
        }

        #endregion Constructors

        #region Events and delegates (1)

        /// <summary>
        /// Describes a function or method that provides the <see cref="IContainer" /> for the
        /// underlying WinForms timer of that class.
        /// </summary>
        /// <param name="scheduler">The underlying scheduler.</param>
        /// <returns>The container to use.</returns>
        public delegate IContainer ContainerProvider(WinFormsJobScheduler scheduler);

        #endregion Events and delegates

        #region Methods (8)

        // Public Methods (2) 

        /// <summary>
        /// Creates a new instance of the <see cref="WinFormsJobScheduler" /> for a specific container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="provider">The job provider.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="container" /> are <see langword="null" />.
        /// </exception>
        public static WinFormsJobScheduler Create(IContainer container, JobProvider provider)
        {
            return Create(container, provider, new object());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="WinFormsJobScheduler" /> for a specific container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="provider">The job provider.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" />, <paramref name="container" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static WinFormsJobScheduler Create(IContainer container, JobProvider provider, object syncRoot)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return new WinFormsJobScheduler(provider,
                                            delegate(WinFormsJobScheduler scheduler)
                                            {
                                                return container;
                                            },
                                            syncRoot);
        }

        // Protected Methods (4) 

        /// <inheriteddoc />
        protected override void DisposeTimer()
        {
            using (Timer t = this._timer)
            {
                this._timer = null;
            }
        }

        /// <inheriteddoc />
        protected override void OnInitialize()
        {
            IContainer container = this._CONTAINER_PROVIDER(this);
            if (container == null)
            {
                this._timer = new Timer();
            }
            else
            {
                this._timer = new Timer(container);
            }

            this._timer.Interval = 750;
            this._timer.Tick += this.Timer_Tick;
        }

        /// <inheriteddoc />
        protected override void StartTimer()
        {
            this._timer.Interval = (int)this.TimerInterval.TotalMilliseconds;
            this._timer.Start();
        }

        /// <inheriteddoc />
        protected override void StopTimer()
        {
            this._timer.Stop();
        }

        // Private Methods (1) 

        private static IContainer GetNullContainer(WinFormsJobScheduler scheduler)
        {
            return null;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (this._SYNC)
            {
                try
                {
                    this.StopTimer();

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
    }
}