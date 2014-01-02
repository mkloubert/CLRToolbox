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
        /// <see cref="CollectionHelper.IsNotEmpty{T}(IEnumerable{T})" />
        public static bool IsNotEmpty<T>(this IEnumerable<T> seq)
        {
            return CollectionHelper.IsNotEmpty<T>(seq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.IsNotEmpty(IEnumerable)" />
        public static bool IsNotEmpty(this IEnumerable seq)
        {
            return CollectionHelper.IsNotEmpty(seq);
        }

        #endregion Methods
    }
}
