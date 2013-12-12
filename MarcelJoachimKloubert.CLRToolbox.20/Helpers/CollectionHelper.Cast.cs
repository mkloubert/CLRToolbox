// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// Casts the items of a general sequence to a specific target type.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <returns>The sequence with the converted items.</returns>
        /// <remarks>
        /// If <paramref name="seq" /> is already the return type, it is simply casted.
        /// </remarks>
        public static IEnumerable<T> Cast<T>(IEnumerable seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IEnumerable<T> typedSeq = seq as IEnumerable<T>;
            if (typedSeq != null)
            {
                return typedSeq;
            }

            return CastInner<T>(seq);
        }
        // Private Methods (1) 

        private static IEnumerable<T> CastInner<T>(IEnumerable seq)
        {
            foreach (T item in seq)
            {
                yield return item;
            }
        }

        #endregion Methods
    }
}
