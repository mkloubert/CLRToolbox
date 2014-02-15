// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography.Passwords
{
    /// <summary>
    /// A general password hash that is based on a <see cref="HashAlgorithm" />.
    /// </summary>
    public class GeneralPasswordHasher : GeneralHasher, IPasswordHasher
    {
        #region Constructors (4)

        /// <inheriteddoc />
        public GeneralPasswordHasher(HashAlgorithm algo, IEnumerable<byte> salt)
            : base(algo, salt)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPasswordHasher" /> class
        /// by using SHA-256 as hash algorithm.
        /// </summary>
        /// <param name="salt">The optional salt.</param>
        public GeneralPasswordHasher(IEnumerable<byte> salt)
            : this(new SHA256Managed(), salt)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPasswordHasher" /> class.
        /// </summary>
        /// <param name="algo">The algoithm to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="algo" /> is <see langword="null" />.
        /// </exception>
        public GeneralPasswordHasher(HashAlgorithm algo)
            : this(algo, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPasswordHasher" /> class.
        /// </summary>
        public GeneralPasswordHasher()
            : this(new SHA256Managed())
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <inheriteddoc />
        public byte[] Salt
        {
            get { return this._SALT; }
        }

        #endregion Properties
    }
}
