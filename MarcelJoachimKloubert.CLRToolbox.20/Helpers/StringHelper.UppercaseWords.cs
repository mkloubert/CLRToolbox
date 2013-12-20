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
        /// <returns>
        /// The converted chars or <see langword="null" /> if <paramref name="chars" />
        /// is also <see langword="null" />.
        /// </returns>
        public static string UppercaseWords(IEnumerable<char> chars,
                                            bool lowerCaseOtherChars)
        {
            if (chars == null)
            {
                return null;
            }

            bool nextCharIsUpper = true;
            StringBuilder result = new StringBuilder();
            foreach (char c in chars)
            {
                char? charToAppend = null;

                if (char.IsWhiteSpace(c))
                {
                    // whitespace => next char is upper
                    nextCharIsUpper = true;
                }
                else
                {
                    if (nextCharIsUpper)
                    {
                        // first char of word

                        charToAppend = char.ToUpper(c);
                        nextCharIsUpper = false;
                    }
                    else
                    {
                        if (lowerCaseOtherChars)
                        {
                            charToAppend = char.ToLower(c);
                        }
                        else
                        {
                            charToAppend = c;
                        }
                    }
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
