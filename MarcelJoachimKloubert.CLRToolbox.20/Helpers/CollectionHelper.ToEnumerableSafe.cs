// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Returns a non null sequence.
        /// </summary>
        /// <param name="seq">The input sequence.</param>
        /// <returns>The/A non null sequence.</returns>
        /// <remarks>
        /// If <paramref name="seq" /> is NOT <see langword="null" />
        /// the instance of <paramref name="seq" /> itself is returned.
        /// </remarks>
        public static IEnumerable ToEnumerableSafe(IEnumerable seq)
        {
            return ToEnumerableSafe(seq, false);
        }

        /// <summary>
        /// Returns a non null sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <returns>The/A non null sequence.</returns>
        /// <remarks>
        /// If <paramref name="seq" /> is NOT <see langword="null" />
        /// the instance of <paramref name="seq" /> itself is returned.
        /// </remarks>
        public static IEnumerable<T> ToEnumerableSafe<T>(IEnumerable<T> seq)
        {
            return ToEnumerableSafe<T>(seq, false);
        }

        /// <summary>
        /// Returns a non null sequence.
        /// </summary>
        /// <param name="seq">The input sequence.</param>
        /// <param name="ofType">
        /// Additionally invoke <see cref="CollectionHelper.OfType{T}(IEnumerable)" /> method on result sequence or not.
        /// </param>
        /// <returns>The/A non null sequence.</returns>
        /// <remarks>
        /// If <paramref name="seq" /> is NOT <see langword="null" /> and <paramref name="ofType" />
        /// is <see langword="false" /> the instance of <paramref name="seq" /> is returned.
        /// </remarks>
        public static IEnumerable ToEnumerableSafe(IEnumerable seq, bool ofType)
        {
            IEnumerable result = seq ?? Empty<object>();

            return ofType ? OfType<object>(result) : result;
        }

        /// <summary>
        /// Returns a non null sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The input sequence.</param>
        /// <param name="ofType">
        /// Additionally invoke <see cref="CollectionHelper.OfType{T}(IEnumerable)" /> method on result sequence or not.
        /// </param>
        /// <returns>The/A non null sequence.</returns>
        /// <remarks>
        /// If <paramref name="seq" /> is NOT <see langword="null" /> and <paramref name="ofType" />
        /// is <see langword="false" /> the instance of <paramref name="seq" /> is returned.
        /// </remarks>
        public static IEnumerable<T> ToEnumerableSafe<T>(IEnumerable<T> seq, bool ofType)
        {
            IEnumerable<T> result = seq ?? Empty<T>();

            return ofType ? OfType<T>(result) : result;
        }

        #endregion Methods
    }
}
