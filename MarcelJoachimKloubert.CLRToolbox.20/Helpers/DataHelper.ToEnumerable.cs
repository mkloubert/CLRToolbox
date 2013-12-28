// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Converts a data reader to a lazy sequence of data records.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The sequence of data records.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<IDataRecord> ToEnumerable(IDataReader reader)
        {
            return ToEnumerable<IDataReader, IDataRecord>(reader,
                                                          delegate(IDataReader r)
                                                          {
                                                              return r;
                                                          });
        }

        /// <summary>
        /// Converts a data reader to a lazy sequence of data records.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="selector">The selector returns the current data record for the given reader of <paramref name="reader" />.</param>
        /// <returns>The sequence of data records.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> and/or <paramref name="selector" /> are <see langword="null" />.
        /// </exception>
        public static IEnumerable<TRec> ToEnumerable<TReader, TRec>(TReader reader, Func<TReader, TRec> selector)
            where TRec : global::System.Data.IDataRecord
            where TReader : global::System.Data.IDataReader
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            while (reader.Read())
            {
                yield return selector(reader);
            }
        }
        // Private Methods (1) 

        private static IEnumerable<TReader> ToEnumerableCommon<TReader>(TReader reader)
            where TReader : global::System.Data.IDataReader
        {
            return ToEnumerable<TReader, TReader>(reader,
                                                  delegate(TReader r)
                                                  {
                                                      return r;
                                                  });
        }

        #endregion Methods
    }
}
