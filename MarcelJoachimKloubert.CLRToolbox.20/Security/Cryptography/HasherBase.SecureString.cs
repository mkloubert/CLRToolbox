// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography
{
    partial class HasherBase
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasher.Hash(SecureString)" />
        public byte[] Hash(SecureString secStr)
        {
            return this.Hash(secStr, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasher.Hash(SecureString, Encoding)" />
        public byte[] Hash(SecureString secStr, Encoding enc)
        {
            return this.Hash(StringHelper.ToUnsecureString(secStr),
                             enc);
        }

        #endregion Methods
    }
}
