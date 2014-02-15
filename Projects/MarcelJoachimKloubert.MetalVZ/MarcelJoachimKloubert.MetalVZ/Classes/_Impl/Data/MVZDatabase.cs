// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.MetalVZ.Classes.Data;
using System;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace MarcelJoachimKloubert.MetalVZ.Classes._Impl.Data
{
    [Export(typeof(global::MarcelJoachimKloubert.MetalVZ.Classes.Data.IMVZDatabase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed partial class MVZDatabase : MVZDisposableBase, IMVZDatabase
    {
        #region Fields (2)

        private const string _CONFIG_CATEGORY_DATABASE = "database";
        private readonly MVZDbContext _DB_CONTEXT;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal MVZDatabase(IConfigRepository repo)
        {
            this._DB_CONTEXT = new MVZDbContext(conn: CreateDbConnection(repo),
                                                ownConnection: true);
        }

        #endregion Constructors

        #region Properties (1)

        public bool CanUpdate
        {
            get { return true; }
        }

        #endregion Properties

        #region Methods (11)

        // Public Methods (5) 

        public void Add<E>(E entity) where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ThrowIfDisposed();

            this._DB_CONTEXT
                .GetDbSet<E>()
                .Add(entity);
        }

        public void Attach<E>(E entity) where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ThrowIfDisposed();

            this._DB_CONTEXT
                .GetDbSet<E>()
                .Attach(entity);
        }

        public IQueryable<E> Query<E>() where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity
        {
            return this._DB_CONTEXT
                       .GetDbSet<E>();
        }

        public void Remove<E>(E entity) where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ThrowIfDisposed();

            this._DB_CONTEXT
                .GetDbSet<E>()
                .Remove(entity);
        }

        public void Update()
        {
            this._DB_CONTEXT
                .SaveChanges();
        }
        // Protected Methods (1) 

        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this._DB_CONTEXT.Dispose();
            }
        }
        // Private Methods (5) 

        private static DbConnection CreateDbConnection(IConfigRepository repo)
        {
            Type providerType;

            // get .NET class of DbConnection
            string providerName;
            if (repo.TryGetValue<string>("provider", out providerName, _CONFIG_CATEGORY_DATABASE) &&
                string.IsNullOrWhiteSpace(providerName) == false)
            {
                providerName = providerName.Trim();

                providerType = CollectionHelper.Single(AppDomain.CurrentDomain
                                                                .GetAssemblies()
                                                                .SelectMany(asm => asm.GetTypes()),
                                                       t => t.FullName == providerName);
            }
            else
            {
                providerType = typeof(global::System.Data.SqlClient.SqlConnection);
            }

            // connection string
            string connStr;
            repo.TryGetValue<string>("connection_string", out connStr, _CONFIG_CATEGORY_DATABASE);

            // create instance of result
            object result;
            if (string.IsNullOrWhiteSpace(connStr))
            {
                result = Activator.CreateInstance(providerType);
            }
            else
            {
                result = Activator.CreateInstance(providerType,
                                                  args: new object[] { connStr });
            }

            return (DbConnection)result;
        }

        void IQueryableDatabase.Add<E>(E entity)
        {
            CollectionHelper.Single(this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance),
                                    m => m.Name == "Add" &&
                                         m.GetGenericArguments().Length == 1 &&
                                         m.GetParameters().Length == 1)
                            .MakeGenericMethod(typeof(E))
                            .Invoke(this, new object[] { entity });
        }

        void IQueryableDatabase.Attach<E>(E entity)
        {
            CollectionHelper.Single(this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance),
                                    m => m.Name == "Attach" &&
                                         m.GetGenericArguments().Length == 1 &&
                                         m.GetParameters().Length == 1)
                            .MakeGenericMethod(typeof(E))
                            .Invoke(this, new object[] { entity });
        }

        IQueryable<E> IQueryableDatabase.Query<E>()
        {
            return (IQueryable<E>)CollectionHelper.Single(this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance),
                                                          m => m.Name == "Query" &&
                                                               m.GetGenericArguments().Length == 1 &&
                                                               m.GetParameters().Length == 0)
                                                  .MakeGenericMethod(typeof(E))
                                                  .Invoke(this, new object[0]);
        }

        void IQueryableDatabase.Remove<E>(E entity)
        {
            CollectionHelper.Single(this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance),
                                    m => m.Name == "Remove" &&
                                         m.GetGenericArguments().Length == 1 &&
                                         m.GetParameters().Length == 1)
                            .MakeGenericMethod(typeof(E))
                            .Invoke(this, new object[] { entity });
        }

        #endregion Methods
    }
}
