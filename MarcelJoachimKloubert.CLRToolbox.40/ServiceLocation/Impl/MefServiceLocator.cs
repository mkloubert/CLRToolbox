// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl
{
    /// <summary>
    /// A service locator based on a <see cref="ExportProvider" /> like a <see cref="CompositionContainer" />.
    /// </summary>
    public class ExportProviderServiceLocator : ServiceLocatorBase
    {
        #region Fields (1)

        /// <summary>
        /// Speichert den zugrundeliegenden <see cref="ExportProvider" />.
        /// </summary>
        protected readonly ExportProvider _PROVIDER;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// initializes a new instance of the <see cref="ExportProviderServiceLocator" /> class.
        /// </summary>
        /// <param name="provider">The underlying <see cref="ExportProvider" />.</param>
        /// <param name="sync">The value for the <see cref="ServiceLocatorBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="sync" /> are <see langword="null" />.
        /// </exception>
        public ExportProviderServiceLocator(ExportProvider provider, object sync)
            : base(sync)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this._PROVIDER = provider;
        }

        /// <summary>
        /// initializes a new instance of the <see cref="ExportProviderServiceLocator" /> class.
        /// </summary>
        /// <param name="provider">The underlying <see cref="ExportProvider" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public ExportProviderServiceLocator(ExportProvider provider)
            : this(provider: provider,
                   sync: new object())
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetAllInstances(Type)" />
        protected override IEnumerable<object> OnGetAllInstances(Type serviceType)
        {
            var container = this._PROVIDER as CompositionContainer;
            if (container != null)
            {
                // handle as extended composition container

                // find 'GetExportedValues' without parameters
                // create typed version for 'serviceType' and invoke
                return ((IEnumerable)container.GetType()
                                              .GetMethods()
                                              .Single(m => m.Name == container.GetMemberName(c => c.GetExportedValues<object>()) &&
                                                           m.GetGenericArguments().Length == 1 &&
                                                           m.GetParameters().Length == 0)
                                              .MakeGenericMethod(serviceType)
                                              .Invoke(container,
                                                      new object[0])).OfType<object>();
            }

            // old skool
            return this._PROVIDER
                       .GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetInstance(Type, object)" />
        protected override object OnGetInstance(Type serviceType, object key)
        {
            var strKey = key.AsString(true);

            var container = this._PROVIDER as CompositionContainer;
            if (container != null)
            {
                // handle as extended composition container

                // find 'GetExportedValue' methods
                var getExportedValueMethods = container.GetType()
                                                       .GetMethods()
                                                       .Where(m => m.Name == container.GetMemberName(c => c.GetExportedValue<object>()) &&
                                                                   m.GetGenericArguments().Length == 1);

                object[] @params;
                if (!string.IsNullOrWhiteSpace(strKey))
                {
                    // with key
                    @params = new object[] { strKey };
                }
                else
                {
                    @params = new object[0];
                }

                // find matching method, create typed version for 'serviceType'
                // and invoke with defined parameter(s)
                return getExportedValueMethods.Single(m => m.GetParameters().Length == @params.Length)
                                              .MakeGenericMethod(serviceType)
                                              .Invoke(container,
                                                      @params);
            }

            // old skool ...

            if (string.IsNullOrWhiteSpace(strKey))
            {
                strKey = AttributedModelServices.GetContractName(serviceType);
            }

            var lazyInstance = this._PROVIDER
                                   .GetExports<object>(strKey)
                                   .FirstOrDefault();

            if (lazyInstance != null)
            {
                // found
                return lazyInstance.Value;
            }

            // not found
            return null;
        }

        #endregion Methods
    }
}
