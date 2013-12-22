// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Swaps the order of elements in a list.
        /// </summary>
        /// <typeparam name="T">Type of the elements of <paramref name="list" />.</typeparam>
        /// <param name="list">The list that should be shuffled.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="list" /> is read only.
        /// </exception>
        public static void Shuffle<T>(IList<T> list)
        {
            Shuffle<T>(list, new Random());
        }

        /// <summary>
        /// Swaps the order of elements in a list.
        /// </summary>
        /// <typeparam name="T">Type of the elements of <paramref name="list" />.</typeparam>
        /// <param name="list">The list that should be shuffled.</param>
        /// <param name="rand">The random number generator to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> and/or <paramref name="rand" /> are <see langword="null" />.
        /// </exception>
        public static void Shuffle<T>(IList<T> list, Random rand)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (rand == null)
            {
                throw new ArgumentNullException("rand");
            }

            for (int i = 0; i < list.Count; i++)
            {
                int newIdx = rand.Next(0, list.Count);
                T temp = list[i];

                list[i] = list[newIdx];
                list[newIdx] = temp;
            }
        }

        #endregion Methods
    }
}
