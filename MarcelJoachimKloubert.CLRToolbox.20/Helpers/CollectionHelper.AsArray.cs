// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de



using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Converts or casts a sequence to an array.
        /// </summary>
        /// <typeparam name="T">Type of the items in sequence.</typeparam>
        /// <param name="seq">The sequence to convert / cast.</param>
        /// <returns><paramref name="seq" /> as array.</returns>
        /// <remarks>
        /// If <paramref name="seq" /> is <see langword="null" /> the result will also be <see langword="null" />.
        /// If <paramref name="seq" /> is already an array, it is simply casted.
        /// </remarks>
        public static T[] AsArray<T>(IEnumerable<T> seq)
        {
            if (seq == null)
            {
                return null;
            }

            if (seq is T[])
            {
                return (T[])seq;
            }

            return ToArray<T>(seq);
        }

        #endregion Methods
    }
}
