// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class IOHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Copies the data of a source stream to a target.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="src" /> is not readable and/or 
        /// <paramref name="dest" /> is not writable.
        /// </exception>
        public static void CopyTo(Stream src, Stream dest)
        {
            CopyTo(src, dest, DEFAULT_STREAM_BUFFER_SIZE);
        }

        /// <summary>
        /// Copies the data of a source stream to a target.
        /// </summary>
        /// <param name="src">The source stream.</param>
        /// <param name="dest">The destination stream.</param>
        /// <param name="bufferSize">The size of the buffer to use.</param>
        public static void CopyTo(Stream src, Stream dest, int bufferSize)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (dest == null)
            {
                throw new ArgumentNullException("dest");
            }

            if (bufferSize < 1)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }

            if (src.CanRead == false)
            {
                throw new IOException("Source is not readable!");
            }

            if (dest.CanWrite == false)
            {
                throw new IOException("Destination is not writable!");
            }

            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            while ((bytesRead = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, bytesRead);
            }
        }

        #endregion Methods
    }
}
