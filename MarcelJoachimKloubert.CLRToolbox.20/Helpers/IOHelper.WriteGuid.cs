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
        /// Writes a GUID to a binary writer.
        /// </summary>
        /// <param name="writer">The target writer.</param>
        /// <param name="value">The value to write.</param>
        /// <exception cref="ArgumentNullException"><paramref name="writer" /> is <see langword="null" />.</exception>
        public static void WriteGuid(BinaryWriter writer, Guid value)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.Write(value.ToByteArray());
        }

        #endregion Methods
    }
}