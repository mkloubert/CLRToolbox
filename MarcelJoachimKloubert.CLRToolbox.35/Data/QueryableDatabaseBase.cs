// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// A basic queryable database connection.
    /// </summary>
    public abstract class QueryableDatabaseBase : DatabaseBase,
                                                  IQueryableDatabase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryableDatabaseBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected QueryableDatabaseBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryableDatabaseBase" /> class.
        /// </summary>
        protected QueryableDatabaseBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (8)

        // Public Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Add{E}(E)" />
        public void Add<E>(E entity)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.OnAdd<E>(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Attach{E}(E)" />
        public void Attach<E>(E entity)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.OnAttach<E>(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Query{E}()" />
        public IQueryable<E> Query<E>()
        {
            IQueryable<E> query;
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                query = this.OnQuery<E>();
                if (query == null)
                {
                    throw new NullReferenceException("query");
                }
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Remove{E}(E)" />
        public void Remove<E>(E entity)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.OnRemove<E>(entity);
            }
        }
        // Protected Methods (4) 

        /// <summary>
        /// The logic for the <see cref="QueryableDatabaseBase.Add{E}(E)" /> method.
        /// </summary>
        /// <typeparam name="E">Type of the entity.</typeparam>
        /// <param name="entity">The entity to add.</param>
        protected virtual void OnAdd<E>(E entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The logic for the <see cref="QueryableDatabaseBase.Attach{E}(E)" /> method.
        /// </summary>
        /// <typeparam name="E">Type of the entity.</typeparam>
        /// <param name="entity">The entity to attach.</param>
        protected virtual void OnAttach<E>(E entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The logic for the <see cref="QueryableDatabaseBase.Query{E}()" /> method.
        /// </summary>
        /// <typeparam name="E">Type of the entity to query.</typeparam>
        /// <returns>The query.</returns>
        protected abstract IQueryable<E> OnQuery<E>();

        /// <summary>
        /// The logic for the <see cref="QueryableDatabaseBase.Remove{E}(E)" /> method.
        /// </summary>
        /// <typeparam name="E">Type of the entity.</typeparam>
        /// <param name="entity">The entity to remove.</param>
        protected virtual void OnRemove<E>(E entity)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
