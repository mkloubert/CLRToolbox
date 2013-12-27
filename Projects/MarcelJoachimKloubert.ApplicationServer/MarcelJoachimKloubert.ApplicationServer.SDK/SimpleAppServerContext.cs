// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.CLRToolbox.Timing;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Simple implementation of <see cref="IAppServerContext" /> interface.
    /// </summary>
    public sealed class SimpleAppServerContext : ObjectContextBase<IAppServer>,
                                                 IAppServerContext
    {
        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITimeProvider.Now" />
        public DateTimeOffset Now
        {
            get { return (this.NowProvider ?? Default_NowProvider)(); }
        }

        /// <summary>
        /// Gets or sets the logic for the <see cref="SimpleAppServerContext.Now" /> property.
        /// </summary>
        public Func<DateTimeOffset> NowProvider
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (1)

        // Private Methods (1) 

        private static DateTimeOffset Default_NowProvider()
        {
            return DateTimeOffset.Now;
        }

        #endregion Methods



        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAppServerContext" /> class.
        /// </summary>
        /// <param name="module">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="module" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public SimpleAppServerContext(IAppServer module, object syncRoot)
            : base(module, syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAppServerContext" /> class.
        /// </summary>
        /// <param name="module">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="module" /> is <see langword="null" />.
        /// </exception>
        public SimpleAppServerContext(IAppServer module)
            : base(module)
        {

        }

        #endregion Constructors


        #region Properties (1)

        /// <summary>
        /// Gets or sets the inner service locator.
        /// </summary>
        public IServiceLocator InnerServiceLocator
        {
            get;
            set;
        }

        #endregion Properties


        #region Methods (2)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetAllInstances(Type, object)" />
        protected override IEnumerable<object> OnGetAllInstances(Type serviceType, object key)
        {
            var innerLoc = this.InnerServiceLocator;
            if (innerLoc != null)
            {
                return innerLoc.GetAllInstances(serviceType, key);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetInstance(Type, object)" />
        protected override object OnGetInstance(Type serviceType, object key)
        {
            var innerLoc = this.InnerServiceLocator;
            if (innerLoc != null)
            {
                return innerLoc.GetInstance(serviceType, key);
            }

            return null;
        }

        #endregion Methods
    }
}
