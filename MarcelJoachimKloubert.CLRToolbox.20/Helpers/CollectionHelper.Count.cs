// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Counts the elements of a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <returns>The number of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static long Count<T>(IEnumerable<T> seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            ICollection<T> coll = seq as ICollection<T>;
            if (coll != null)
            {
                return coll.Count;
            }

            long result = 0;
            using (IEnumerator<T> enumerator = seq.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ++result;
                }
            }

            return result;
        }

        /// <summary>
        /// Counts the elements of a sequence.
        /// </summary>
        /// <param name="seq">The sequence.</param>
        /// <returns>The number of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static long Count(IEnumerable seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            ICollection coll = seq as ICollection;
            if (coll != null)
            {
                return coll.Count;
            }

            long result = 0;
            foreach (object item in seq)
            {
                ++result;
            }

            return result;
        }

        #endregion Methods
    }
}
