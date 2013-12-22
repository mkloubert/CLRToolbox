// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using MarcelJoachimKloubert.ApplicationServer.DataLayer;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.AppServer.Services.AppServerDb
{
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.DataLayer.IAppServerDatabase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed partial class DbContextAppServerDatabase : AppServerDatabaseBase
    {
        #region Fields (7)

        private const string _CONFIG_CATEGORY_DATABASE = "database";
        private const string _CONFIG_VALUE_CONNECTION_STRING = "connection_string";
        private readonly AppServerDbContext _DB_CONTEXT;
        private const string _DB_PROVIDER_ADONET_MSSQL = "ado_mssql";
        private const string _DB_PROVIDER_ADONET_ODBC = "ado_odbc";
        private const string _DB_PROVIDER_ADONET_OLE = "ado_oledb";
        private readonly AppServerImpl _SERVER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal DbContextAppServerDatabase(AppServerImpl server)
        {
            this._SERVER = server;

            // database provider
            string dbProvider;
            this._SERVER
                .Config
                .TryGetValue(category: _CONFIG_CATEGORY_DATABASE,
                             name: "provider",
                             value: out dbProvider,
                             defaultVal: _DB_PROVIDER_ADONET_MSSQL);

            // define DbConnection
            DbConnection dbConn = null;
            switch ((dbProvider ?? string.Empty).ToLower().Trim())
            {
                case _DB_PROVIDER_ADONET_MSSQL:
                    // ADO.NET - Micsoroft SQL Server
                    {
                        var connStr = this._SERVER
                                          .Config
                                          .GetValue<string>(category: _CONFIG_CATEGORY_DATABASE,
                                                            name: _CONFIG_VALUE_CONNECTION_STRING);

                        dbConn = new SqlConnection(connStr);
                    }
                    break;

                case _DB_PROVIDER_ADONET_ODBC:
                    // ADO.NET - ODBC
                    {
                        var connStr = this._SERVER
                                          .Config
                                          .GetValue<string>(category: _CONFIG_CATEGORY_DATABASE,
                                                            name: _CONFIG_VALUE_CONNECTION_STRING);

                        dbConn = new OdbcConnection(connStr);
                    }
                    break;

                case _DB_PROVIDER_ADONET_OLE:
                    // ADO.NET - OLE DB
                    {
                        var connStr = this._SERVER
                                          .Config
                                          .GetValue<string>(category: _CONFIG_CATEGORY_DATABASE,
                                                            name: _CONFIG_VALUE_CONNECTION_STRING);

                        dbConn = new OleDbConnection(connStr);
                    }
                    break;
            }

            if (dbConn == null)
            {
                throw new NotSupportedException(string.Format("'{0}' provider is NOT supported!",
                                                              dbProvider));
            }

            this._DB_CONTEXT = new AppServerDbContext(dbConn,
                                                      this._SERVER.EntityAssemblies,
                                                      true);
        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (1) 

        public override IQueryable<E> Query<E>()
        {
            return this._DB_CONTEXT
                       .GetDbSet<E>();
        }
        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this._DB_CONTEXT
                    .Dispose();
            }
        }

        #endregion Methods
    }
}
