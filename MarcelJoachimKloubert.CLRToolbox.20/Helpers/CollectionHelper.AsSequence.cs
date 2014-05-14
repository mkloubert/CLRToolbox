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
        /// Returns a general item as a sequence of a specific type.
        /// If <paramref name="seq" /> already has the result type of that method it is simply casted.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <returns>
        /// The sequence with the converted items or <see langword="null" /> if <paramref name="seq" />
        /// is also a <see langword="null" /> rreference.
        /// </returns>
        public static IEnumerable<T> AsSequence<T>(IEnumerable seq)
        {
            if (seq == null)
            {
                return null;
            }

            return Cast<T>(seq);
        }

        #endregion Methods
    }
}