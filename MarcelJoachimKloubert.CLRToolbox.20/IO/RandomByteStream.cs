// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Security.Cryptography;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A stream that returns random bytes when reading.
    /// </summary>
    public abstract class RandomByteStream : ReadOnlyStreamBase
    {
        #region Fields (1)

        private readonly Action<byte[]> _RANDOM_BYTE_FILLER;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomByteStream"/> class.
        /// </summary>
        /// <param name="rng">The random number generator to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rng" /> is <see langword="null" />.
        /// </exception>
        public RandomByteStream(RNGCryptoServiceProvider rng)
        {
            if (rng == null)
            {
                throw new ArgumentNullException("rng");
            }

            this._RANDOM_BYTE_FILLER = delegate(byte[] buffer)
            {
                rng.GetBytes(buffer);
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomByteStream"/> class.
        /// </summary>
        /// <param name="r">The random number generator to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="r" /> is <see langword="null" />.
        /// </exception>
        public RandomByteStream(Random r)
        {
            if (r == null)
            {
                throw new ArgumentNullException("r");
            }

            this._RANDOM_BYTE_FILLER = delegate(byte[] buffer)
            {
                r.NextBytes(buffer);
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomByteStream"/> class
        /// with a new <see cref="RNGCryptoServiceProvider" /> as random data generator.
        /// </summary>
        public RandomByteStream()
            : this(new RNGCryptoServiceProvider())
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnRead(byte[] buffer, int offset, int count, ref int bytesRead)
        {
            byte[] randomData = new byte[count];
            this._RANDOM_BYTE_FILLER(randomData);

            for (int i = 0; i < randomData.Length; i++)
            {
                buffer[offset + i] = randomData[i];
            }
        }

        #endregion Methods
    }
}
