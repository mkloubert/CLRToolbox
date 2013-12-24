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
        /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <returns>Both sequences contain the same data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left" /> and/or <paramref name="right" /> are <see langword="null" />.
        /// </exception>
        public static bool SequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            return SequenceEqual<T>(left, right, null);
        }

        /// <summary>
        /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <param name="comparer">
        /// The optional comparer to use. This value can be <see langword="null" /> to use a default comparer.
        /// </param>
        /// <returns>Both sequences contain the same data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left" /> and/or <paramref name="right" /> are <see langword="null" />.
        /// </exception>
        public static bool SequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> comparer)
        {
            if (left == null)
            {
                throw new ArgumentNullException("left");
            }

            if (right == null)
            {
                throw new ArgumentNullException("right");
            }

            Func<T, T, bool> equalsFunc;
            if (comparer == null)
            {
                equalsFunc = delegate(T x, T y)
                    {
                        return object.Equals(x, y);
                    };
            }
            else
            {
                equalsFunc = comparer.Equals;
            }

            bool checkCount = true;

            // first try to check size by using
            // generic ICollection<T> interface
            if (checkCount)
            {
                ICollection<T> collLeft = left as ICollection<T>;
                if (collLeft != null)
                {
                    ICollection<T> collRight = right as ICollection<T>;
                    if (collRight != null)
                    {
                        checkCount = false;

                        if (collLeft.Count != collRight.Count)
                        {
                            // unique sizes
                            return false;
                        }
                    }
                }
            }

            // now try to check size by using general
            // ICollection interface
            if (checkCount)
            {
                ICollection collLeft = left as ICollection;
                if (collLeft != null)
                {
                    ICollection collRight = right as ICollection;
                    if (collRight != null)
                    {
                        checkCount = false;

                        if (collLeft.Count != collRight.Count)
                        {
                            // unique sizes
                            return false;
                        }
                    }
                }
            }

            using (IEnumerator<T> enumeratorLeft = left.GetEnumerator())
            {
                using (IEnumerator<T> enumeratorRight = right.GetEnumerator())
                {
                    while (enumeratorLeft.MoveNext())
                    {
                        if (!enumeratorRight.MoveNext() || !equalsFunc(enumeratorLeft.Current, enumeratorRight.Current))
                        {
                            // left has more elements than right
                            // OR current items are different

                            return false;
                        }
                    }

                    if (enumeratorRight.MoveNext())
                    {
                        // right sequence has more items than left one
                        return false;
                    }
                }
            }

            // are the same
            return true;
        }

        #endregion Methods
    }
}
