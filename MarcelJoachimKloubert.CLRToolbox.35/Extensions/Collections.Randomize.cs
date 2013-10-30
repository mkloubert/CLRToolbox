// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Returns the elements of a sequence in random order.
        /// </summary>
        /// <typeparam name="T">Type of the elements of <paramref name="seq" />.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <returns>The random sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> seq)
        {
            return Randomize<T>(seq, new Random());
        }

        /// <summary>
        /// Returns the elements of a sequence in random order.
        /// </summary>
        /// <typeparam name="T">Type of the elements of <paramref name="seq" />.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <param name="r">The random number generator to use.</param>
        /// <returns>The random sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="r" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> seq, Random r)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (r == null)
            {
                throw new ArgumentNullException("r");
            }

            var list = seq.ToList();
            Shuffle<T>(list, r);

            foreach (var item in list)
            {
                yield return item;
            }
        }

        #endregion Methods
    }
}
