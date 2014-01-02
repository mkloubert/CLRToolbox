// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Data
{
    static partial class ClrToolboxDataExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.FromDbValue{T}(IDataRecord, IEnumerable{char})" />
        public static object FromDbValue<T>(this IDataRecord rec, IEnumerable<char> name)
        {
            return DataHelper.FromDbValue<T>(rec, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.FromDbValue{T}(IDataRecord, int)" />
        public static object FromDbValue<T>(this IDataRecord rec, int ordinal)
        {
            return DataHelper.FromDbValue<T>(rec, ordinal);
        }

        #endregion Methods
    }
}
