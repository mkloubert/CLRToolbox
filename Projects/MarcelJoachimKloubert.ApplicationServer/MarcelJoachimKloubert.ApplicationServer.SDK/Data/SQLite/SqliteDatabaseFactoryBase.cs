// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

namespace MarcelJoachimKloubert.ApplicationServer.Data.SQLite
{
    /// <summary>
    /// A basic factory that creates and opens SQLite databases and connections.
    /// </summary>
    public abstract class SqliteDatabaseFactoryBase : TMObject,
                                                      ISqliteDatabaseFactory
    {
        #region Fields (1)

        /// <summary>
        /// ´Store the name of a database that is stored in memory.
        /// </summary>
        public const string DB_NAME_MEMORY = ":memory:";

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteDatabaseFactoryBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected SqliteDatabaseFactoryBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteDatabaseFactoryBase" /> class.
        /// </summary>
        protected SqliteDatabaseFactoryBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (4)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISqliteDatabaseFactory.OpenDatabase(IEnumerable{char}, bool)" />
        public IDbConnection OpenDatabase(IEnumerable<char> name,
                                          bool canWrite = false)
        {
            var result = this.OnOpenDatabase(this.ParseDatabaseName(name.AsString()),
                                             canWrite);
            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISqliteDatabaseFactory.OpenTempDatabase()" />
        public IDbConnection OpenTempDatabase()
        {
            return this.OpenDatabase(DB_NAME_MEMORY, true);
        }
        // Protected Methods (2) 

        /// <summary>
        /// The logic for <see cref="SqliteDatabaseFactoryBase.OpenDatabase(IEnumerable{char}, bool)" /> method.
        /// </summary>
        /// <param name="name">The name of the database to open.</param>
        /// <param name="canWrite">Database should be writable or not.</param>
        /// <returns>The opened instance.</returns>
        protected abstract IDbConnection OnOpenDatabase(string name,
                                                        bool canWrite);

        /// <summary>
        /// Parses a string to use as database name.
        /// </summary>
        /// <param name="name">The input name.</param>
        /// <returns>The parsed version of <paramref name="name" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="name" /> is invalid.
        /// </exception>
        protected string ParseDatabaseName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            name = name.ToLower().Trim();
            if (name == string.Empty)
            {
                throw new FormatException();
            }

            return name;
        }

        #endregion Methods
    }
}
