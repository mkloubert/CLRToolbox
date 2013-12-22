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
        /// <see cref="CollectionHelper.Randomize{T}(IEnumerable{T})" />
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> seq)
        {
            return CollectionHelper.Randomize<T>(seq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.Randomize{T}(IEnumerable{T}, Random)" />
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> seq, Random rand)
        {
            return CollectionHelper.Randomize<T>(seq, rand);
        }

        #endregion Methods
    }
}
