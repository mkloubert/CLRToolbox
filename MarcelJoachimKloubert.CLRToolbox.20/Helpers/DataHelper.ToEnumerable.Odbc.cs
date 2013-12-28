// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Converts an ODBC data reader to a lazy sequence of ODBC data records.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The sequence of data records.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<OdbcDataReader> ToEnumerable(OdbcDataReader reader)
        {
            return ToEnumerableCommon(reader);
        }

        #endregion Methods
    }
}
