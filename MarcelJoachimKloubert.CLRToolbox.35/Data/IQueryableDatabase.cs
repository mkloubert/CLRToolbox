// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Describes an queryable database connection.
    /// </summary>
    public interface IQueryableDatabase : IDatabase
    {
        #region Operations (4)

        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <typeparam name="E">Type of the entity to add.</typeparam>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity" /> is <see langword="null" />.</exception>
        void Add<E>(E entity);

        /// <summary>
        /// Attaches an entity.
        /// </summary>
        /// <typeparam name="E">Type of the entity to attach.</typeparam>
        /// <param name="entity">The entity to attach.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity" /> is <see langword="null" />.</exception>
        void Attach<E>(E entity);

        /// <summary>
        /// Starts a query.
        /// </summary>
        /// <typeparam name="E">Type of the entity to query.</typeparam>
        /// <returns>The query.</returns>
        IQueryable<E> Query<E>();

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <typeparam name="E">Type of the entity to remove.</typeparam>
        /// <param name="entity">The entity to remove.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity" /> is <see langword="null" />.</exception>
        void Remove<E>(E entity);

        #endregion Operations
    }
}
