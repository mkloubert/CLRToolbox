// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
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
        /// <see cref="CollectionHelper.ToEnumerableSafe(IEnumerable, bool)" />
        public static IEnumerable ToEnumerableSafe(this IEnumerable seq)
        {
            return CollectionHelper.ToEnumerableSafe(seq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.ToEnumerableSafe{T}(IEnumerable{T})" />
        public static IEnumerable<T> ToEnumerableSafe<T>(this IEnumerable<T> seq)
        {
            return CollectionHelper.ToEnumerableSafe<T>(seq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.ToEnumerableSafe(IEnumerable, bool)" />
        public static IEnumerable ToEnumerableSafe(this IEnumerable seq, bool ofType)
        {
            return CollectionHelper.ToEnumerableSafe(seq, ofType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.ToEnumerableSafe{T}(IEnumerable{T}, bool)" />
        public static IEnumerable<T> ToEnumerableSafe<T>(this IEnumerable<T> seq, bool ofType)
        {
            return CollectionHelper.ToEnumerableSafe<T>(seq, ofType);
        }

        #endregion Methods
    }
}
