// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Entities
{
    /// <summary>
    /// A basic entity repository.
    /// </summary>
    public abstract class EntityRepositoryBase : DisposableBase, IEntityRepository
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepositoryBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected EntityRepositoryBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepositoryBase" /> class.
        /// </summary>
        protected EntityRepositoryBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (3)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="LoadAll{E}()" />
        public IEnumerable<E> LoadAll<E>() where E : IEntity
        {
            IEnumerable<E> result;
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                result = this.OnLoadAll<E>();
            }

            return CollectionHelper.ToEnumerableSafe(result, true);
        }
        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DisposableBase.OnDispose(bool)" />
        protected override void OnDispose(bool disposing)
        {
            // dummy
        }

        /// <summary>
        /// Stores the logic of <see cref="EntityRepositoryBase.LoadAll{E}()" /> method.
        /// </summary>
        /// <typeparam name="E">Type of the entity.</typeparam>
        /// <returns>The lazy loaded data.</returns>
        protected abstract IEnumerable<E> OnLoadAll<E>() where E : IEntity;

        #endregion Methods
    }
}
