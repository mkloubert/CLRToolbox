// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl
{
    /// <summary>
    /// A service locator based on delegates.
    /// </summary>
    public sealed partial class DelegateServiceLocator : ServiceLocatorBase
    {
        #region Fields (5)

        private readonly IServiceLocator _BASE_LOCATOR;
        private readonly IDictionary<Type, InstanceProvider> _MULTI_PROVIDERS = new Dictionary<Type, InstanceProvider>();
        private MultiInstanceFallbackProvider _multiInstanceFallback;
        private readonly IDictionary<Type, InstanceProvider> _SINGLE_PROVIDERS = new Dictionary<Type, InstanceProvider>();
        private SingleInstanceFallbackProvider _singleInstanceFallback;

        #endregion Fields

        #region Constructors (3)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="DelegateServiceLocator" /> class.
        /// </summary>
        /// <param name="baseLocator">The value for <see cref="DelegateServiceLocator.BaseLocator" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DelegateServiceLocator(IServiceLocator baseLocator, object syncRoot)
            : base(syncRoot)
        {
            this._BASE_LOCATOR = baseLocator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateServiceLocator" /> class.
        /// </summary>
        /// <param name="baseLocator">The value for <see cref="DelegateServiceLocator.BaseLocator" /> property.</param>
        public DelegateServiceLocator(IServiceLocator baseLocator)
            : this(baseLocator, new object())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateServiceLocator" /> class.
        /// </summary>
        public DelegateServiceLocator()
            : this(null)
        {

        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the base service locator, if defined.
        /// </summary>
        public IServiceLocator BaseLocator
        {
            get { return this._BASE_LOCATOR; }
        }

        /// <summary>
        /// Gets or sets the provider that is invoked if that object is not able
        /// to locate instances for a specific service type.
        /// </summary>
        public MultiInstanceFallbackProvider MultiInstanceFallback
        {
            get { return this._multiInstanceFallback; }

            set { this._multiInstanceFallback = value; }
        }
        
        /// <summary>
        /// Gets or sets the provider that is invoked if that object is not able
        /// to locate an instance for a specific service type.
        /// </summary>
        public SingleInstanceFallbackProvider SingleInstanceFallback
        {
            get { return this._singleInstanceFallback; }

            set { this._singleInstanceFallback = value; }
        }

        #endregion Properties

        #region Delegates and Events (4)

        // Delegates (4) 

        /// <summary>
        /// Describes a function / method that provides a list of instances of a service.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="locator">The underlying locator.</param>
        /// <param name="key">The used key.</param>
        /// <returns>The list of instances or <see langword="null" /> to throw a <see cref="ServiceActivationException" />.</returns>
        public delegate IEnumerable<T> MultiInstanceProvider<T>(DelegateServiceLocator locator, object key);

        /// <summary>
        /// Describes a function / method that provides a list of instances of a service
        /// if that object itself is not able to locate instances for the defined service type.
        /// </summary>
        /// <param name="locator">The underlying locator.</param>
        /// <param name="key">The used key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The list of instances or <see langword="null" /> to throw a <see cref="ServiceActivationException" />.</returns>
        public delegate IEnumerable<object> MultiInstanceFallbackProvider(DelegateServiceLocator locator, object key, Type serviceType);

        /// <summary>
        /// Describes a function / method that provides a single instance of a service.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="locator">The underlying locator.</param>
        /// <param name="key">The used key.</param>
        /// <returns>The list of instances or <see langword="null" /> to throw a <see cref="ServiceActivationException" />.</returns>
        public delegate T SingleInstanceProvider<T>(DelegateServiceLocator locator, object key);
        
        /// <summary>
        /// Describes a fallback function / method that provides a single instance of a service
        /// if that object itself is not able to locate an instance for the defined service type.
        /// </summary>
        /// <param name="locator">The underlying locator.</param>
        /// <param name="key">The used key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The list of instances or <see langword="null" /> to throw a <see cref="ServiceActivationException" />.</returns>
        public delegate object SingleInstanceFallbackProvider(DelegateServiceLocator locator, object key, Type serviceType);

        #endregion Delegates and Events

        #region Methods (8)

        // Public Methods (4) 

        /// <summary>
        /// Registers a provider for resolving a list of instances of a service.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="provider">The provider to register.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// A provider is already registrated for the service type.
        /// </exception>
        public DelegateServiceLocator RegisterMultiProvider<T>(MultiInstanceProvider<T> provider)
        {
            return this.RegisterMultiProvider<T>(provider, true);
        }

        /// <summary>
        /// Registers a provider for resolving a list of instances of a service.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="provider">The provider to register.</param>
        /// <param name="registerDefaultSingleProvider">
        /// Also register a default single instance provider or not.
        /// </param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// A provider is already registrated for the service type.
        /// </exception>
        public DelegateServiceLocator RegisterMultiProvider<T>(MultiInstanceProvider<T> provider,
                                                               bool registerDefaultSingleProvider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            lock (this._SYNC)
            {
                Type serviceType = typeof(T);

                if (this._MULTI_PROVIDERS.ContainsKey(serviceType))
                {
                    throw new InvalidOperationException();
                }

                this._MULTI_PROVIDERS
                    .Add(serviceType,
                         new InstanceProvider(serviceType, provider));
            }

            if (registerDefaultSingleProvider)
            {
                this.RegisterSingleProvider<T>(MultiToSingle<T>(provider),
                                               false);
            }

            return this;
        }

        /// <summary>
        /// Registers a provider for resolving a single instance of a service.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="provider">The provider to register.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// A provider is already registrated for the service type.
        /// </exception>
        public DelegateServiceLocator RegisterSingleProvider<T>(SingleInstanceProvider<T> provider)
        {
            return this.RegisterSingleProvider<T>(provider, true);
        }

        /// <summary>
        /// Registers a provider for resolving a single instance of a service.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="provider">The provider to register.</param>
        /// <param name="registerDefaultMultiProvider">
        /// Also register a default multi instance provider or not.
        /// </param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// A provider is already registrated for the service type.
        /// </exception>
        public DelegateServiceLocator RegisterSingleProvider<T>(SingleInstanceProvider<T> provider,
                                                                bool registerDefaultMultiProvider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            lock (this._SYNC)
            {
                Type serviceType = typeof(T);

                if (this._SINGLE_PROVIDERS.ContainsKey(serviceType))
                {
                    throw new InvalidOperationException();
                }

                this._SINGLE_PROVIDERS
                    .Add(serviceType,
                         new InstanceProvider(serviceType, provider));
            }

            if (registerDefaultMultiProvider)
            {
                this.RegisterMultiProvider<T>(SingleToMulti<T>(provider),
                                              false);
            }

            return this;
        }
        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetAllInstances(Type, object)" />
        protected override IEnumerable<object> OnGetAllInstances(Type serviceType, object key)
        {
            IEnumerable<object> result = null;

            InstanceProvider provider;
            if (this._MULTI_PROVIDERS.TryGetValue(serviceType, out provider))
            {
                IEnumerable seq = provider.Invoke<IEnumerable>(this.BaseLocator,
                                                               key);

                if (seq != null)
                {
                    result = CollectionHelper.AsSequence<object>(seq);
                }

                if (result == null)
                {
                    throw new ServiceActivationException(serviceType, key);
                }
            }
            
            bool tryFallback = true;

            if (result == null)
            {
                if (this._BASE_LOCATOR != null)
                {
                    // use base service locator instead

                    tryFallback = false;
                    result = this._BASE_LOCATOR
                                 .GetAllInstances(serviceType, key);
                }
            }

            if (tryFallback)
            {
                // try by fallback, if defined

                MultiInstanceFallbackProvider fb = this.MultiInstanceFallback;
                if (fb != null)
                {
                    result = fb(this, key, serviceType);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetInstance(Type, object)" />
        protected override object OnGetInstance(Type serviceType, object key)
        {
            object result = null;

            InstanceProvider provider;
            if (this._SINGLE_PROVIDERS.TryGetValue(serviceType, out provider))
            {
                result = provider.Invoke<object>(this.BaseLocator,
                                                 key);

                if (result == null)
                {
                    throw new ServiceActivationException(serviceType, key);
                }
            }

            bool tryFallback = true;

            if (result == null)
            {
                if (this._BASE_LOCATOR != null)
                {
                    // use base service locator instead

                    tryFallback = false;
                    result = this._BASE_LOCATOR
                                 .GetInstance(serviceType, key);
                }
            }

            if (tryFallback)
            {
                // try by fallback, if defined

                SingleInstanceFallbackProvider fb = this.SingleInstanceFallback;
                if (fb != null)
                {
                    result = fb(this, key, serviceType);
                }
            }

            return result;
        }
        // Private Methods (2) 

        private static SingleInstanceProvider<T> MultiToSingle<T>(MultiInstanceProvider<T> provider)
        {
            return new SingleInstanceProvider<T>(delegate(DelegateServiceLocator locator, object key)
                {
                    Type serviceType = typeof(T);

                    IEnumerable<T> result = provider(locator, key);
                    if (result == null)
                    {
                        throw new ServiceActivationException(serviceType, key);
                    }

                    try
                    {
                        return CollectionHelper.Single<T>(result);
                    }
                    catch (Exception ex)
                    {
                        throw new ServiceActivationException(serviceType, key, ex);
                    }
                });
        }

        private static MultiInstanceProvider<T> SingleToMulti<T>(SingleInstanceProvider<T> provider)
        {
            return new MultiInstanceProvider<T>(delegate(DelegateServiceLocator locator, object key)
                {
                    try
                    {
                        T instance = provider(locator, key);
                        if (instance != null)
                        {
                            return new T[] { instance };
                        }
                    }
                    catch (ServiceActivationException saex)
                    {
                        if (saex.InnerException != null)
                        {
                            throw;
                        }
                    }

                    return CollectionHelper.Empty<T>();
                });
        }

        #endregion Methods
    }
}
