// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Creates an object from a data record.
        /// </summary>
        /// <typeparam name="TRec">Type of the record.</typeparam>
        /// <typeparam name="T">Type of the created object.</typeparam>
        /// <param name="rec">The record from where to build the object from.</param>
        /// <param name="func">The function that create the result object.</param>
        /// <returns>The created object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rec" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static T Build<TRec, T>(TRec rec, Func<TRec, T> func)
            where TRec : System.Data.IDataRecord
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return Build<TRec, object, T>(rec,
                                         delegate(TRec r, object state)
                                         {
                                             return func(r);
                                         },
                                         null);
        }

        /// <summary>
        /// Creates an object from a data record.
        /// </summary>
        /// <typeparam name="TRec">Type of the record.</typeparam>
        /// <typeparam name="S">Type of <paramref name="funcState" />.</typeparam>
        /// <typeparam name="T">Type of the created object.</typeparam>
        /// <param name="rec">The record from where to build the object from.</param>
        /// <param name="func">The function that create the result object.</param>
        /// <param name="funcState">The second argument of <paramref name="func" />.</param>
        /// <returns>The created object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rec" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static T Build<TRec, S, T>(TRec rec, Func<TRec, S, T> func, S funcState)
            where TRec : System.Data.IDataRecord
        {
            return Build<TRec, S, T>(rec,
                                     func,
                                     delegate(TRec r)
                                     {
                                         return funcState;
                                     });
        }

        /// <summary>
        /// Creates an object from a data record.
        /// </summary>
        /// <typeparam name="TRec">Type of the record.</typeparam>
        /// <typeparam name="S">Type of the result of <paramref name="funcStateFactory" />.</typeparam>
        /// <typeparam name="T">Type of the created object.</typeparam>
        /// <param name="rec">The record from where to build the object from.</param>
        /// <param name="func">The function that create the result object.</param>
        /// <param name="funcStateFactory">The function that returns the second argument of <paramref name="func" />.</param>
        /// <returns>The created object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rec" />, <paramref name="func" /> and/or <paramref name="funcStateFactory" /> are
        /// <see langword="null" />.
        /// </exception>
        public static T Build<TRec, S, T>(TRec rec, Func<TRec, S, T> func, Func<TRec, S> funcStateFactory)
            where TRec : System.Data.IDataRecord
        {
            if (rec == null)
            {
                throw new ArgumentNullException("rec");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            return func(rec,
                        funcStateFactory(rec));
        }

        #endregion Methods
    }
}
