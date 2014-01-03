// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Data.Entities;

namespace MarcelJoachimKloubert.ApplicationServer.DataLayer.Entities
{
    /// <summary>
    /// Describes the entity repository of the application server.
    /// </summary>
    public interface IAppServerEntityRepository : IEntityRepository
    {
        #region Operations (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEntityRepository.LoadAll{E}()" />
        new IEnumerable<E> LoadAll<E>() where E : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;

        #endregion Operations
    }
}
