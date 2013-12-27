// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Security;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography
{
    partial interface IHasher
    {
        #region Operations (2)

        /// <summary>
        /// Hashes a secure string as UTF-8 data.
        /// </summary>
        /// <param name="secStr">The string to hash.</param>
        /// <returns>The hash of <paramref name="secStr" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="secStr" /> is <see langword="null" />.
        /// </exception>
        byte[] Hash(SecureString secStr);

        /// <summary>
        /// Hashes a char sequence.
        /// </summary>
        /// <param name="secStr">The string to hash.</param>
        /// <param name="enc">The encoding to use.</param>
        /// <returns>The hash of <paramref name="secStr" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="secStr" /> and/or <paramref name="enc" /> are <see langword="null" />.
        /// </exception>
        byte[] Hash(SecureString secStr, Encoding enc);

        #endregion Operations
    }
}
