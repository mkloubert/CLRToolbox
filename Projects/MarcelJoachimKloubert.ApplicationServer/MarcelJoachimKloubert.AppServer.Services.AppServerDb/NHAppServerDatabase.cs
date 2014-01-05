// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Linq;
using MarcelJoachimKloubert.ApplicationServer.DataLayer;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.AppServer.Services.AppServerDb
{
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.DataLayer.IAppServerDatabase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class NHAppServerDatabase : AppServerDatabaseBase
    {
        #region Fields (3)

        private readonly Configuration _CONF;
        private const string _RES_NAMESPACE_PREFIX = "MarcelJoachimKloubert.AppServer.Services.AppServerDb.Resources.";
        private readonly ISessionFactory _SESSION_FACTORY;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal NHAppServerDatabase(AppServerImpl server,
                                     IAppServerDataConnection conn)
        {
            // configure
            {
                var newCfg = new Configuration();
                newCfg.DataBaseIntegration(c =>
                    {
                        c.Dialect<MsSql2008Dialect>();
                        c.ConnectionString = conn.ConnectionString;
                        c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                        c.SchemaAction = SchemaAutoAction.Create;
                    });

                // entity types
                {
                    var mapper = new ModelMapper();

                    var entityTypes = server.EntityAssemblies
                                            .SelectMany(asm => asm.GetTypes())
                                            .Where(t => t.IsClass &&
                                                        !t.IsAbstract && t.GetInterfaces()
                                                                          .Any(it => it.Equals(typeof(global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity))));

                    foreach (var et in entityTypes)
                    {
                        var entityMapperType = typeof(AppServerEntityMapper<>).MakeGenericType(et);

                        var entityMapper = (IConformistHoldersProvider)Activator.CreateInstance(entityMapperType,
                                                                                                nonPublic: true);
                        mapper.AddMapping(entityMapper);
                    }

                    newCfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
                }

                this._CONF = newCfg;
            }

            // this._SESSION_FACTORY = this._CONF.BuildSessionFactory();
        }

        #endregion Constructors

        #region Properties (1)

        public override bool CanUpdate
        {
            get { return true; }
        }

        #endregion Properties

        #region Methods (5)

        // Public Methods (4) 

        public override void Add<E>(E entity)
        {
            throw new NotImplementedException();
        }

        public override void Attach<E>(E entity)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<E> Query<E>()
        {
            throw new NotImplementedException();
        }

        public override void Remove<E>(E entity)
        {
            throw new NotImplementedException();
        }
        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
