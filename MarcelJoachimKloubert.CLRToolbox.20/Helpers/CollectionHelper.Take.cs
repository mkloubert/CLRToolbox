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

        // Public Methods (2) 

        /// <summary>
        /// Takes a specific number of elements from a sequence.
        /// </summary>
        /// <param name="seq">The sequence from where to take the elements from.</param>
        /// <param name="count">The (maximum) number of elements to take.</param>
        /// <returns>The token elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<object> Take(IEnumerable seq, long count)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IEnumerator e = seq.GetEnumerator();
            try
            {
                while ((count > 0) && e.MoveNext())
                {
                    --count;
                    yield return e.Current;
                }
            }
            finally
            {
                IDisposable disp = e as IDisposable;
                if (disp != null)
                {
                    disp.Dispose();
                }
            }
        }

        /// <summary>
        /// Takes a specific number of elements from a sequence.
        /// </summary>
        /// <param name="seq">The sequence from where to take the elements from.</param>
        /// <param name="count">The (maximum) number of elements to take.</param>
        /// <returns>The token elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> Take<T>(IEnumerable<T> seq, long count)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            using (IEnumerator<T> e = seq.GetEnumerator())
            {
                while ((count > 0) && e.MoveNext())
                {
                    --count;
                    yield return e.Current;
                }
            }
        }

        #endregion Methods
    }
}
