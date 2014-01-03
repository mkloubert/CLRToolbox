// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox.Data.Entities;

namespace MarcelJoachimKloubert.ApplicationServer.DataLayer.Entities
{
    /// <summary>
    /// A basic entity repository of application server.
    /// </summary>
    public abstract class AppServerEntityRepositoryBase : EntityRepositoryBase,
                                                          IAppServerEntityRepository
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerEntityRepositoryBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AppServerEntityRepositoryBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerEntityRepositoryBase" /> class.
        /// </summary>
        protected AppServerEntityRepositoryBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (3)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="EntityRepositoryBase.OnLoadAll{E}>()" />
        protected override sealed IEnumerable<E> OnLoadAll<E>()
        {
            var genericOnLoadAllOfAppServerMethod = this.GetType()
                                                        .GetMethod("OnLoadAllOfAppServer",
                                                                   BindingFlags.Instance | BindingFlags.NonPublic);

            var onLoadAllOfAppServerMethod = genericOnLoadAllOfAppServerMethod.MakeGenericMethod(typeof(E));

            return (IEnumerable<E>)onLoadAllOfAppServerMethod.Invoke(this,
                                                                     new object[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerEntityRepositoryBase.OnLoadAll{E}>()" />
        protected abstract IEnumerable<E> OnLoadAllOfAppServer<E>() where E : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;
        // Private Methods (1) 

        IEnumerable<E> IAppServerEntityRepository.LoadAll<E>()
        {
            return this.LoadAll<E>();
        }

        #endregion Methods
    }
}
