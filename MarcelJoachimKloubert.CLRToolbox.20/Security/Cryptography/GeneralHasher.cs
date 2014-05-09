// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography
{
    /// <summary>
    /// A general hasher that is based on a <see cref="HashAlgorithm" />.
    /// </summary>
    public class GeneralHasher : HasherBase
    {
        #region Fields (2)

        private readonly HashAlgorithm _ALGORITHM;

        /// <summary>
        /// Stores the optional salt.
        /// </summary>
        protected readonly byte[] _SALT;

        #endregion Fields

        #region Constructors (2)

        /// <inheriteddoc />
        public GeneralHasher(HashAlgorithm algo, IEnumerable<byte> salt)
            : base()
        {
            if (algo == null)
            {
                throw new ArgumentNullException("algo");
            }

            this._ALGORITHM = algo;
            this._SALT = CollectionHelper.AsArray(salt);
        }

        /// <inheriteddoc />
        public GeneralHasher(HashAlgorithm algo)
            : this(algo, null)
        {
        }

        #endregion Constructors

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Creates a new instance with a specific <see cref="HashAlgorithm" />.
        /// </summary>
        /// <typeparam name="TAlgo">Type of the algorithm to use.</typeparam>
        /// <returns>The created instance.</returns>
        public static GeneralHasher Create<TAlgo>()
            where TAlgo : global::System.Security.Cryptography.HashAlgorithm, new()
        {
            return Create<TAlgo>(null);
        }

        /// <summary>
        /// Creates a new instance with a specific <see cref="HashAlgorithm" />.
        /// </summary>
        /// <typeparam name="TAlgo">Type of the algorithm to use.</typeparam>
        /// <param name="salt">The optional salt.</param>
        /// <returns>The created instance.</returns>
        public static GeneralHasher Create<TAlgo>(IEnumerable<byte> salt)
            where TAlgo : global::System.Security.Cryptography.HashAlgorithm, new()
        {
            return new GeneralHasher(new TAlgo(),
                                     salt);
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override sealed void OnHash(Stream srcStream, Stream targetStream)
        {
            byte[] hash;
            using (MemoryStream temp = new MemoryStream())
            {
                IOHelper.CopyTo(srcStream, temp);

                if (this._SALT != null)
                {
                    // use salt

                    temp.Write(this._SALT,
                               0,
                               this._SALT.Length);
                }

                temp.Position = 0;
                hash = this._ALGORITHM
                           .ComputeHash(temp);
            }

            targetStream.Write(hash, 0, hash.Length);
        }

        #endregion Methods
    }
}