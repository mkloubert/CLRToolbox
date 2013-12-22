// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Converts each first char of a word to upper case.
        /// </summary>
        /// <param name="chars">The chars to convert.</param>
        /// <returns>
        /// The converted chars or <see langword="null" /> if <paramref name="chars" />
        /// is also <see langword="null" />.
        /// </returns>
        /// <remarks>
        /// Other chars of words are NOT converted to lower case.
        /// </remarks>
        public static string UppercaseWords(IEnumerable<char> chars)
        {
            return UppercaseWords(chars, false);
        }

        /// <summary>
        /// Converts each first char of a word to upper case.
        /// </summary>
        /// <param name="chars">The chars to convert.</param>
        /// <param name="lowerCaseOtherChars">
        /// Convert other chars of words (beginning at second one) to lower case.
        /// </param>
        /// <returns>The converted chars.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="chars" /> is <see langword="null" />.</exception>
        public static string UppercaseWords(IEnumerable<char> chars,
                                            bool lowerCaseOtherChars)
        {
            if (chars == null)
            {
                throw new ArgumentNullException("chars");
            }

            bool nextIsUpper = true;
            StringBuilder result = new StringBuilder();
            foreach (char c in chars)
            {
                char? charToAppend = null;

                if (char.IsLetterOrDigit(c))
                {
                    if (nextIsUpper)
                    {
                        // first letter of word

                        charToAppend = char.ToUpper(c);
                        nextIsUpper = false;
                    }
                    else
                    {
                        charToAppend = lowerCaseOtherChars ? char.ToLower(c) : c;
                    }
                }
                else
                {
                    nextIsUpper = true;
                    charToAppend = c;
                }

                if (charToAppend.HasValue)
                {
                    result.Append(charToAppend.Value);
                }
            }

            return result.ToString();
        }

        #endregion Methods
    }
}
