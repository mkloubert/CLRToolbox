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
        /// Tries to return the one and only element of a sequence by using a predicate for filtering out elements.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="predicate">The element filter / predicate.</param>
        /// <returns>The only element or the default value of the type if no element exists in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and / or <paramref name="predicate" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="seq" /> contains more than one element.
        /// </exception>
        public static T SingleOrDefault<T>(IEnumerable<T> seq, Func<T, bool> predicate)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            T result = default(T);

            using (IEnumerator<T> enumerator = seq.GetEnumerator())
            {
                byte elementCount = 0;

                while (enumerator.MoveNext())
                {
                    T current = enumerator.Current;
                    if (!predicate(current))
                    {
                        continue;
                    }

                    if (++elementCount > 1)
                    {
                        throw new InvalidOperationException(_ERR_MSG_SINGLE_MORE_THAN_ONE_ELEMENT);
                    }

                    result = current;
                }
            }

            return result;
        }

        #endregion Methods
    }
}
