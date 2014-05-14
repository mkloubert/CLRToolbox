// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
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

            List<T> list = coll as List<T>;
            if (list != null)
            {
                // use build in method
                list.AddRange(items);
            }
            else
            {
                ForEach(items,
                        delegate(IForEachItemExecutionContext<T, ICollection<T>> ctx)
                        {
                            ICollection<T> c = ctx.State;

                            c.Add(ctx.Item);
                        }, coll);
            }
        }

        #endregion Methods
    }
}