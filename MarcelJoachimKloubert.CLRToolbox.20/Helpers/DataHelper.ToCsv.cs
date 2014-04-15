// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Data;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class DataHelper
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        /// Creates CSV data from a <see cref="DataTable" />.
        /// The generated data is stored in english format.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>The created data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="table" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>(Column) Header is added.</remarks>
        public static string ToCsv(DataTable table)
        {
            return ToCsv(table, true);
        }

        /// <summary>
        /// Creates CSV data from a <see cref="DataTable" />.
        /// The generated data is stored in english format.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="addHeader">Add column header or not.</param>
        /// <returns>The created data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="table" /> is <see langword="null" />.
        /// </exception>
        public static string ToCsv(DataTable table, bool addHeader)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            StringBuilder builder = new StringBuilder();
            ToCsv(table);

            return builder.ToString();
        }

        /// <summary>
        /// Writes CSV data from a <see cref="DataTable" /> to a <see cref="TextWriter" />.
        /// The generated data is stored in english format.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="writer">The writer where to write the data to.</param>
        /// <returns>The created data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="table" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        /// <remarks>(Column) Header is added.</remarks>
        public static void ToCsv(DataTable table, TextWriter writer)
        {
            ToCsv(table, writer, true);
        }

        /// <summary>
        /// Writes CSV data from a <see cref="DataTable" /> to a <see cref="StringBuilder" />.
        /// The generated data is stored in english format.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="builder">The <see cref="StringBuilder" /> where to write the data to.</param>
        /// <returns>The created data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="table" /> and/or <paramref name="builder" /> are <see langword="null" />.
        /// </exception>
        /// <remarks>(Column) Header is added.</remarks>
        public static void ToCsv(DataTable table, StringBuilder builder)
        {
            ToCsv(table, builder, true);
        }

        /// <summary>
        /// Writes CSV data from a <see cref="DataTable" /> to a <see cref="TextWriter" />.
        /// The generated data is stored in english format.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="writer">The writer where to write the data to.</param>
        /// <param name="addHeader">Add column header or not.</param>
        /// <returns>The created data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="table" /> and/or <paramref name="writer" /> are <see langword="null" />.
        /// </exception>
        public static void ToCsv(DataTable table, TextWriter writer, bool addHeader)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (addHeader)
            {
                DataColumnCollection columns = table.Columns;
                if (columns != null)
                {
                    for (int x = 0; x < columns.Count; x++)
                    {
                        if (x > 0)
                        {
                            writer.Write(',');
                        }

                        DataColumn col = columns[x];
                        if (col != null)
                        {
                            writer.Write(ParseForCsvCell(col.ColumnName));
                        }
                    }

                    writer.Write("\r\n");
                }
            }

            DataRowCollection rows = table.Rows;
            if (rows != null)
            {
                for (int y = 0; y < rows.Count; y++)
                {
                    if (y > 0)
                    {
                        writer.Write("\r\n");
                    }

                    DataRow row = rows[y];
                    if (row != null)
                    {
                        object[] cells = row.ItemArray;
                        if (cells != null)
                        {
                            for (int x = 0; x < cells.Length; x++)
                            {
                                if (x > 0)
                                {
                                    writer.Write(',');
                                }

                                object c = cells[x];

                                writer.Write(ParseForCsvCell(c));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes CSV data from a <see cref="DataTable" /> to a <see cref="StringBuilder" />.
        /// The generated data is stored in english format.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="builder">The <see cref="StringBuilder" /> where to write the data to.</param>
        /// <param name="addHeader">Add column header or not.</param>
        /// <returns>The created data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="table" /> and/or <paramref name="builder" /> are <see langword="null" />.
        /// </exception>
        public static void ToCsv(DataTable table, StringBuilder builder, bool addHeader)
        {
            using (StringWriter writer = new StringWriter(builder))
            {
                ToCsv(table, writer);
            }
        }

        #endregion Methods
    }
}