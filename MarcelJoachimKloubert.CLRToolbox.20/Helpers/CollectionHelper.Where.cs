// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Filters items from a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="predicate">The filter logic.</param>
        /// <returns>The filtered items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="predicate" />
        /// are <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> Where<T>(IEnumerable<T> seq, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return Where<T>(seq,
                            delegate(T item, long index)
                            {
                                return predicate(item);
                            });
        }

        /// <summary>
        /// Filters items from a sequence.
        /// </summary>
        /// <param name="seq">The sequence.</param>
        /// <param name="predicate">The filter logic.</param>
        /// <returns>The filtered items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="predicate" />
        /// are <see langword="null" />.
        /// </exception>
        public static IEnumerable Where(IEnumerable seq, Func<object, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }


            return Where(seq,
                         delegate(object item, long index)
                         {
                             return predicate(item);
                         });
        }

        /// <summary>
        /// Filters items from a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="predicate">The filter logic.</param>
        /// <returns>The filtered items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="predicate" />
        /// are <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> Where<T>(IEnumerable<T> seq, Func<T, long, bool> predicate)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            long index = 0;
            foreach (T item in seq)
            {
                if (predicate(item, index++))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Filters items from a sequence.
        /// </summary>
        /// <param name="seq">The sequence.</param>
        /// <param name="predicate">The filter logic.</param>
        /// <returns>The filtered items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="predicate" />
        /// are <see langword="null" />.
        /// </exception>
        public static IEnumerable Where(IEnumerable seq, Func<object, long, bool> predicate)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            long index = 0;
            foreach (object item in seq)
            {
                if (predicate(item, index++))
                {
                    yield return item;
                }
            }
        }

        #endregion Methods
    }
}
