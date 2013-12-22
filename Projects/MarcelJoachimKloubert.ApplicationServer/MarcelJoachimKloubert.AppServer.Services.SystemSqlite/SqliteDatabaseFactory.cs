// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SQLite;
using System.IO;
using MarcelJoachimKloubert.ApplicationServer.Data.SQLite;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.AppServer.Services.SystemSqlite
{
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.Data.SQLite.ISqliteDatabaseFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class SqliteDatabaseFactory : SqliteDatabaseFactoryBase
    {
        #region Fields (1)

        private readonly AppServerImpl _SERVER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal SqliteDatabaseFactory(AppServerImpl server)
        {
            this._SERVER = server;
        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override IDbConnection OnOpenDatabase(string name, bool canWrite)
        {
            SQLiteConnection result = null;

            try
            {
                string dbName = name;
                if (dbName != DB_NAME_MEMORY)
                {
                    // not in memory ... save to file

                    var dbDir = new DirectoryInfo(Path.Combine(this._SERVER.WorkingDirectory, "db"));
                    if (!dbDir.Exists)
                    {
                        dbDir.Create();
                        dbDir.Refresh();
                    }

                    var dbFile = new FileInfo(Path.Combine(dbDir.FullName, string.Format("{0}.db",
                                                                                         name)));
                    if (!canWrite && !dbFile.Exists)
                    {
                        throw new InvalidOperationException(string.Format("Cannot open the non-existing file '{0}' in read-only mode!",
                                                                          dbFile.FullName));
                    }

                    dbName = dbFile.FullName;
                }

                string connStr;
                if (canWrite)
                {
                    connStr = string.Format(@"Data Source={0}; Version=3",
                                            dbName);
                }
                else
                {
                    connStr = string.Format(@"Data Source={0}; Version=3; Read Only=True",
                                            dbName);
                }

                result = new SQLiteConnection(connStr);
                return result.OpenAndReturn();
            }
            catch
            {
                if (result != null)
                {
                    result.Dispose();
                }

                throw;
            }
        }

        #endregion Methods
    }
}
