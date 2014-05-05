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
        ///
        /// </summary>
        /// <see cref="List{T}.AddRange(IEnumerable{T})" />
        public static void AddRange<T>(ICollection<T> coll, IEnumerable<T> items)
        {
            if (coll == null)
            {
                throw new ArgumentNullException("coll");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            List<T> list = items as List<T>;
            if (list != null)
            {
                list.AddRange(items);
            }
            else
            {
                foreach (T i in items)
                {
                    coll.Add(i);
                }
            }
        }

        #endregion Methods
    }
}