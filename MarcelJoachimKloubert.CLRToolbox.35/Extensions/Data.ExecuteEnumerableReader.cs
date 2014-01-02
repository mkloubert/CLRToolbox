// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Data;
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
        /// <see cref="DataHelper.ExecuteEnumerableReader(IDbCommand)" />
        public static IEnumerable<IDataRecord> ExecuteEnumerableReader(this IDbCommand cmd)
        {
            return DataHelper.ExecuteEnumerableReader(cmd);
        }

        #endregion Methods
    }
}
