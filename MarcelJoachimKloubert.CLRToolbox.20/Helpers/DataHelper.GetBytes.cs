// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Returns the value of a field inside a <see cref="IDataRecord" /> as byte array.
        /// </summary>
        /// <param name="rec">The record from where to read the value from.</param>
        /// <param name="ordinal">The ordinal of the field inside the data record where the value is stored.</param>
        /// <returns>The read value.</returns>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rec" /> is <see langword="null" />.
        /// </exception>
        public static byte[] GetBytes(IDataRecord rec, int ordinal)
        {
            return FromDbValue<byte[]>(rec, ordinal);
        }

        /// <summary>
        /// Returns the value of a field inside a <see cref="IDataRecord" /> as byte array.
        /// </summary>
        /// <param name="rec">The record from where to read the value from.</param>
        /// <param name="name">The name of the field inside the data record where the value is stored.</param>
        /// <returns>The read value.</returns>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rec" /> is <see langword="null" />.
        /// </exception>
        public static byte[] GetBytes(IDataRecord rec, IEnumerable<char> name)
        {
            return FromDbValue<byte[]>(rec, name);
        }

        #endregion Methods
    }
}
