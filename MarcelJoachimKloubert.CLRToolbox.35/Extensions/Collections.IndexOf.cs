// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.IndexOf(IEnumerable, object)" />
        public static long IndexOf(this IEnumerable seq, object item)
        {
            return CollectionHelper.IndexOf(seq, item);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.IndexOf{T}(IEnumerable{T}, T)" />
        public static long IndexOf<T>(this IEnumerable<T> seq, T item)
        {
            return CollectionHelper.IndexOf<T>(seq, item);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.IndexOf(IEnumerable, object, IEqualityComparer)" />
        public static long IndexOf(this IEnumerable seq, object item, IEqualityComparer comparer)
        {
            return CollectionHelper.IndexOf(seq, item, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.IndexOf{T}(IEnumerable{T}, T, IEqualityComparer{T})" />
        public static long IndexOf<T>(this IEnumerable<T> seq, T item, IEqualityComparer<T> comparer)
        {
            return CollectionHelper.IndexOf<T>(seq, item, comparer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.IndexOf(IEnumerable, object, Func{object, object, long, bool})" />
        public static long IndexOf(this IEnumerable seq, object item, Func<object, object, long, bool> finder)
        {
            return CollectionHelper.IndexOf(seq, item, finder);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.IndexOf{T}(IEnumerable{T}, T, Func{T, T, long, bool})" />
        public static long IndexOf<T>(this IEnumerable<T> seq, T item, Func<T, T, long, bool> finder)
        {
            return CollectionHelper.IndexOf<T>(seq, item, finder);
        }

        #endregion Methods
    }
}