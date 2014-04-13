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

            long? countLeft = TryGetCountFromProperty<T>(left);
            long? countRight = TryGetCountFromProperty<T>(right);
            if (countLeft.HasValue &&
                countRight.HasValue)
            {
                if (countLeft.Value != countRight.Value)
                {
                    return false;
                }
            }

            using (IEnumerator<T> enumeratorLeft = left.GetEnumerator())
            {
                using (IEnumerator<T> enumeratorRight = right.GetEnumerator())
                {
                    while (enumeratorLeft.MoveNext())
                    {
                        if ((false == enumeratorRight.MoveNext()) ||
                            (false == equalsFunc(enumeratorLeft.Current, enumeratorRight.Current)))
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
