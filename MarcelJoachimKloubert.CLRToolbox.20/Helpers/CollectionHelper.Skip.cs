// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Skips a specific number of elements in a sequence.
        /// </summary>
        /// <param name="seq">The source sequence.</param>
        /// <param name="count">The number of elements to skip.</param>
        /// <returns>The target sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is a <see langword="null" /> reference.
        /// </exception>
        public static IEnumerable<T> Skip<T>(IEnumerable<T> seq, long count)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            using (IEnumerator<T> e = seq.GetEnumerator())
            {
                // skip elements
                while (count > 0 && e.MoveNext())
                {
                    --count;
                }

                // take the rest
                if (count <= 0)
                {
                    while (e.MoveNext())
                    {
                        yield return e.Current;
                    }
                }
            }
        }

        #endregion Methods
    }
}