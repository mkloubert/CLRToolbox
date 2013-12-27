// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.ApplicationServer.DataLayer
{
    /// <summary>
    /// A basic application server database connection.
    /// </summary>
    public abstract class AppServerDatabaseBase : DisposableBase,
                                                  IAppServerDatabase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerDatabaseBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AppServerDatabaseBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerDatabaseBase" /> class.
        /// </summary>
        protected AppServerDatabaseBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IDatabase.CanUpdate" />
        public abstract bool CanUpdate
        {
            get;
        }

        #endregion Properties

        #region Methods (10)

        // Public Methods (5) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Add{E}(E)" />
        public abstract void Add<E>(E entity) where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Attach{E}(E)" />
        public abstract void Attach<E>(E entity) where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerDatabase.Query{E}()" />
        public abstract IQueryable<E> Query<E>() where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Remove{E}(E)" />
        public abstract void Remove<E>(E entity) where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IDatabase.Update()" />
        public void Update()
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (!this.CanUpdate)
                {
                    throw new InvalidOperationException();
                }

                this.OnUpdate();
            }
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for <see cref="DatabaseBase.Update()" /> method.
        /// </summary>
        protected virtual void OnUpdate()
        {
            throw new NotImplementedException();
        }
        // Private Methods (4) 

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
