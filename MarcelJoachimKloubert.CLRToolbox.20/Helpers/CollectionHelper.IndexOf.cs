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
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IList list = seq as IList;
            if (list != null)
            {
                // use build-in method
                return list.IndexOf(item);
            }

            long index = -1;
            foreach (object i in seq)
            {
                ++index;

                if (object.Equals(i, item))
                {
                    return index;
                }
            }

            return -1;
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
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IList<T> list = seq as IList<T>;
            if (list != null)
            {
                // use build-in method
                return list.IndexOf(item);
            }

            return IndexOf((IEnumerable)seq, item);
        }

        #endregion Methods
    }
}