// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Functions
{
    /// <summary>
    /// A basic function locator.
    /// </summary>
    public abstract class FunctionLocatorBase : TMObject,
                                                IFunctionLocator
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionLocatorBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected FunctionLocatorBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionLocatorBase" /> class.
        /// </summary>
        protected FunctionLocatorBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IFunctionLocator.GetAllFunctions()" />
        public IEnumerable<IFunction> GetAllFunctions()
        {
            IEnumerable<IFunction> nonNullFuncList = this.OnGetAllFunctions() ?? CollectionHelper.Empty<IFunction>();

            return CollectionHelper.OfType<IFunction>(nonNullFuncList);
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for the <see cref="FunctionLocatorBase.GetAllFunctions()" /> method.
        /// </summary>
        /// <returns>The list of functions.</returns>
        protected abstract IEnumerable<IFunction> OnGetAllFunctions();

        #endregion Methods
    }
}
