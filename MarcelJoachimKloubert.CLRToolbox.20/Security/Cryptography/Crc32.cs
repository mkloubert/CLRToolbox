// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography
{
    /// <summary>
    /// This is a hasher that is based on CRC-32 algorithm.
    /// The class is based on Damien Guard's implementation that can be found at <see href="https://github.com/damieng/DamienGKit" />.
    /// </summary>
    public class Crc32 : HashAlgorithm
    {
        #region Fields (5)

        private uint _hash;
        private readonly uint _SEED;
        private readonly uint[] _TABLE;

        /// <summary>
        /// Stores the default polynomial value.
        /// </summary>
        public const uint DEFAULT_POLYNOMIAL = 0xEDB88320u;

        /// <summary>
        /// Stores the default seed value.
        /// </summary>
        public const uint DEFAULT_SEED = 0xFFFFFFFFu;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc32"/> class.
        /// </summary>
        /// <param name="polynomial">The custom polynomial value.</param>
        /// <param name="seed">The custom seed value.</param>
        public Crc32(uint polynomial, uint seed)
        {
            this._TABLE = InitializeTable(polynomial);
            this._SEED = _hash = seed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc32" /> class.
        /// </summary>
        public Crc32()
            : this(DEFAULT_POLYNOMIAL, DEFAULT_SEED)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <inheriteddoc />
        public override int HashSize
        {
            get { return 32; }
        }

        #endregion Properties

        #region Methods (6)

        // Public Methods (1) 

        /// <inheriteddoc />
        public override void Initialize()
        {
            this._hash = this._SEED;
        }
        // Protected Methods (2) 

        /// <inheriteddoc />
        protected override void HashCore(byte[] buffer, int start, int length)
        {
            this._hash = CalculateHash(this._TABLE, this._hash,
                                       buffer, start, length);
        }

        /// <inheriteddoc />
        protected override byte[] HashFinal()
        {
            return this.HashValue = UInt32ToBigEndianBytes(~this._hash);
        }
        // Private Methods (3) 

        private static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
        {
            uint result = seed;
            for (int i = start; i < size - start; i++)
            {
                result = (result >> 8) ^ table[buffer[i] ^ result & 0xFF];
            }

            return result;
        }

        private static uint[] InitializeTable(uint polynomial)
        {
            uint[] result = new uint[256];
            for (int i = 0; i < result.Length; i++)
            {
                uint entry = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ polynomial;
                    }
                    else
                    {
                        entry = entry >> 1;
                    }
                }

                result[i] = entry;
            }

            return result;
        }

        private static byte[] UInt32ToBigEndianBytes(uint uint32)
        {
            byte[] result = BitConverter.GetBytes(uint32);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(result);
            }

            return result;
        }

        #endregion Methods
    }
}
