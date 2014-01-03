// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Reflection;
using MarcelJoachimKloubert.ApplicationServer.DataLayer.Entities;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.AppServer.Services.AppServerDb
{
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.DataLayer.Entities.IAppServerEntityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class AppServerEntityRepository : AppServerEntityRepositoryBase
    {
        #region Fields (2)

        private readonly IDbConnection _CONNECTION;
        private readonly AppServerImpl _SERVER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal AppServerEntityRepository(AppServerImpl server)
        {
            this._SERVER = server;

            // define DbConnection
            string dbProvider;
            this._CONNECTION = this._SERVER.TryGetServerDbConnectionByConfig(out dbProvider);

            if (this._CONNECTION == null)
            {
                throw new NotSupportedException(string.Format("'{0}' provider is NOT supported!",
                                                              dbProvider));
            }

            this._CONNECTION.Open();
        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (2) 

        protected override void OnDispose(bool disposing)
        {
            base.OnDispose(disposing);

            if (disposing)
            {
                this._CONNECTION.Dispose();
            }
        }

        protected override IEnumerable<E> OnLoadAllOfAppServer<E>()
        {
            using (IDbCommand cmd = this._CONNECTION.CreateCommand())
            {
                string table;
                string schema;
                EntityHelper.TryGetTableNameAndSchema<E>(name: out table,
                                                         schema: out schema);

                if (string.IsNullOrWhiteSpace(schema))
                {
                    cmd.CommandText = string.Format("SELECT * FROM {0}; ",
                                                    table);
                }
                else
                {
                    cmd.CommandText = string.Format("SELECT * FROM {0}.{1}; ",
                                                    schema, table);
                }

                foreach (var rec in cmd.ExecuteEnumerableReader())
                {
                    var newEntity = Activator.CreateInstance<E>();

                    var loadFrom = CollectionHelper.Single(newEntity.GetType()
                                                                    .GetMethods(BindingFlags.Instance | BindingFlags.Public),
                                                           m => m.Name == "LoadFrom" &&
                                                                m.GetGenericArguments().Length == 1 &&
                                                                m.GetParameters().Length == 2)
                                                   .MakeGenericMethod(typeof(global::System.Data.IDataRecord));

                    loadFrom.Invoke(newEntity,
                                    new object[] { rec, null });

                    yield return newEntity;
                }
            }
        }

        #endregion Methods
    }
}
