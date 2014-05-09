// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Diagnostics;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation
{
    /// <summary>
    /// Is thrown if a service could be be located by a <see cref="IServiceLocator" /> object.
    /// </summary>
    [DebuggerDisplay("ServiceActivationException = {ServiceType}; {Key}")]
    public partial class ServiceActivationException : Exception
    {
        #region Fields (3)

        private readonly object _KEY;
        private readonly Type _SERVICE_TYPE;
        /// <summary>
        /// Stores the default message of that exception.
        /// </summary>
        public const string DEFAULT_EXCEPTION_MESSAGE = "Could be locate service by given type and key!";

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceActivationException" /> class.
        /// </summary>
        /// <param name="serviceType">Value for <see cref="ServiceActivationException.ServiceType" /> property.</param>
        /// <param name="key">Value for <see cref="ServiceActivationException.Key" /> property.</param>
        /// <param name="innerException">The optional inner exception.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceType" /> is <see langword="null" />.
        /// </exception>
        public ServiceActivationException(Type serviceType, object key,
                                          Exception innerException)
            : base(DEFAULT_EXCEPTION_MESSAGE,
                   innerException)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            this._SERVICE_TYPE = serviceType;
            this._KEY = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceActivationException" /> class.
        /// </summary>
        /// <param name="serviceType">Value for <see cref="ServiceActivationException.ServiceType" /> property.</param>
        /// <param name="key">Value for <see cref="ServiceActivationException.Key" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceType" /> is <see langword="null" />.
        /// </exception>
        public ServiceActivationException(Type serviceType, object key)
            : base(DEFAULT_EXCEPTION_MESSAGE)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            this._SERVICE_TYPE = serviceType;
            this._KEY = key;
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the service key.
        /// </summary>
        public object Key
        {
            get { return this._KEY; }
        }

        /// <summary>
        /// Gets the type of the underlying service.
        /// </summary>
        public Type ServiceType
        {
            get { return this._SERVICE_TYPE; }
        }

        #endregion Properties
    }
}
