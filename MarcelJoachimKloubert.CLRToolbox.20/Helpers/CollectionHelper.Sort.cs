// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (6)

        // Public Methods (4) 

        /// <summary>
        /// Sorts a list by using a delegate with custom comparable value.
        /// </summary>
        /// <typeparam name="T">Type of the items to compare.</typeparam>
        /// <typeparam name="C">Type of the comparable values.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="selector">The selector that returns the comparable value for an item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> and/or <paramref name="selector" /> are <see langword="null" />.
        /// </exception>
        public static void Sort<T, C>(IList<T> list,
                                      Func<T, C> selector)
            where C : global::System.IComparable<C>
        {
            Sort<T, C>(list, selector, false);
        }

        /// <summary>
        /// Sorts a list by using a delegate with custom comparable value.
        /// </summary>
        /// <typeparam name="T">Type of the items to compare.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="selector">The selector that returns the comparable value for an item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> and/or <paramref name="selector" /> are <see langword="null" />.
        /// </exception>
        public static void Sort<T>(IList<T> list,
                                   Func<T, IComparable> selector)
        {
            Sort<T>(list, selector, false);
        }

        /// <summary>
        /// Sorts a list by using a delegate with custom comparable value.
        /// </summary>
        /// <typeparam name="T">Type of the items to compare.</typeparam>
        /// <typeparam name="C">Type of the comparable values.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="selector">The selector that returns the comparable value for an item.</param>
        /// <param name="descending">Sort descending or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> and/or <paramref name="selector" /> are <see langword="null" />.
        /// </exception>
        public static void Sort<T, C>(IList<T> list,
                                      Func<T, C> selector,
                                      bool descending)
            where C : global::System.IComparable<C>
        {
            SortInner<T, C>(list,
                            selector,
                            delegate(C x, C y)
                            {
                                return x.CompareTo(y);
                            },
                            descending);
        }

        /// <summary>
        /// Sorts a list by using a delegate with custom comparable value.
        /// </summary>
        /// <typeparam name="T">Type of the items to compare.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="selector">The selector that returns the comparable value for an item.</param>
        /// <param name="descending">Sort descending or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> and/or <paramref name="selector" /> are <see langword="null" />.
        /// </exception>
        public static void Sort<T>(IList<T> list,
                                   Func<T, IComparable> selector,
                                   bool descending)
        {
            SortInner<T, IComparable>(list,
                                      selector,
                                      delegate(IComparable x, IComparable y)
                                      {
                                          return x.CompareTo(y);
                                      },
                                      descending);
        }
        // Private Methods (2) 

        private static int CalculateComparerValue<C>(C x, C y, Func<C, C, int> func, bool descending)
        {
            int multiplicator = descending ? -1 : 1;

            unchecked
            {
                if (x != null)
                {
                    // use x to compare with y
                    return func(x, y) * multiplicator;
                }
                else if (y != null)
                {
                    // use y to compare with x from the other side
                    return func(y, x) * multiplicator * -1;
                }
            }

            // both are (null), so they are equal
            return 0;
        }

        private static void SortInner<T, C>(IList<T> list, Func<T, C> selector, Func<C, C, int> func, bool descending)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            for (int i = 1; i <= list.Count; i++)
            {
                for (int j = 0; j < list.Count - i; j++)
                {
                    int compValue = CalculateComparerValue(selector(list[j]),  // x
                                                           selector(list[j + 1]),  // y
                                                           func,
                                                           descending);

                    if (compValue > 0)
                    {
                        // x > y
                        T temp = list[j];

                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }

        #endregion Methods
    }
}
