// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
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
        public static IEnumerable<T> Cast<T>(IEnumerable seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            foreach (object item in seq)
            {
                yield return (T)item;
            }
        }

        #endregion Methods
    }
}
