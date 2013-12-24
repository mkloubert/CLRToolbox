// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class IOHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Reads the next 16 bytes and return them as <see cref="Guid" />.
        /// </summary>
        /// <param name="reader">The reader from where to read the data from.</param>
        /// <returns>The read data as GUID.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reader" /> is <see langword="null" />.</exception>
        /// <exception cref="EndOfStreamException">Not nough data available for a GUID.</exception>
        public static Guid ReadGuid(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            const int BYTES_TO_READ = 16;

            byte[] data = reader.ReadBytes(BYTES_TO_READ);
            if (data.Length < BYTES_TO_READ)
            {
                throw new EndOfStreamException("Not enough data for a GUID!");
            }

            return new Guid(data);
        }

        #endregion Methods
    }
}
