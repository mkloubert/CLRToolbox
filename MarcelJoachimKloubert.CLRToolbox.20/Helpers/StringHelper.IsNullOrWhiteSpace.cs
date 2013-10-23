// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Checks if a char array is <see langword="null" /> or contains whitespaces only.
        /// </summary>
        /// <param name="chars">The sequence to check.</param>
        /// <returns>Is <see langword="null" /> or contains whitespaces only. Otherwise <see langword="false" />.</returns>
        public static bool IsNullOrWhiteSpace(IEnumerable<char> chars)
        {
            if (chars == null)
            {
                return true;
            }

            foreach (char c in chars)
            {
                if (char.IsWhiteSpace(c) == false)
                {
                    // a non-whitespace character found
                    return false;
                }
            }

            return true;
        }

        #endregion Methods
    }
}
