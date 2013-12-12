// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.Sort{T, C}(IList{T}, Func{T, C})" />
        public static void Sort<T, C>(this IList<T> list,
                                      Func<T, C> selector)
            where C : global::System.IComparable<C>
        {
            CollectionHelper.Sort<T, C>(list, selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.Sort{T}(IList{T}, Func{T, IComparable})" />
        public static void Sort<T>(this IList<T> list,
                                   Func<T, IComparable> selector)
        {
            CollectionHelper.Sort<T>(list, selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.Sort{T, C}(IList{T}, Func{T, C}, bool)" />
        public static void Sort<T, C>(this IList<T> list,
                                      Func<T, C> selector,
                                      bool descending)
            where C : global::System.IComparable<C>
        {
            CollectionHelper.Sort<T, C>(list, selector, descending);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.Sort{T}(IList{T}, Func{T, IComparable}, bool)" />
        public static void Sort<T>(this IList<T> list,
                                   Func<T, IComparable> selector,
                                   bool descending)
        {
            CollectionHelper.Sort<T>(list, selector, descending);
        }

        #endregion Methods
    }
}
