// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class IOHelper
    {
        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Extracts the data of a stream to a byte array. This is similar to <see cref="MemoryStream.ToArray()" />.
        /// </summary>
        /// <param name="stream">The stream from where to extract the data from.</param>
        /// <returns>The extracted data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        public static byte[] ToByteArray(Stream stream)
        {
            return ToByteArrayInner(stream, null);
        }

        /// <summary>
        /// Extracts the data of a stream to a byte array. This is similar to <see cref="MemoryStream.ToArray()" />.
        /// </summary>
        /// <param name="stream">The stream from where to extract the data from.</param>
        /// <param name="bufferSize">The buffer size for the copy operation.</param>
        /// <returns>The extracted data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is invalid.
        /// </exception>
        /// <remarks>
        /// <paramref name="bufferSize" /> is ignored if <paramref name="stream" /> is a <see cref="MemoryStream" />.
        /// </remarks>
        public static byte[] ToByteArray(Stream stream, int bufferSize)
        {
            return ToByteArrayInner(stream, bufferSize);
        }
        // Private Methods (1) 

        private static byte[] ToByteArrayInner(Stream stream, int? bufferSize)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (bufferSize < 1)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }

            MemoryStream ms = stream as MemoryStream;
            if (ms != null)
            {
                return ms.ToArray();
            }

            using (ms = new MemoryStream())
            {
                if (bufferSize.HasValue)
                {
                    CopyTo(stream, ms, bufferSize.Value);
                }
                else
                {
                    CopyTo(stream, ms);
                }

                return ms.ToArray();
            }
        }

        #endregion Methods
    }
}
