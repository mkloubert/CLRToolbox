// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.IndexOf{T}(IEnumerable{T}, T)" />
        public static long Index<T>(this IEnumerable<T> seq, T item)
        {
            return CollectionHelper.IndexOf<T>(seq, item);
        }

        #endregion Methods
    }
}