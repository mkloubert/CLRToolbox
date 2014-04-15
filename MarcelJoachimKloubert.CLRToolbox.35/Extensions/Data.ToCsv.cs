// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Data;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Data
{
    static partial class ClrToolboxDataExtensionMethods
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DataHelper.ToCsv(DataTable)" />
        public static string ToCsv(this DataTable table)
        {
            return DataHelper.ToCsv(table);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DataHelper.ToCsv(DataTable, bool)" />
        public static string ToCsv(this DataTable table, bool addHeader)
        {
            return DataHelper.ToCsv(table, addHeader);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DataHelper.ToCsv(DataTable, TextWriter)" />
        public static void ToCsv(this DataTable table, TextWriter writer)
        {
            DataHelper.ToCsv(table, writer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DataHelper.ToCsv(DataTable, StringBuilder)" />
        public static void ToCsv(this DataTable table, StringBuilder builder)
        {
            DataHelper.ToCsv(table, builder);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DataHelper.ToCsv(DataTable, TextWriter, bool)" />
        public static void ToCsv(this DataTable table, TextWriter writer, bool addHeader)
        {
            DataHelper.ToCsv(table, writer, addHeader);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DataHelper.ToCsv(DataTable, StringBuilder, bool)" />
        public static void ToCsv(this DataTable table, StringBuilder builder, bool addHeader)
        {
            DataHelper.ToCsv(table, builder, addHeader);
        }

        #endregion Methods
    }
}