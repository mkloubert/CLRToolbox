// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Net
{
    /// <summary>
    /// Simple implementation of <see cref="ITcpAddress" /> interface.
    /// </summary>
    public sealed class SimpleTcpAddress : ITcpAddress
    {
        #region Fields (2)

        private string _address;
        private int _port;

        #endregion Properties

        #region Properties (2)

        /// <inheriteddoc />
        public string Address
        {
            get { return this._address; }

            set { this._address = value; }
        }

        /// <inheriteddoc />
        public int Port
        {
            get { return this._port; }

            set { this._port = value; }
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return string.Format("{0}:{1}",
                                 this.Address, this.Port);
        }

        #endregion Methods
    }
}