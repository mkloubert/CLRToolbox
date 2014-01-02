// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Creates an object from a data record.
        /// </summary>
        /// <typeparam name="TRead">Type of the reader.</typeparam>
        /// <typeparam name="T">Type of the created objects.</typeparam>
        /// <param name="reader">The record from where to build the object from.</param>
        /// <param name="func">The function that create the result object.</param>
        /// <returns>The created objects.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> BuildAll<TRead, T>(TRead reader, Func<TRead, T> func)
            where TRead : System.Data.IDataReader
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return BuildAll<TRead, object, T>(reader,
                                         delegate(TRead r, object state)
                                         {
                                             return func(r);
                                         },
                                         null);
        }

        /// <summary>
        /// Creates a list of objects from a data reader.
        /// </summary>
        /// <typeparam name="TRead">Type of the reader.</typeparam>
        /// <typeparam name="S">Type of <paramref name="funcState" />.</typeparam>
        /// <typeparam name="T">Type of the created objects.</typeparam>
        /// <param name="reader">The record from where to build the object from.</param>
        /// <param name="func">The function that create the result object.</param>
        /// <param name="funcState">The second argument of <paramref name="func" />.</param>
        /// <returns>The created objects.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> BuildAll<TRead, S, T>(TRead reader, Func<TRead, S, T> func, S funcState)
            where TRead : System.Data.IDataReader
        {
            return BuildAll<TRead, S, T>(reader,
                                         func,
                                         delegate(TRead r)
                                         {
                                             return funcState;
                                         });
        }

        /// <summary>
        /// Creates a list of objects from a data reader.
        /// </summary>
        /// <typeparam name="TRead">Type of the reader.</typeparam>
        /// <typeparam name="S">Type of the result of <paramref name="funcStateFactory" />.</typeparam>
        /// <typeparam name="T">Type of the created objects.</typeparam>
        /// <param name="reader">The record from where to build the object from.</param>
        /// <param name="func">The function that create the result object.</param>
        /// <param name="funcStateFactory">The function that returns the second argument of <paramref name="func" />.</param>
        /// <returns>The created objects.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" />, <paramref name="func" /> and/or <paramref name="funcStateFactory" /> are
        /// <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> BuildAll<TRead, S, T>(TRead reader, Func<TRead, S, T> func, Func<TRead, S> funcStateFactory)
            where TRead : System.Data.IDataReader
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            while (reader.Read())
            {
                yield return Build<TRead, S, T>(reader, func, funcStateFactory);
            }
        }

        #endregion Methods
    }
}
