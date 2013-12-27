// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography
{
    /// <summary>
    /// Describes a hasher.
    /// </summary>
    public partial interface IHasher : ITMObject
    {
        #region Operations (3)

        /// <summary>
        /// Hashes data.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>The hash of <paramref name="data" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <see langword="null" />.
        /// </exception>
        byte[] Hash(IEnumerable<byte> data);

        /// <summary>
        /// Hashes UTF-8 char sequence.
        /// </summary>
        /// <param name="chars">The chars to hash.</param>
        /// <returns>The hash of <paramref name="chars" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="chars" /> is <see langword="null" />.
        /// </exception>
        byte[] Hash(IEnumerable<char> chars);

        /// <summary>
        /// Hashes a char sequence.
        /// </summary>
        /// <param name="chars">The chars to hash.</param>
        /// <param name="enc">The encoding to use.</param>
        /// <returns>The hash of <paramref name="chars" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="chars" /> and/or <paramref name="enc" /> are <see langword="null" />.
        /// </exception>
        byte[] Hash(IEnumerable<char> chars, Encoding enc);

        #endregion Operations
    }
}
