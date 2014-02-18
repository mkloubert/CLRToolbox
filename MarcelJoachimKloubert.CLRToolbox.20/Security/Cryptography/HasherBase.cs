// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography
{
    /// <summary>
    /// A basic hasher.
    /// </summary>
    public abstract partial class HasherBase : TMObject, IHasher
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HasherBase" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HasherBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HasherBase" /> class.
        /// </summary>
        protected HasherBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (4)

        // Public Methods (3) 

        /// <inheriteddoc />
        public byte[] Hash(IEnumerable<byte> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            using (MemoryStream srcStream = new MemoryStream(CollectionHelper.AsArray(data), false))
            {
                using (MemoryStream targetStream = new MemoryStream())
                {
                    this.OnHash(srcStream, targetStream);

                    return targetStream.ToArray();
                }
            }
        }

        /// <inheriteddoc />
        public byte[] Hash(IEnumerable<char> chars)
        {
            return this.Hash(chars, Encoding.UTF8);
        }

        /// <inheriteddoc />
        public byte[] Hash(IEnumerable<char> chars, Encoding enc)
        {
            if (chars == null)
            {
                throw new ArgumentNullException("chars");
            }

            if (enc == null)
            {
                throw new ArgumentNullException("enc");
            }

            return this.Hash(enc.GetBytes(StringHelper.AsString(chars)));
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for <see cref="HasherBase.Hash(IEnumerable{byte})" /> method.
        /// </summary>
        /// <param name="srcStream">The stream with the data that should be hashed.</param>
        /// <param name="targetStream">The stream where to write the hash to.</param>
        protected abstract void OnHash(Stream srcStream, Stream targetStream);

        #endregion Methods
    }
}
