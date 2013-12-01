// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Net
{
    /// <summary>
    /// Simple implementation of <see cref="ITcpAddress" /> interface.
    /// </summary>
    public sealed class SimpleTcpAddress : ITcpAddress
    {
        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITcpAddress.Address" />
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITcpAddress.Port" />
        public int Port
        {
            get;
            set;
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
