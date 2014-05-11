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
        /// Concats squences.
        /// </summary>
        /// <typeparam name="T">Type of the </typeparam>
        /// <param name="seq">The original sequence.</param>
        /// <param name="sequencesToConcat">
        /// The list of sequences to concat.
        /// <see langword="null" /> items will be ignored.
        /// </param>
        /// <returns>The concated sequences.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="sequencesToConcat" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> Concat<T>(IEnumerable<T> seq, params IEnumerable<T>[] sequencesToConcat)
        {
            return Concat<T>(seq,
                             (IEnumerable<IEnumerable<T>>)sequencesToConcat);
        }

        /// <summary>
        /// Concats squences.
        /// </summary>
        /// <typeparam name="T">Type of the </typeparam>
        /// <param name="seq">The original sequence.</param>
        /// <param name="sequencesToConcat">
        /// The list of sequences to concat.
        /// <see langword="null" /> items will be ignored.
        /// </param>
        /// <returns>The concated sequences.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="sequencesToConcat" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> Concat<T>(IEnumerable<T> seq, IEnumerable<IEnumerable<T>> sequencesToConcat)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (sequencesToConcat == null)
            {
                throw new ArgumentNullException("sequencesToConcat");
            }

            // original sequence
            using (IEnumerator<T> e = seq.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    yield return e.Current;
                }
            }

            // additional sequences
            using (IEnumerator<IEnumerable<T>> enSTC = sequencesToConcat.GetEnumerator())
            {
                while (enSTC.MoveNext())
                {
                    IEnumerable<T> seqToConcat = enSTC.Current;
                    if (seqToConcat == null)
                    {
                        // ignore
                        continue;
                    }

                    using (IEnumerator<T> e = seqToConcat.GetEnumerator())
                    {
                        yield return e.Current;
                    }
                }
            }
        }

        #endregion Methods
    }
}