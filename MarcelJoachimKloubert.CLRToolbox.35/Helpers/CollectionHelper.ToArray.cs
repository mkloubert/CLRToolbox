// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Enumerable.ToArray{T}(IEnumerable{T})" />
        public static T[] ToArray<T>(IEnumerable<T> seq)
        {
            return Enumerable.ToArray<T>(seq);
        }

        #endregion Methods
    }
}
