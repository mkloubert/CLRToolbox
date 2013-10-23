// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.AsArray{T}(IEnumerable{T})" />
        public static T[] AsArray<T>(this IEnumerable<T> seq)
        {
            return CollectionHelper.AsArray<T>(seq);
        }

        #endregion Methods
    }
}
