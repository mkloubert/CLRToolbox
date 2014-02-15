// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CloudNET.SDK
{
    /// <summary>
    /// A basic object that belongs to a <see cref="CloudServer" />.
    /// </summary>
    public abstract class CloudServerObjectBase
    {
        #region Fields (2)

        private readonly CloudServer _SERVER;
        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudServerObjectBase"/> class.
        /// </summary>
        /// <param name="server">The value for the <see cref="CloudServerObjectBase.Server" /> property.</param>
        /// <param name="sync">The value for the <see cref="CloudServerObjectBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="server" /> and/or <paramref name="sync" /> are <see langword="null" />.
        /// </exception>
        protected CloudServerObjectBase(CloudServer server, object sync)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            if (sync == null)
            {
                throw new ArgumentNullException("sync");
            }

            this._SERVER = server;
            this._SYNC = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudServerObjectBase"/> class.
        /// </summary>
        /// <param name="server">The value for the <see cref="CloudServerObjectBase.Server" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="server" /> is <see langword="null" />.
        /// </exception>
        protected CloudServerObjectBase(CloudServer server)
            : this(server, new object())
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the server that object belongs to.
        /// </summary>
        public CloudServer Server
        {
            get { return this._SERVER; }
        }

        #endregion Properties
    }
}
