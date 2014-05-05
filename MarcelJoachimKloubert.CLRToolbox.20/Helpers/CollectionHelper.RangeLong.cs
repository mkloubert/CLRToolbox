// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (2)

        /// <summary>
        /// Generates a sequence of integral numbers within a specified range.
        /// </summary>
        /// <param name="count">The number of sequential longs to generate.</param>
        /// <returns>The generated sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count" /> is smaller than 0.
        /// </exception>
        /// <remarks>The first value, if not empty, is 0.</remarks>
        public static IEnumerable<long> RangeLong(long count)
        {
            return RangeLong(0, count);
        }

        /// <summary>
        /// Generates a sequence of integral numbers within a specified range.
        /// </summary>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="count">The number of sequential longs to generate.</param>
        /// <returns>The generated sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count" /> is smaller than 0.
        /// </exception>
        public static IEnumerable<long> RangeLong(long start, long count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            for (long i = 0; i < count; i++)
            {
                yield return start + i;
            }
        }

        #endregion Methods
    }
}