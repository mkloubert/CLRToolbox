using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    /// <summary>
    /// A basic HTTP module locator.
    /// </summary>
    public abstract class HttpModuleLocatorBase : TMObject,
                                                  IHttpModuleLocator
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpModuleLocatorBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HttpModuleLocatorBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpModuleLocatorBase" /> class.
        /// </summary>
        protected HttpModuleLocatorBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpModuleLocator.GetAllModules()" />
        public IEnumerable<IHttpModule> GetAllModules()
        {
            IEnumerable<IHttpModule> nonNullModList = this.OnGetAllModules() ?? CollectionHelper.Empty<IHttpModule>();

            return CollectionHelper.OfType<IHttpModule>(nonNullModList);
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for the <see cref="HttpModuleLocatorBase.OnGetAllModules()" /> method.
        /// </summary>
        /// <returns>The list of modules.</returns>
        protected abstract IEnumerable<IHttpModule> OnGetAllModules();

        #endregion Methods
    }
}
