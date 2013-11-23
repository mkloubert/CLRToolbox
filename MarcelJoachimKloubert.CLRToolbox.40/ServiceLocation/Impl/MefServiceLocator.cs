// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Linq.Expressions;
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

        #region Methods (3)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetAllInstances(Type, object)" />
        protected override IEnumerable<object> OnGetAllInstances(Type serviceType, object key)
        {
            var strKey = key.AsString(true);

            var container = this._PROVIDER as CompositionContainer;
            if (container != null)
            {
                // handle as extended composition container

                return InvokeGetExportedValueMethod(c => c.GetExportedValues<object>(),
                                                    container,
                                                    serviceType,
                                                    strKey);
            }

            // old skool ...

            if (strKey == null)
            {
                strKey = AttributedModelServices.GetContractName(serviceType);
            }

            return this._PROVIDER
                       .GetExportedValues<object>(strKey);
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

                return InvokeGetExportedValueMethod(c => c.GetExportedValue<object>(),
                                                    container,
                                                    serviceType,
                                                    strKey);
            }

            // old skool ...

            if (strKey == null)
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
        // Private Methods (1) 

        private static R InvokeGetExportedValueMethod<R>(Expression<Func<CompositionContainer, R>> expr,
                                                         CompositionContainer container,
                                                         Type serviceType,
                                                         string key)
        {
            var method = (expr.Body as MethodCallExpression).Method;
            var methodName = method.Name;

            var getExportedValueMethods = container.GetType()
                                                   .GetMethods()
                                                   .Where(m => m.Name == methodName &&
                                                               m.GetGenericArguments().Length == 1);

            object[] @params;
            if (key != null)
            {
                // with key
                @params = new object[] { key };
            }
            else
            {
                @params = new object[0];
            }

            return TMConvert.ChangeType<R>(getExportedValueMethods.Single(m => m.GetParameters().Length == @params.Length)
                                                                  .MakeGenericMethod(serviceType)
                                                                  .Invoke(container, @params));
        }

        #endregion Methods
    }
}
