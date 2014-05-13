// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (7)

        // Public Methods (7) 

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TResult">Tyoe of the destination items.</typeparam>
        /// <param name="src">The source sequence.</param>
        /// <returns>The new flatten list of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TResult>(IEnumerable src)
        {
            return SelectMany<object, TResult>(Cast<object>(src),
                                               delegate(object s)
                                               {
                                                   return (IEnumerable<TResult>)s;
                                               });
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TSrc">Type of the source items.</typeparam>
        /// <typeparam name="TResult">Tyoe of the destination items.</typeparam>
        /// <param name="src">The source sequence.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The new flatten list of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="selector" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// At least one result of <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSrc, TResult>(IEnumerable<TSrc> src, Func<TSrc, IEnumerable<TResult>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            return SelectMany<TSrc, TResult>(src,
                                             delegate(TSrc s, long index)
                                             {
                                                 return selector(s);
                                             });
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TSrc">Type of the source items.</typeparam>
        /// <typeparam name="TResult">Tyoe of the destination items.</typeparam>
        /// <param name="src">The source sequence.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The new flatten list of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="selector" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// At least one result of <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSrc, TResult>(IEnumerable<TSrc> src, Func<TSrc, long, IEnumerable<TResult>> selector)
        {
            return SelectMany<TSrc, TResult, TResult>(src,
                                                      selector,
                                                      delegate(TSrc s, TResult c, long index)
                                                      {
                                                          return c;
                                                      });
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TSrc">Type of the source items.</typeparam>
        /// <typeparam name="TColl">The collection type.</typeparam>
        /// <typeparam name="TResult">Tyoe of the destination items.</typeparam>
        /// <param name="src">The source sequence.</param>
        /// <param name="collSelector">The selector for the temporary items.</param>
        /// <param name="resSelector">The selector for the real items.</param>
        /// <returns>The new flatten list of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" />, <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// At least one result of <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSrc, TColl, TResult>(IEnumerable<TSrc> src,
                                                                            Func<TSrc, IEnumerable<TColl>> collSelector,
                                                                            Func<TSrc, TColl, TResult> resSelector)
        {
            if (collSelector == null)
            {
                throw new ArgumentNullException("collSelector");
            }

            if (resSelector == null)
            {
                throw new ArgumentNullException("resSelector");
            }

            return SelectMany<TSrc, TColl, TResult>(src,
                                                    delegate(TSrc s, long index)
                                                    {
                                                        return collSelector(s);
                                                    },
                                                    delegate(TSrc s, TColl c, long index)
                                                    {
                                                        return resSelector(s, c);
                                                    });
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TSrc">Type of the source items.</typeparam>
        /// <typeparam name="TColl">The collection type.</typeparam>
        /// <typeparam name="TResult">Tyoe of the destination items.</typeparam>
        /// <param name="src">The source sequence.</param>
        /// <param name="collSelector">The selector for the temporary items.</param>
        /// <param name="resSelector">The selector for the real items.</param>
        /// <returns>The new flatten list of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" />, <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// At least one result of <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSrc, TColl, TResult>(IEnumerable<TSrc> src,
                                                                            Func<TSrc, long, IEnumerable<TColl>> collSelector,
                                                                            Func<TSrc, TColl, TResult> resSelector)
        {
            if (resSelector == null)
            {
                throw new ArgumentNullException("resSelector");
            }

            return SelectMany<TSrc, TColl, TResult>(src,
                                                    collSelector,
                                                    delegate(TSrc s, TColl c, long index)
                                                    {
                                                        return resSelector(s, c);
                                                    });
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TSrc">Type of the source items.</typeparam>
        /// <typeparam name="TColl">The collection type.</typeparam>
        /// <typeparam name="TResult">Tyoe of the destination items.</typeparam>
        /// <param name="src">The source sequence.</param>
        /// <param name="collSelector">The selector for the temporary items.</param>
        /// <param name="resSelector">The selector for the real items.</param>
        /// <returns>The new flatten list of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" />, <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// At least one result of <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSrc, TColl, TResult>(IEnumerable<TSrc> src,
                                                                            Func<TSrc, IEnumerable<TColl>> collSelector,
                                                                            Func<TSrc, TColl, long, TResult> resSelector)
        {
            if (collSelector == null)
            {
                throw new ArgumentNullException("collSelector");
            }

            return SelectMany<TSrc, TColl, TResult>(src,
                                                    delegate(TSrc s, long index)
                                                    {
                                                        return collSelector(s);
                                                    },
                                                    resSelector);
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TSrc">Type of the source items.</typeparam>
        /// <typeparam name="TColl">The collection type.</typeparam>
        /// <typeparam name="TResult">Tyoe of the destination items.</typeparam>
        /// <param name="src">The source sequence.</param>
        /// <param name="collSelector">The selector for the temporary items.</param>
        /// <param name="resSelector">The selector for the real items.</param>
        /// <returns>The new flatten list of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" />, <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// At least one result of <paramref name="collSelector" /> and/or <paramref name="resSelector" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSrc, TColl, TResult>(IEnumerable<TSrc> src,
                                                                            Func<TSrc, long, IEnumerable<TColl>> collSelector,
                                                                            Func<TSrc, TColl, long, TResult> resSelector)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (collSelector == null)
            {
                throw new ArgumentNullException("collSelector");
            }

            if (resSelector == null)
            {
                throw new ArgumentNullException("resSelector");
            }

            using (IEnumerator<TSrc> eSRC = src.GetEnumerator())
            {
                long idxSrc = -1;

                while (eSRC.MoveNext())
                {
                    ++idxSrc;
                    TSrc srcItem = eSRC.Current;

                    IEnumerable<TColl> seqColl = collSelector(srcItem, idxSrc);

                    using (IEnumerator<TColl> eCOLL = seqColl.GetEnumerator())
                    {
                        long idxColl = -1;

                        while (eCOLL.MoveNext())
                        {
                            ++idxColl;

                            yield return resSelector(srcItem, eCOLL.Current, idxColl);
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}