// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Factories
{
    partial class EqualityComparerFactory
    {
        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// Creates a case insensitive <see cref="IEqualityComparer{T}" /> for <see cref="string" />.
        /// </summary>
        /// <returns>The created comparer.</returns>
        /// /// <remarks>
        /// Strings are trimmed and empty strings are handled as <see langword="null" /> references.
        /// </remarks>
        public static IEqualityComparer<string> CreateCaseInsensitiveStringComparer()
        {
            return CreateCaseInsensitiveStringComparer(true);
        }

        /// <summary>
        /// Creates s case insensitive <see cref="IEqualityComparer{T}" /> for <see cref="string" />.
        /// </summary>
        /// <param name="trim">Compare string trimmed or not.</param>
        /// <returns>The created comparer.</returns>
        /// <remarks>
        /// Empty strings are handled as <see langword="null" /> references.
        /// </remarks>
        public static IEqualityComparer<string> CreateCaseInsensitiveStringComparer(bool trim)
        {
            return CreateCaseInsensitiveStringComparer(trim, true);
        }

        /// <summary>
        /// Creates an case insensitive <see cref="IEqualityComparer{T}" /> for <see cref="string" />.
        /// </summary>
        /// <param name="trim">Compare string trimmed or not.</param>
        /// <param name="emptyIsNull">Handle empty strings as <see langword="null" /> reference or not.</param>
        /// <returns>The created comparer.</returns>
        public static IEqualityComparer<string> CreateCaseInsensitiveStringComparer(bool trim, bool emptyIsNull)
        {
            return new DelegateEqualityComparer<string>(delegate(string x, string y)
            {
                return ParseString(x, trim, emptyIsNull) ==
                       ParseString(y, trim, emptyIsNull);
            }, delegate(string obj)
            {
                string str = ParseString(obj, trim, emptyIsNull);
                return str != null ? str.GetHashCode() : 0;
            });
        }

        /// <summary>
        /// Creates a case insensitive <see cref="IEqualityComparer{T}" /> for use in HTTP context.
        /// </summary>
        /// <returns>The created comparer.</returns>
        public static IEqualityComparer<string> CreateHttpKeyComparer()
        {
            return CreateCaseInsensitiveStringComparer(true, true);
        }
        // Private Methods (1) 

        private static string ParseString(string str, bool trim, bool emptyIsNull)
        {
            string result = str;

            if (result != null)
            {
                result = result.ToLower();

                if (trim)
                {
                    result = result.Trim();
                }

                if (result == string.Empty &&
                    emptyIsNull)
                {
                    result = null;
                }
            }

            return result;
        }

        #endregion Methods
    }
}
