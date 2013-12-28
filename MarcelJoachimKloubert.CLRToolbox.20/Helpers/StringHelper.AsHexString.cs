// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Converts a byte sequence to a lower case hex string.
        /// </summary>
        /// <param name="binData">The binary data / sequence.</param>
        /// <returns>
        /// The converted to data or <see langword="null" /> if <paramref name="binData" />
        /// is also a <see langword="null" /> reference.
        /// </returns>
        /// <remarks>Result string is returned in lower case chars.</remarks>
        public static string AsHexString(IEnumerable<byte> binData)
        {
            return AsHexString(binData, true);
        }

        /// <summary>
        /// Converts a byte sequence to a hex string.
        /// </summary>
        /// <param name="binData">The binary data / sequence.</param>
        /// <param name="lowerCase">Return in lower case chars or not.</param>
        /// <returns>
        /// The converted to data or <see langword="null" /> if <paramref name="binData" />
        /// is also a <see langword="null" /> reference.
        /// </returns>
        public static string AsHexString(IEnumerable<byte> binData, bool lowerCase)
        {
            if (binData == null)
            {
                return null;
            }

            StringBuilder result = new StringBuilder();
            foreach (byte b in binData)
            {
                if (lowerCase)
                {
                    result.AppendFormat("{0:x2}", b);
                }
                else
                {
                    result.AppendFormat("{0:X2}", b);
                }
            }

            return result.ToString();
        }

        #endregion Methods
    }
}
