// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// Executes a command and returns it as sequence of underlying data record.
        /// </summary>
        /// <param name="cmd">The command to execute.</param>
        /// <returns>The (lazy) sequence of command.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cmd" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<IDataRecord> ExecuteEnumerableReader(IDbCommand cmd)
        {
            return ExecuteEnumerableReaderInner<IDataRecord>(cmd);
        }
        // Private Methods (1) 

        private static IEnumerable<TRec> ExecuteEnumerableReaderInner<TRec>(IDbCommand cmd)
            where TRec : global::System.Data.IDataRecord
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return (TRec)reader;
                }
            }
        }

        #endregion Methods
    }
}
