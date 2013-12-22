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

        #region Methods (2)

        // Public Methods (1) 

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
        // Protected Methods (1) 

        /// <summary>
        /// The logic for the <see cref="QueryableDatabaseBase.Query{E}()" /> method.
        /// </summary>
        /// <typeparam name="E">Type of the entity to query.</typeparam>
        /// <returns>The query.</returns>
        protected abstract IQueryable<E> OnQuery<E>();

        #endregion Methods
    }
}
