// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Collections;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Transforms the items of a sequence to another type.
        /// </summary>
        /// <typeparam name="TSrc">The source type of the items.</typeparam>
        /// <typeparam name="TDest">The target type.</typeparam>
        /// <param name="seq">The sequence to transform.</param>
        /// <param name="selector">The logic that transforms an item to the target type.</param>
        /// <returns>The transformed sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TDest> Select<TSrc, TDest>(IEnumerable<TSrc> seq, Func<TSrc, TDest> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            return Select<TSrc, TDest>(seq,
                                       delegate(TSrc item, long i)
                                       {
                                           return selector(item);
                                       });
        }

        /// <summary>
        /// Transforms the items of a sequence to another type.
        /// </summary>
        /// <typeparam name="TSrc">The source type of the items.</typeparam>
        /// <typeparam name="TDest">The target type.</typeparam>
        /// <param name="seq">The sequence to transform.</param>
        /// <param name="selector">
        /// The logic that transforms an item to the target type. The second parameter is current the zero based index.
        /// </param>
        /// <returns>The transformed sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TDest> Select<TSrc, TDest>(IEnumerable<TSrc> seq, Func<TSrc, long, TDest> selector)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            using (IEnumerator<TSrc> e = seq.GetEnumerator())
            {
                long i = -1;

                while (e.MoveNext())
                {
                    yield return selector(e.Current, ++i);
                }
            }
        }

        #endregion Methods
    }
}
