// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Casts the items of a general sequence to a specific target type.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <returns>The sequence with the converted items.</returns>
        /// <remarks>The is an upgrade to .NET 2.0 version of that method.</remarks>
        public static IEnumerable<T> Cast<T>(IEnumerable seq)
        {
            return global::System.Linq.Enumerable.Cast<T>(seq);
        }

        #endregion Methods
    }
}
