// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.AsArray{T}(IEnumerable{T})" />
        public static T[] AsArray<T>(this IEnumerable<T> seq)
        {
            return CollectionHelper.AsArray<T>(seq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.AsArray(IEnumerable)" />
        public static object[] AsArray(this IEnumerable seq)
        {
            return CollectionHelper.AsArray(seq);
        }

        #endregion Methods
    }
}
