// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MarcelJoachimKloubert.CLRToolbox.Composition
{
    /// <summary>
    /// A helper class for compositing a single instance of a sepcific service.
    /// </summary>
    /// <typeparam name="T">Type of the service.</typeparam>
    public sealed class SingleInstanceComposer<T> : InstanceComposerBase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleInstanceComposer{T}" /> class.
        /// </summary>
        /// <param name="container">The value for <see cref="InstanceComposerBase.Container" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public SingleInstanceComposer(CompositionContainer container, object syncRoot)
            : base(container, syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleInstanceComposer{T}" /> class.
        /// </summary>
        /// <param name="container">The value for <see cref="InstanceComposerBase.Container" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> is <see langword="null" />.
        /// </exception>
        public SingleInstanceComposer(CompositionContainer container)
            : base(container)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the instance.
        /// </summary>
        [Import(AllowRecomposition = true)]
        public T Instance
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="InstanceComposerBase.OnReset()" />
        protected override void OnReset()
        {
            base.OnReset();

            this.Instance = default(T);
        }

        #endregion Methods
    }
}
