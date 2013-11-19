// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation
{
    /// <summary>
    /// Describes an object for locating service objects.
    /// </summary>
    public interface IServiceLocator : IServiceProvider
    {
        #region Operations (6)

        /// <summary>
        /// Gets all instances of a service.
        /// </summary>
        /// <typeparam name="S">Type of the service.</typeparam>
        /// <returns>All instances of the service.</returns>
        IEnumerable<S> GetAllInstances<S>();

        /// <summary>
        /// Gets all instances of a service.
        /// </summary>
        /// <param name="serviceType">Typ des Dienstes.</param>
        /// <returns>All instances of the service.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceType" /> is <see langword="null" />.
        /// </exception>
        IEnumerable<object> GetAllInstances(Type serviceType);

        /// <summary>
        /// Gets a single instance of a default service.
        /// </summary>
        /// <typeparam name="S">Type of the service.</typeparam>
        /// <returns>The instance of the service.</returns>
        /// <exception cref="ServiceActivationException">
        /// Error while locating service instance, e.g. not found.
        /// </exception>
        S GetInstance<S>();

        /// <summary>
        /// Gets a single instance of a specific service.
        /// </summary>
        /// <typeparam name="S">Type of the service.</typeparam>
        /// <param name="key">
        /// Key of the service.
        /// <see langword="null" /> indicates to get the default service.
        /// </param>
        /// <returns>The instance of the service.</returns>
        /// <exception cref="ServiceActivationException">
        /// Error while locating service instance, e.g. not found.
        /// </exception>
        S GetInstance<S>(object key);

        /// <summary>
        /// Gets a single instance of a default service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The instance of the service.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceType" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ServiceActivationException">
        /// Error while locating service instance, e.g. not found.
        /// </exception>
        object GetInstance(Type serviceType);

        /// <summary>
        /// Gets a single instance of a specific service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">
        /// Key of the service.
        /// <see langword="null" /> indicates to get the default service.
        /// </param>
        /// <returns>The instance of the service.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceType" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ServiceActivationException">
        /// Error while locating service instance, e.g. not found.
        /// </exception>
        object GetInstance(Type serviceType, object key);

        #endregion Operations
    }
}
