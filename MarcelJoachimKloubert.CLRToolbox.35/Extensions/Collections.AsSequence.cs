// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
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
        /// <see cref="CollectionHelper.AsSequence{T}(IEnumerable)" />
        public static IEnumerable<T> AsSequence<T>(this IEnumerable seq)
        {
            return CollectionHelper.AsSequence<T>(seq);
        }

        #endregion Methods
    }
}
