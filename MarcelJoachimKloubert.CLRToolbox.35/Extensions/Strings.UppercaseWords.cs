// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="StringHelper.UppercaseWords(IEnumerable{char})" />
        public static string UppercaseWords(this IEnumerable<char> chars)
        {
            return StringHelper.UppercaseWords(chars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="StringHelper.UppercaseWords(IEnumerable{char}, bool)" />
        public static string UppercaseWords(this IEnumerable<char> chars,
                                            bool lowerCaseOtherChars)
        {
            return StringHelper.UppercaseWords(chars,
                                               lowerCaseOtherChars);
        }

        #endregion Methods
    }
}
