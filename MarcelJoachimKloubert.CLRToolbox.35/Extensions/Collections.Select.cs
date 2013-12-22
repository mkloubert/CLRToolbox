// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
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
        /// <see cref="CollectionHelper.Select{TSrc, TDest}(IEnumerable{TSrc}, Func{TSrc, long, TDest})" />
        public static IEnumerable<TDest> Select<TSrc, TDest>(this IEnumerable<TSrc> seq, Func<TSrc, long, TDest> selector)
        {
            return CollectionHelper.Select<TSrc, TDest>(seq, selector);
        }

        #endregion Methods
    }
}
