// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Converts an object so it can be used as parameter in a <see cref="IDbCommand" /> e.g.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The converted object.</returns>
        public static object ToDbValue(object obj)
        {
            return obj ?? DBNull.Value;
        }

        #endregion Methods
    }
}
