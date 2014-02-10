// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Security;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Appends a ist of chars to a <see cref="SecureString" />
        /// </summary>
        /// <param name="secStr">The secure string where to append the data to.</param>
        /// <param name="charsToAppend">The data to append.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="secStr" /> is <see langword="null" />.
        /// </exception>
        public static void Append(SecureString secStr, IEnumerable<char> charsToAppend)
        {
            if (secStr == null)
            {
                throw new ArgumentNullException("secStr");
            }

            if (charsToAppend != null)
            {
                foreach (char c in charsToAppend)
                {
                    secStr.AppendChar(c);
                }
            }
        }

        #endregion Methods
    }
}
