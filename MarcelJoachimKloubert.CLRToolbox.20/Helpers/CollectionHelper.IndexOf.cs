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
        /// <typeparam name="T">Type of the items of <paramref name="seq" />.</typeparam>
        /// <param name="seq">The sequence where to search the <paramref name="item" /> in.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The zero based index or -1 if not found.</returns>
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

            IList list2 = seq as IList;
            if (list2 != null)
            {
                // use build-in method
                return list2.IndexOf(item);
            }

            return -1;
        }

        #endregion Methods
    }
}