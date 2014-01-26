// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
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
        /// <see cref="CollectionHelper.Where(IEnumerable, Func{object, bool})" />
        public static IEnumerable Where(this IEnumerable seq, Func<object, bool> predicate)
        {
            return CollectionHelper.Where(seq, predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.Where(IEnumerable, Func{object, long, bool})" />
        public static IEnumerable Where(this IEnumerable seq, Func<object, long, bool> predicate)
        {
            return CollectionHelper.Where(seq, predicate);
        }

        #endregion Methods
    }
}
