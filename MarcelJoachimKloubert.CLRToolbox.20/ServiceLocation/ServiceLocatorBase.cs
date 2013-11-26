﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation
{
    /// <summary>
    /// A basic object that locates service instances.
    /// </summary>
    public abstract partial class ServiceLocatorBase : TMObject, IServiceLocator
    {
        #region Constructors (2)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected ServiceLocatorBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorBase" /> class.
        /// </summary>
        protected ServiceLocatorBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (11)

        // Public Methods (8) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetAllInstances{S}()" />
        public IEnumerable<S> GetAllInstances<S>()
        {
            return this.GetAllInstances<S>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetAllInstances(Type)" />
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this.GetAllInstances(serviceType, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetAllInstances{S}(object)" />
        public IEnumerable<S> GetAllInstances<S>(object key)
        {
            foreach (object obj in this.GetAllInstances(typeof(S), key))
            {
                yield return (S)obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetAllInstances(Type, object)" />
        public IEnumerable<object> GetAllInstances(Type serviceType, object key)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            IEnumerable<object> result = null;

            ServiceActivationException exceptionToThrow = null;
            try
            {
                result = this.OnGetAllInstances(serviceType, key);

                if (result == null)
                {
                    exceptionToThrow = new ServiceActivationException(serviceType,
                                                                      null);
                }
            }
            catch (Exception ex)
            {
                exceptionToThrow = new ServiceActivationException(serviceType,
                                                                  null,
                                                                  ex);
            }

            if (exceptionToThrow != null)
            {
                throw exceptionToThrow;
            }

            foreach (object obj in result)
            {
                yield return !DBNull.Value.Equals(obj) ? obj : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetInstance{S}()" />
        public S GetInstance<S>()
        {
            return (S)this.GetInstance(typeof(S));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetInstance{S}(object)" />
        public S GetInstance<S>(object key)
        {
            return (S)this.GetInstance(typeof(S),
                                       key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetInstance(Type)" />
        public object GetInstance(Type serviceType)
        {
            return this.GetInstance(serviceType,
                                    null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IServiceLocator.GetInstance(Type, object)" />
        public object GetInstance(Type serviceType, object key)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            object result = null;

            ServiceActivationException exceptionToThrow = null;
            try
            {
                result = ParseValue(this.OnGetInstance(serviceType,
                                                       ParseValue(key)));

                if (result == null)
                {
                    exceptionToThrow = new ServiceActivationException(serviceType,
                                                                      key);
                }
            }
            catch (Exception ex)
            {
                exceptionToThrow = new ServiceActivationException(serviceType,
                                                                  key,
                                                                  ex);
            }

            if (exceptionToThrow != null)
            {
                throw exceptionToThrow;
            }

            return result;
        }
        // Protected Methods (2) 

        /// <summary>
        /// Stores the logic for the <see cref="ServiceLocatorBase.GetAllInstances(Type)" /> method.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">
        /// The key of the service.
        /// <see langword="null" /> indicates to locate the default service.
        /// </param>
        /// <returns>The list of service instances.</returns>
        /// <exception cref="ServiceActivationException">
        /// Error while locating service instance, e.g. not found.
        /// </exception>
        protected abstract IEnumerable<object> OnGetAllInstances(Type serviceType, object key);

        /// <summary>
        /// Stores the logic for the <see cref="ServiceLocatorBase.GetInstance(Type, object)" /> method.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">
        /// The key of the service.
        /// <see langword="null" /> indicates to locate the default service.
        /// </param>
        /// <returns>The located instance.</returns>
        /// <exception cref="ServiceActivationException">
        /// Error while locating service instance, e.g. not found.
        /// </exception>
        protected abstract object OnGetInstance(Type serviceType,
                                                object key);
        // Private Methods (1) 

        private static object ParseValue(object value)
        {
            object result = value;
            if (DBNull.Value.Equals(result))
            {
                result = null;
            }

            return result;
        }

        #endregion Methods
    }
}
