// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;
using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Reads a value from a data record.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="rec">The record from where to read the value from.</param>
        /// <param name="name">The name of the field inside the data record where the value is stored.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rec" /> is <see langword="null" />.
        /// </exception>
        public static T FromDbValue<T>(IDataRecord rec, IEnumerable<char> name)
        {
            if (rec == null)
            {
                throw new ArgumentNullException("rec");
            }

            return FromDbValue<T>(rec,
                                  rec.GetOrdinal(StringHelper.AsString(name)));
        }

        /// <summary>
        /// Reads a value from a data record.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="rec">The record from where to read the value from.</param>
        /// <param name="ordinal">The ordinal of the field inside the data record where the value is stored.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rec" /> is <see langword="null" />.
        /// </exception>
        public static T FromDbValue<T>(IDataRecord rec, int ordinal)
        {
            if (rec == null)
            {
                throw new ArgumentNullException("rec");
            }

            return GlobalConverter.Current
                                  .ChangeType<T>(rec.IsDBNull(ordinal) ? null : rec.GetValue(ordinal));
        }

        #endregion Methods
    }
}
