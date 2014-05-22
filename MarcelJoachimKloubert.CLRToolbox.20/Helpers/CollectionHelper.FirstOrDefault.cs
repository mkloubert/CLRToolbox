// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Tries to return the first element of a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <returns>The first element or the default value of the type.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static T FirstOrDefault<T>(IEnumerable<T> seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IList<T> list = seq as IList<T>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            else
            {
                return FirstOrDefault<T>(seq,
                                         delegate(T item)
                                         {
                                             return true;
                                         });
            }

            return default(T);
        }

        /// <summary>
        /// Tries to return the first element of a sequence by using a predicate.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="predicate">The logic to find the matching element.</param>
        /// <returns>The first element or the default value of the type.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="predicate" /> are <see langword="null" />.
        /// </exception>
        public static T FirstOrDefault<T>(IEnumerable<T> seq, Func<T, bool> predicate)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            using (IEnumerator<T> enumerator = seq.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    T item = enumerator.Current;

                    if (predicate(item))
                    {
                        return item;
                    }
                }
            }

            return default(T);
        }

        #endregion Methods
    }
}