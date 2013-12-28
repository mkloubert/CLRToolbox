// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CompressionHelper
    {
        #region Methods (8)

        // Public Methods (6) 

        /// <summary>
        /// Compresses binary data via GZIP.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <returns>The compressed data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <see langword="null" />.
        /// </exception>
        public static byte[] GZip(IEnumerable<byte> data)
        {
            using (MemoryStream src = new MemoryStream(CollectionHelper.AsArray(data),
                                                       false))
            {
                return GZip(src);
            }
        }

        /// <summary>
        /// Compresses the data of a source stream via GZIP.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <returns>The compressed data.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="src" /> cannot be read.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public static byte[] GZip(Stream src)
        {
            using (MemoryStream dest = new MemoryStream())
            {
                GZip(src, dest);
                return dest.ToArray();
            }
        }

        /// <summary>
        /// Compresses the data of a source stream via GZIP.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="bufferSize">The buffer size for read operation to use.</param>
        /// <returns>The compressed data.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="src" /> cannot be read.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        public static byte[] GZip(Stream src, int bufferSize)
        {
            return GZipInner(src, bufferSize);
        }

        /// <summary>
        /// Compresses binary data to a destination stream via GZIP.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="dest">The destination stream.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="dest" /> cannot be written.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public static void GZip(IEnumerable<byte> data, Stream dest)
        {
            using (MemoryStream src = new MemoryStream(CollectionHelper.AsArray(data), false))
            {
                GZip(src, dest);
            }
        }

        /// <summary>
        /// Compresses the data of a source stream to a destination stream via GZIP.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="src" /> cannot be read and/or <paramref name="dest" /> cannot be written.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public static void GZip(Stream src, Stream dest)
        {
            GZipInner(src, dest, null);
        }

        /// <summary>
        /// Compresses the data of a source stream to a destination stream via GZIP.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <param name="bufferSize">The buffer size for read operation to use.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="src" /> cannot be read and/or <paramref name="dest" /> cannot be written.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        public static void GZip(Stream src, Stream dest, int bufferSize)
        {
            GZipInner(src, dest, bufferSize);
        }
        // Private Methods (2) 

        private static byte[] GZipInner(Stream src, int? bufferSize)
        {
            using (MemoryStream dest = new MemoryStream())
            {
                GZipInner(src, dest, bufferSize);

                return dest.ToArray();
            }
        }

        private static void GZipInner(Stream src, Stream dest, int? bufferSize)
        {
            using (GZipStream gzip = new GZipStream(dest, CompressionMode.Compress, true))
            {
                if (bufferSize.HasValue)
                {
                    IOHelper.CopyTo(src, gzip, bufferSize.Value);
                }
                else
                {
                    IOHelper.CopyTo(src, gzip);
                }

                gzip.Flush();
            }
        }

        #endregion Methods
    }
}
