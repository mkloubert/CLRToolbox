// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Converts a sequence to an array.
        /// </summary>
        /// <typeparam name="T">Type of the items in sequence.</typeparam>
        /// <param name="seq">The sequence to convert / cast.</param>
        /// <returns><paramref name="seq" /> as array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="seq" /> is <see langword="null" />.</exception>
        public static T[] ToArray<T>(IEnumerable<T> seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            List<T> list = seq as List<T>;
            if (list != null)
            {
                return list.ToArray();
            }

            return new TMArrayBuffer<T>(seq).ToArray();
        }

        #endregion Methods
    }
}