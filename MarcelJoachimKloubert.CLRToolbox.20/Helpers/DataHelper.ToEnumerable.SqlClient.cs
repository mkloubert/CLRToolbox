// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Converts a SQL Client data reader to a lazy sequence of SQL Client data records.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The sequence of data records.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<SqlDataReader> ToEnumerable(SqlDataReader reader)
        {
            return ToEnumerableCommon(reader);
        }

        #endregion Methods
    }
}
