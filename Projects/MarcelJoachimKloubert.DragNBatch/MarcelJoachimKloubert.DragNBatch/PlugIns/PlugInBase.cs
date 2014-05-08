// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MarcelJoachimKloubert.DragNBatch.PlugIns
{
    /// <summary>
    /// A basic plug in.
    /// </summary>
    public abstract class PlugInBase : DisposableBase, IPlugIn
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="PlugInBase" /> class.
        /// </summary>
        /// <param name="id">The value for the <see cref="PlugInBase.Id" /> property.</param>
        protected PlugInBase(Guid id)
        {
            this.Id = id;
        }

        #endregion Constructors

        #region Properties (5)

        /// <summary>
        /// Gets the underlying context.
        /// </summary>
        public IPlugInContext Context
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public virtual string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentUICulture); }
        }

        /// <inheriteddoc />
        public Guid Id
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public bool IsInitialized
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public string Name
        {
            get { return this.GetType().FullName; }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Events (1) 

        /// <inheriteddoc />
        public event EventHandler Initialized;

        #endregion Delegates and Events

        #region Methods (11)

        // Public Methods (8) 

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
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return this.OnGetDisplayName(culture)
                       .AsString();
        }

        /// <inheriteddoc />
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        
        /// <inheriteddoc />
        public void HandleFiles(IHandleFilesContext context)
        {
            this.OnHandleFiles(context);
        }

        /// <inheriteddoc />
        public void Initialize()
        {
            this.Initialize(null);
        }

        /// <inheriteddoc />
        public void Initialize(IPlugInContext ctx)
        {
            lock (this._SYNC)
            {
                if (ctx == null)
                {
                    throw new ArgumentNullException("ctx");
                }

                if (this.IsInitialized)
                {
                    throw new InvalidOperationException();
                }

                this.OnInitialize(ref ctx);

                this.Context = ctx;
                this.IsInitialized = true;

                this.RaiseEventHandler(this.Initialized);
            }
        }

        // Protected Methods (3) 

        /// <inheriteddoc />
        protected virtual IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return this.Name;
        }
        
        /// <summary>
        /// The logic for the <see cref="PlugInBase.HandleFiles(IHandleFilesContext)" /> method.
        /// </summary>
        /// <param name="context">The underlying context.</param>
        protected abstract void OnHandleFiles(IHandleFilesContext context);

        /// <summary>
        /// The logic for the <see cref="PlugInBase.Initialize(IPlugInContext)" /> method.
        /// </summary>
        /// <param name="ctx">The value for the <see cref="PlugInBase.Context" /> property.</param>
        protected virtual void OnInitialize(ref IPlugInContext ctx)
        {
            // dummy
        }

        #endregion Methods
    }
}