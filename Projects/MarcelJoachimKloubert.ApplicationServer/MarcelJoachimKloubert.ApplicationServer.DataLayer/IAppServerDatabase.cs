// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.ApplicationServer.DataLayer
{
    /// <summary>
    /// Describes an aplication server database connection.
    /// </summary>
    public interface IAppServerDatabase : IQueryableDatabase
    {
        #region Operations (4)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Add{E}(E)" />
        new void Add<E>(E entity) where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Attach{E}(E)" />
        new void Attach<E>(E entity) where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Query{E}()" />
        new IQueryable<E> Query<E>() where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IQueryableDatabase.Remove{E}(E)" />
        new void Remove<E>(E entity) where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        #endregion Operations
    }
}
