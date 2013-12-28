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
        /// UNcompresses binary data via GZIP.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <returns>The decompressed data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <see langword="null" />.
        /// </exception>
        public static byte[] GUnzip(IEnumerable<byte> data)
        {
            using (MemoryStream src = new MemoryStream(CollectionHelper.AsArray(data),
                                                       false))
            {
                return GUnzip(src);
            }
        }

        /// <summary>
        /// UNcompresses the data of a source stream via GZIP.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <returns>The decompressed data.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="src" /> cannot be read.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public static byte[] GUnzip(Stream src)
        {
            using (MemoryStream dest = new MemoryStream())
            {
                GUnzip(src, dest);
                return dest.ToArray();
            }
        }

        /// <summary>
        /// UNcompresses the data of a source stream via GZIP.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="bufferSize">The buffer size for read operation to use.</param>
        /// <returns>The decompressed data.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="src" /> cannot be read.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        public static byte[] GUnzip(Stream src, int bufferSize)
        {
            return GUnzipInner(src, bufferSize);
        }

        /// <summary>
        /// UNcompresses binary data to a destination stream via GZIP.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="dest">The destination stream.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="dest" /> cannot be written.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public static void GUnzip(IEnumerable<byte> data, Stream dest)
        {
            using (MemoryStream src = new MemoryStream(CollectionHelper.AsArray(data), false))
            {
                GUnzip(src, dest);
            }
        }

        /// <summary>
        /// UNcompresses the data of a source stream to a destination stream via GZIP.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="src" /> cannot be read and/or <paramref name="dest" /> cannot be written.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public static void GUnzip(Stream src, Stream dest)
        {
            GUnzipInner(src, dest, null);
        }

        /// <summary>
        /// UNcompresses the data of a source stream to a destination stream via GZIP.
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
        public static void GUnzip(Stream src, Stream dest, int bufferSize)
        {
            GUnzipInner(src, dest, bufferSize);
        }
        // Private Methods (2) 

        private static byte[] GUnzipInner(Stream src, int? bufferSize)
        {
            using (MemoryStream dest = new MemoryStream())
            {
                GZipInner(src, dest, bufferSize);

                return dest.ToArray();
            }
        }

        private static void GUnzipInner(Stream src, Stream dest, int? bufferSize)
        {
            using (GZipStream gunzip = new GZipStream(src, CompressionMode.Decompress, true))
            {
                if (bufferSize.HasValue)
                {
                    IOHelper.CopyTo(gunzip, dest, bufferSize.Value);
                }
                else
                {
                    IOHelper.CopyTo(gunzip, dest);
                }

                gunzip.Flush();
            }
        }

        #endregion Methods
    }
}
