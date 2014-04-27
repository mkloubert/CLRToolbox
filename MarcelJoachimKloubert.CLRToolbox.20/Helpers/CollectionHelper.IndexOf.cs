// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        /// Returns the first zero based item of an item inside a sequence.
        /// </summary>
        /// <param name="seq">The sequence where to search the <paramref name="item" /> in.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The zero based index or -1 if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static long IndexOf(IEnumerable seq, object item)
        {
            return IndexOf(seq, item,
                           delegate(object currentItem, object itemToSearch, long currentIndex)
                           {
                               return object.Equals(currentItem, itemToSearch);
                           });
        }

        /// <summary>
        /// Returns the first zero based item of an item inside a sequence.
        /// </summary>
        /// <param name="seq">The sequence where to search the <paramref name="item" /> in.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The zero based index or -1 if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static long IndexOf<T>(IEnumerable<T> seq, T item)
        {
            return IndexOf<T>(seq, item,
                              EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Returns the first zero based item of an item inside a sequence.
        /// </summary>
        /// <param name="seq">The sequence where to search the <paramref name="item" /> in.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">
        /// The comparer to use to compare the current item of <paramref name="seq" />
        /// with <paramref name="item" />.
        /// </param>
        /// <returns>The zero based index or -1 if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="comparer" /> are <see langword="null" />.
        /// </exception>
        public static long IndexOf(IEnumerable seq, object item, IEqualityComparer comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            return IndexOf(seq, item,
                           delegate(object currentItem, object itemToSearch, long currentIndex)
                           {
                               return comparer.Equals(currentItem, itemToSearch);
                           });
        }

        /// <summary>
        /// Returns the first zero based item of an item inside a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items of <paramref name="seq" />.</typeparam>
        /// <param name="seq">The sequence where to search the <paramref name="item" /> in.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">
        /// The comparer to use to compare the current item of <paramref name="seq" />
        /// with <paramref name="item" />.
        /// </param>
        /// <returns>The zero based index or -1 if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="comparer" /> are <see langword="null" />.
        /// </exception>
        public static long IndexOf<T>(IEnumerable<T> seq, T item, IEqualityComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            return IndexOf<T>(seq, item,
                              delegate(T currentItem, T itemToSearch, long currentIndex)
                              {
                                  return comparer.Equals(currentItem, itemToSearch);
                              });
        }

        /// <summary>
        /// Returns the first zero based item of an item inside a sequence.
        /// </summary>
        /// <param name="seq">The sequence where to search the <paramref name="item" /> in.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="finder">
        /// The function to use to compare the current item of <paramref name="seq" />
        /// with <paramref name="item" />.
        /// </param>
        /// <returns>The zero based index or -1 if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="finder" /> are <see langword="null" />.
        /// </exception>
        public static long IndexOf(IEnumerable seq, object item, Func<object, object, long, bool> finder)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IList genList = seq as IList;
            if (genList != null)
            {
                // use build-in method
                return genList.IndexOf(item);
            }

            long index = -1;
            foreach (object i in seq)
            {
                ++index;

                if (finder(i, item, index))
                {
                    // found
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns the first zero based item of an item inside a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items of <paramref name="seq" />.</typeparam>
        /// <param name="seq">The sequence where to search the <paramref name="item" /> in.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="finder">
        /// The function to use to compare the current item of <paramref name="seq" />
        /// with <paramref name="item" />.
        /// </param>
        /// <returns>The zero based index or -1 if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="finder" /> are <see langword="null" />.
        /// </exception>
        public static long IndexOf<T>(IEnumerable<T> seq, T item, Func<T, T, long, bool> finder)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            IList<T> list = seq as IList<T>;
            if (list != null)
            {
                // use build-in method
                return list.IndexOf(item);
            }

            return IndexOf((IEnumerable)seq, item,
                           delegate(object currentItem, object i, long currentIndex)
                           {
                               return finder((T)currentItem, (T)i,
                                             currentIndex);
                           });
        }

        #endregion Methods
    }
}