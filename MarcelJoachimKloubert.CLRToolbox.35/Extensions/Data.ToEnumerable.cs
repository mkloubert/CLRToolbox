// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.ToEnumerable(IDataReader)" />
        public static IEnumerable<IDataRecord> ToEnumerable(this IDataReader reader)
        {
            return DataHelper.ToEnumerable(reader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.ToEnumerable{TReader, TRec}(TReader, Func{TReader, TRec})" />
        public static IEnumerable<TRec> ToEnumerable<TReader, TRec>(this TReader reader, Func<TReader, TRec> selector)
            where TRec : global::System.Data.IDataRecord
            where TReader : global::System.Data.IDataReader
        {
            return DataHelper.ToEnumerable<TReader, TRec>(reader, selector);
        }

        #endregion Methods
    }
}
