// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security.Cryptography;

namespace MarcelJoachimKloubert.CLRToolbox.Objects
{
    partial class ObjectContextBase
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// Creates the object that uses the algorithm to hash that context.
        /// </summary>
        /// <returns>The created hash algorithm.</returns>
        protected HashAlgorithm CreateHasher()
        {
            return new SHA512Managed();
        }

        #endregion Methods
    }
}
