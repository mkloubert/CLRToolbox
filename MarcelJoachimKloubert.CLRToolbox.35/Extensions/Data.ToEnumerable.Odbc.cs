// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Data.Odbc;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Data
{
    static partial class ClrToolboxDataExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.ToEnumerable(OdbcDataReader)" />
        public static IEnumerable<OdbcDataReader> ToEnumerable(this OdbcDataReader reader)
        {
            return DataHelper.ToEnumerable(reader);
        }

        #endregion Methods
    }
}
