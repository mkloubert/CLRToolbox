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
        /// <see cref="CollectionHelper.IsEmpty{T}(IEnumerable{T})" />
        public static bool IsEmpty<T>(this IEnumerable<T> seq)
        {
            return CollectionHelper.IsEmpty<T>(seq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.IsEmpty(IEnumerable)" />
        public static bool IsEmpty(this IEnumerable seq)
        {
            return CollectionHelper.IsEmpty(seq);
        }

        #endregion Methods
    }
}
