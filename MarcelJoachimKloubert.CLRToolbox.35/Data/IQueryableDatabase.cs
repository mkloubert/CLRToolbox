// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Describes an queryable database connection.
    /// </summary>
    public interface IQueryableDatabase : IDatabase
    {
        #region Operations (1)

        /// <summary>
        /// Starts a query.
        /// </summary>
        /// <typeparam name="E">Type of the entity to query.</typeparam>
        /// <returns>The query.</returns>
        IQueryable<E> Query<E>();

        #endregion Operations
    }
}
