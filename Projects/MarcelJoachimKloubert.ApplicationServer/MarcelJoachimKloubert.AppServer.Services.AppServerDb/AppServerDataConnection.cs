// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Data;
using MarcelJoachimKloubert.ApplicationServer.DataLayer;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Data;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.AppServer.Services.AppServerDb
{
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.DataLayer.IAppServerDataConnection))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class AppServerDataConnection : TMObject,
                                                    IAppServerDataConnection
    {
        #region Fields (2)

        private readonly IAdoDataConnection _INNER_CONNECTION;
        private readonly AppServerImpl _SERVER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal AppServerDataConnection(AppServerImpl server)
        {
            this._SERVER = server;

            string dbProvider;
            this._INNER_CONNECTION = this._SERVER
                                         .TryGetServerDbConnectionDataByConfig(out dbProvider);

            if (this._INNER_CONNECTION == null)
            {
                throw new NotSupportedException(string.Format("'{0}' provider is NOT supported!",
                                                              dbProvider));
            }
        }

        #endregion Constructors

        #region Properties (2)

        public string ConnectionString
        {
            get { return this._INNER_CONNECTION.ConnectionString; }
        }

        public Type Provider
        {
            get { return this._INNER_CONNECTION.Provider; }
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (2) 

        public IDbConnection GetConnection()
        {
            return this._INNER_CONNECTION.GetConnection();
        }

        public IDbConnection OpenConnection()
        {
            return this._INNER_CONNECTION.OpenConnection();
        }

        #endregion Methods
    }
}
