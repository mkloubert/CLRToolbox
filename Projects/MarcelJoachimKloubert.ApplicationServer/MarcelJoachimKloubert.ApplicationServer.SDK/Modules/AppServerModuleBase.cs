// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Globalization;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// A basic application server module.
    /// </summary>
    public abstract partial class AppServerModuleBase : NotificationObjectBase, IAppServerModule
    {
        #region Fields (2)

        private IAppServerModuleContext _context;
        private bool _isRunning;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerModuleBase" /> class.
        /// </summary>
        /// <param name="id">The value for <see cref="AppServerModuleBase.Id" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AppServerModuleBase(Guid id, object syncRoot)
            : base(syncRoot)
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerModuleBase" /> class.
        /// </summary>
        /// <param name="id">The value for <see cref="AppServerModuleBase.Id" /> property.</param>
        protected AppServerModuleBase(Guid id)
            : this(id, new object())
        {

        }

        #endregion Constructors

        #region Properties (9)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanRestart" />
        public virtual bool CanRestart
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanStart" />
        public virtual bool CanStart
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanStop" />
        public virtual bool CanStop
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerModule.Context" />
        public IAppServerModuleContext Context
        {
            get { return this._context; }

            private set
            {
                if (!object.Equals(this._context, value))
                {
                    this.OnPropertyChanging(() => this.Context);
                    this.OnPropertyChanging(() => this.IsInitialized);

                    this._context = value;

                    this.OnPropertyChanged(() => this.Context);
                    this.OnPropertyChanged(() => this.IsInitialized);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.DisplayName" />
        public string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IIdentifiable.Id" />
        public Guid Id
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerModule.IsInitialized" />
        public bool IsInitialized
        {
            get { return this.Context != null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.IsRunning" />
        public bool IsRunning
        {
            get { return this._isRunning; }

            private set
            {
                if (this._isRunning != value)
                {
                    this.OnPropertyChanging(() => this.IsRunning);
                    this._isRunning = value;
                    this.OnPropertyChanged(() => this.IsRunning);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.Name" />
        public abstract string Name
        {
            get;
        }

        #endregion Properties

        #region Methods (16)

        // Public Methods (9) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IIdentifiable other)
        {
            return other != null ? this.Equals(other.Id) : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(Guid other)
        {
            return this.Id == other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
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

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.GetDisplayName(CultureInfo)" />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return StringHelper.AsString(this.OnGetDisplayName(culture));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()" />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerModule.Initialize(IAppServerModuleInitContext)" />
        public void Initialize(IAppServerModuleInitContext initContext)
        {
            lock (this._SYNC)
            {
                if (initContext == null)
                {
                    throw new ArgumentNullException("initContext");
                }

                if (this.IsInitialized)
                {
                    throw new InvalidOperationException();
                }

                var context = initContext.ModuleContext;
                if (context == null)
                {
                    throw new ArgumentException("initContext");
                }

                var isInitialized = true;
                this.OnInitialize(initContext,
                                  ref isInitialized);

                this.Context = isInitialized ? context : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Restart()" />
        public void Restart()
        {
            lock (this._SYNC)
            {
                this.ThrowIfNotInitialized();

                if (!this.CanRestart)
                {
                    throw new InvalidOperationException();
                }

                this.StopInner(StartStopContext.Restart);
                this.StartInner(StartStopContext.Restart);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Start()" />
        public void Start()
        {
            lock (this._SYNC)
            {
                this.ThrowIfNotInitialized();

                if (!this.CanStart)
                {
                    throw new InvalidOperationException();
                }

                this.StartInner(StartStopContext.Start);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Stop()" />
        public void Stop()
        {
            lock (this._SYNC)
            {
                this.ThrowIfNotInitialized();

                if (!this.CanStop)
                {
                    throw new InvalidOperationException();
                }

                this.StopInner(StartStopContext.Stop);
            }
        }
        // Protected Methods (5) 

        /// <summary>
        /// The logic for <see cref="AppServerModuleBase.GetDisplayName(CultureInfo)" />.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The display name based on <paramref name="culture" />.</returns>
        protected virtual IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return this.Name;
        }

        /// <summary>
        /// The logic for the <see cref="AppServerModuleBase.Initialize(IAppServerModuleInitContext)" /> method.
        /// </summary>
        /// <param name="initContext">The context.</param>
        /// <param name="isInitialized">
        /// Defines if initilize operation was successful or not.
        /// Is <see langword="true" /> at the beginning.
        /// </param>
        protected abstract void OnInitialize(IAppServerModuleInitContext initContext,
                                             ref bool isInitialized);

        /// <summary>
        /// The logic for <see cref="AppServerModuleBase.Start()" /> and
        /// the <see cref="AppServerModuleBase.Restart()" /> method.
        /// </summary>
        /// <param name="context">The invokation context.</param>
        /// <param name="isRunning">
        /// The new value for <see cref="AppServerModuleBase.IsRunning" /> property.
        /// Is <see langword="true" /> by default.
        /// </param>
        protected abstract void OnStart(StartStopContext context,
                                        ref bool isRunning);

        /// <summary>
        /// The logic for <see cref="AppServerModuleBase.Stop()" /> and
        /// the <see cref="AppServerModuleBase.Restart()" /> method.
        /// </summary>
        /// <param name="context">The invokation context.</param>
        /// <param name="isRunning">
        /// The new value for <see cref="AppServerModuleBase.IsRunning" /> property.
        /// Is <see langword="false" /> by default.
        /// </param>
        protected abstract void OnStop(StartStopContext context,
                                       ref bool isRunning);

        /// <summary>
        /// Throws an exception if that object has not been initialized yet.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Object has not been initialized yet.
        /// </exception>
        protected void ThrowIfNotInitialized()
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException("Object has not been initialized yet!");
            }
        }
        // Private Methods (2) 

        private void StartInner(StartStopContext context)
        {
            if (this.IsRunning)
            {
                return;
            }

            var isRunning = true;
            try
            {
                this.OnStart(context, ref isRunning);
            }
            finally
            {
                this.IsRunning = isRunning;
            }
        }

        private void StopInner(StartStopContext context)
        {
            if (!this.IsRunning)
            {
                return;
            }

            var isRunning = false;
            try
            {
                this.OnStop(context, ref isRunning);
            }
            finally
            {
                this.IsRunning = isRunning;
            }
        }

        #endregion Methods
    }
}
