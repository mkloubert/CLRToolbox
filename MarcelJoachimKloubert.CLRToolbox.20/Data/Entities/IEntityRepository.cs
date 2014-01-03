// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Entities
{
    /// <summary>
    /// Describes an entity repository.
    /// </summary>
    public interface IEntityRepository : ITMDisposable
    {
        #region Operations (1)

        /// <summary>
        /// Loads all entities of a specific type.
        /// </summary>
        /// <typeparam name="E">Type of the entity.</typeparam>
        /// <returns>The lazy loaded data.</returns>
        IEnumerable<E> LoadAll<E>() where E : global::MarcelJoachimKloubert.CLRToolbox.Data.Entities.IEntity;

        #endregion Operations
    }
}
