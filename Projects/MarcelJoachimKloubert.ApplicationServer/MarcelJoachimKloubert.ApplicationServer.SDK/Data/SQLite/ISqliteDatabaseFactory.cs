// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;
using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.ApplicationServer.Data.SQLite
{
    /// <summary>
    /// A factory that creates and opens SQLite databases and connections.
    /// </summary>
    public interface ISqliteDatabaseFactory : ITMObject
    {
        #region Operations (2)

        /// <summary>
        /// Opens a database.
        /// </summary>
        /// <param name="name">The name of the database.</param>
        /// <param name="canWrite">Database should be writable or not.</param>
        /// <returns>The new connection.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="name" /> is invalid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Tried to open a non-existing file and <paramref name="canWrite" /> is <see langword="false" />.
        /// </exception>
        IDbConnection OpenDatabase(IEnumerable<char> name,
                                   bool canWrite = true);

        /// <summary>
        /// Opens a writable temporary database, that is usually stored in memory.
        /// </summary>
        /// <returns>The new connection.</returns>
        IDbConnection OpenTempDatabase();

        #endregion Operations
    }
}
