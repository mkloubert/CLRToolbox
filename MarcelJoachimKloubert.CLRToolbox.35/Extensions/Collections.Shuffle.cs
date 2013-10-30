// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
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
        public static void Shuffle<T>(this IList<T> list)
        {
            Shuffle<T>(list, new Random());
        }

        /// <summary>
        /// Swaps the order of elements in a list.
        /// </summary>
        /// <typeparam name="T">Type of the elements of <paramref name="list" />.</typeparam>
        /// <param name="list">The list that should be shuffled.</param>
        /// <param name="r">The random number generator to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> and/or <paramref name="r" /> are <see langword="null" />.
        /// </exception>
        public static void Shuffle<T>(this IList<T> list, Random r)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (r == null)
            {
                throw new ArgumentNullException("r");
            }

            for (var i = 0; i < list.Count; i++)
            {
                var ni = r.Next(0, list.Count);
                var temp = list[i];

                list[i] = list[ni];
                list[ni] = temp;
            }
        }

        #endregion Methods
    }
}
