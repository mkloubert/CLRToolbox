// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation
{
    /// <summary>
    /// Access to a global <see cref="IServiceLocator" /> instance.
    /// </summary>
    public static class ServiceLocator
    {
        #region Fields (1)

        private static ServiceLocatorProvider _provider;

        #endregion Fields

        #region Properties (1)

        /// <summary>
        /// Gets the current, global service locator returned by the defined provider delegate
        /// in <see cref="ServiceLocator.SetLocatorProvider(ServiceLocatorProvider)" />.
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// <see cref="ServiceLocatorProvider" /> instance is not defined.
        /// </exception>
        public static IServiceLocator Current
        {
            get { return _provider(); }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes logic that returns the value for <see cref="ServiceLocator.Current" />.
        /// </summary>
        /// <returns></returns>
        public delegate IServiceLocator ServiceLocatorProvider();

        #endregion Delegates and Events

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Sets the value for <see cref="ServiceLocator.Current" />.
        /// </summary>
        /// <param name="newLocator">The new value.</param>
        public static void SetLocator(IServiceLocator newLocator)
        {
            SetLocatorProvider(newLocator == null ? null : new ServiceLocatorProvider(delegate()
                {
                    return newLocator;
                }));
        }

        /// <summary>
        /// Sets the provider delegate that returns the object for <see cref="ServiceLocator.Current" />.
        /// </summary>
        /// <param name="newProvider">The new provider.</param>
        public static void SetLocatorProvider(ServiceLocatorProvider newProvider)
        {
            _provider = newProvider;
        }

        #endregion Methods
    }
}
