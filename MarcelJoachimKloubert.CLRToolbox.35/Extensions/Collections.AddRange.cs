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
        /// <see cref="CollectionHelper.AddRange{T}(ICollection{T}, IEnumerable{T})" />
        public static void AddRange<T>(this ICollection<T> coll, IEnumerable<T> seq)
        {
            CollectionHelper.AddRange<T>(coll, seq);
        }

        #endregion Methods
    }
}
