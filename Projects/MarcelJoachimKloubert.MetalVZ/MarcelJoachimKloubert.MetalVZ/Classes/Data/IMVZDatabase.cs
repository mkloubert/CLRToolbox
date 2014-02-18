// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Data;
using System.Linq;

namespace MarcelJoachimKloubert.MetalVZ.Classes.Data
{
    /// <summary>
    /// Describes an object that accesses the data(base) of the current MetalVZ application.
    /// </summary>
    public interface IMVZDatabase : IMVZObject, IQueryableDatabase
    {
        #region Operations (4)

        /// <inheriteddoc />
        new void Add<E>(E entity) where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity;

        /// <inheriteddoc />
        new void Attach<E>(E entity) where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity;

        /// <inheriteddoc />
        new IQueryable<E> Query<E>() where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity;

        /// <inheriteddoc />
        new void Remove<E>(E entity) where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity;

        #endregion Operations
    }
}
