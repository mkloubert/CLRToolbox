// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Determines whether all elements of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="func">The condition.</param>
        /// <returns>All element of a sequence satisfies the condition of <paramref name="func" /> or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static bool All<T>(IEnumerable<T> seq, Func<T, long, bool> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return All<T, object>(seq,
                                  delegate(T item, long index, object state)
                                  {
                                      return true;
                                  }, (object)null);
        }

        /// <summary>
        /// Determines whether all elements of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <typeparam name="S">Type of the state object for <paramref name="func" />.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="func">The condition.</param>
        /// <param name="funcState">The state object for <paramref name="func" />.</param>
        /// <returns>All element of a sequence satisfies the condition of <paramref name="func" /> or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static bool All<T, S>(IEnumerable<T> seq, Func<T, long, S, bool> func, S funcState)
        {
            return All<T, S>(seq,
                             func,
                             delegate(T item, long index)
                             {
                                 return funcState;
                             });
        }

        /// <summary>
        /// Determines whether all elements of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <typeparam name="S">Type of the state object for <paramref name="func" />.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="func">The condition.</param>
        /// <param name="funcFactory">The function that provides the state object for <paramref name="func" />.</param>
        /// <returns>All element of a sequence satisfies the condition of <paramref name="func" /> or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static bool All<T, S>(IEnumerable<T> seq, Func<T, long, S, bool> func, Func<T, long, S> funcFactory)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcFactory == null)
            {
                throw new ArgumentNullException("funcFactory");
            }

            using (IEnumerator<T> e = seq.GetEnumerator())
            {
                long index = -1;

                while (e.MoveNext())
                {
                    ++index;
                    T item = e.Current;

                    if (func(item,
                             index,
                             funcFactory(item, index)) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion Methods
    }
}