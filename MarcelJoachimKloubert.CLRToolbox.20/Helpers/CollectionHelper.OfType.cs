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

        // Public Methods (1) 

        /// <summary>
        /// Filters the items from a sequence of a specific result type.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <returns>The sequence with the filtered items.</returns>
        public static IEnumerable<T> OfType<T>(IEnumerable seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IGeneralList genList = seq as IGeneralList;
            if (genList != null)
            {
                // use build-in
                return genList.OfType<T>();
            }

            return OfTypeInner<T>(seq);
        }

        // Private Methods (1) 

        private static IEnumerable<T> OfTypeInner<T>(IEnumerable seq)
        {
            foreach (object item in seq)
            {
                if (item is T)
                {
                    yield return (T)item;
                }
            }
        }

        #endregion Methods
    }
}