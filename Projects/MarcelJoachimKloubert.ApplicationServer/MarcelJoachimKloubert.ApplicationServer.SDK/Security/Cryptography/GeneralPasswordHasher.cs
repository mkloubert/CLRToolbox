// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using MarcelJoachimKloubert.CLRToolbox.Security.Cryptography;

namespace MarcelJoachimKloubert.ApplicationServer.Security.Cryptography
{
    /// <summary>
    /// A general bassword hash that is based on a <see cref="HashAlgorithm" />.
    /// </summary>
    public class GeneralPasswordHasher : GeneralHasher, IPasswordHasher
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPasswordHasher" /> class.
        /// </summary>
        /// <param name="algo">The algoithm to use.</param>
        /// <param name="salt">The optional salt.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="algo" /> is <see langword="null" />.
        /// </exception>
        public GeneralPasswordHasher(HashAlgorithm algo,
                                     IEnumerable<byte> salt = null)
            : base(algo, salt)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPasswordHasher" /> class
        /// by using SHA-256 as hash algorithm.
        /// </summary>
        /// <param name="salt">The optional salt.</param>
        public GeneralPasswordHasher(IEnumerable<byte> salt = null)
            : this(new SHA256Managed(), salt)
        {

        }

        #endregion Constructors
    }
}
