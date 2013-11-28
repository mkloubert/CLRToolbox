// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MarcelJoachimKloubert.CLRToolbox.Composition
{
    /// <summary>
    /// A helper class for compositing a list of instances of a specific service.
    /// </summary>
    /// <typeparam name="T">Type of the service.</typeparam>
    public sealed class MultiInstanceComposer<T> : InstanceComposerBase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiInstanceComposer{T}" /> class.
        /// </summary>
        /// <param name="container">The value for <see cref="InstanceComposerBase.Container" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public MultiInstanceComposer(CompositionContainer container, object syncRoot)
            : base(container, syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiInstanceComposer{T}" /> class.
        /// </summary>
        /// <param name="container">The value for <see cref="InstanceComposerBase.Container" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> is <see langword="null" />.
        /// </exception>
        public MultiInstanceComposer(CompositionContainer container)
            : base(container)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the list of instances.
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        public List<T> Instances
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

            this.Instances = null;
        }

        #endregion Methods
    }
}
