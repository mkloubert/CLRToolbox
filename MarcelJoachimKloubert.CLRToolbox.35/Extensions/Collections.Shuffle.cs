// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
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
        /// <see cref="CollectionHelper.Shuffle{T}(IList{T})" />
        public static void Shuffle<T>(this IList<T> list)
        {
            CollectionHelper.Shuffle<T>(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.Shuffle{T}(IList{T}, Random)" />
        public static void Shuffle<T>(this IList<T> list, Random rand)
        {
            CollectionHelper.Shuffle<T>(list, rand);
        }

        #endregion Methods
    }
}
