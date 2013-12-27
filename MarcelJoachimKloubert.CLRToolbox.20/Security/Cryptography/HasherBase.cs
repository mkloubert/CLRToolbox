// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

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

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasher.Hash(IEnumerable{byte})" />
        public byte[] Hash(IEnumerable<byte> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            using (MemoryStream targetStream = new MemoryStream())
            {
                this.OnHash(CollectionHelper.AsArray(data),
                            targetStream);

                return targetStream.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasher.Hash(IEnumerable{char})" />
        public byte[] Hash(IEnumerable<char> chars)
        {
            return this.Hash(chars, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasher.Hash(IEnumerable{char}, Encoding)" />
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
        /// <param name="data">The data to hash.</param>
        /// <param name="targetStream">The stream where to write the hash to.</param>
        protected abstract void OnHash(byte[] data, Stream targetStream);

        #endregion Methods
    }
}
