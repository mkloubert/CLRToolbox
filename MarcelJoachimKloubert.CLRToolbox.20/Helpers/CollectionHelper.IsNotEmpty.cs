// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Checks if a sequence is NOT empty.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence to check.</param>
        /// <returns>Sequence is empty (<see langword="false" />) or not (<see langword="true" />).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static bool IsNotEmpty<T>(IEnumerable<T> seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IGeneralList genList = seq as IGeneralList;
            if (seq != null)
            {
                return genList.IsNotEmpty;
            }

            return IsEmpty<T>(seq) == false;
        }

        /// <summary>
        /// Checks if a sequence is NOT empty.
        /// </summary>
        /// <param name="seq">The sequence to check.</param>
        /// <returns>Sequence is empty (<see langword="false" />) or not (<see langword="true" />).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static bool IsNotEmpty(IEnumerable seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IGeneralList genList = seq as IGeneralList;
            if (seq != null)
            {
                return genList.IsNotEmpty;
            }

            return IsEmpty(seq) == false;
        }

        #endregion Methods
    }
}
