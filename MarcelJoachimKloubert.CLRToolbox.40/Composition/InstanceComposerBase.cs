// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MarcelJoachimKloubert.CLRToolbox.Composition
{
    /// <summary>
    /// A basic helper class for composing instances.
    /// </summary>
    public abstract class InstanceComposerBase : DisposableBase
    {
        #region Fields (1)

        private bool _refreshDone;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceComposerBase" /> class.
        /// </summary>
        /// <param name="container">The value for <see cref="InstanceComposerBase.Container" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        protected InstanceComposerBase(CompositionContainer container, object syncRoot)
            : base(syncRoot)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceComposerBase" /> class.
        /// </summary>
        /// <param name="container">The value for <see cref="InstanceComposerBase.Container" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> is <see langword="null" />.
        /// </exception>
        protected InstanceComposerBase(CompositionContainer container)
            : this(container, new object())
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying container.
        /// </summary>
        public CompositionContainer Container
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (3) 

        /// <summary>
        /// Refreshes the managed instance(s).
        /// </summary>
        public void Refesh()
        {
            lock (this._SYNC)
            {
                this.Container
                    .ComposeParts(this);

                this._refreshDone = true;
            }
        }

        /// <summary>
        /// Refreshes the managed instance(s) if not done yet.
        /// </summary>
        public void RefeshIfNeeded()
        {
            if (!this._refreshDone)
            {
                this.Refesh();
            }
        }

        /// <summary>
        /// Resets the managed instance(s).
        /// </summary>
        public void Reset()
        {
            lock (this._SYNC)
            {
                this.OnReset();

                this._refreshDone = false;
            }
        }
        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DisposableBase.OnDispose(bool)" />
        protected override sealed void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this.Container.Dispose();
            }
        }

        /// <summary>
        /// The logic for <see cref="InstanceComposerBase.Reset()" /> method.
        /// </summary>
        protected virtual void OnReset()
        {
            // do nothing by default
        }

        #endregion Methods

    }
}
