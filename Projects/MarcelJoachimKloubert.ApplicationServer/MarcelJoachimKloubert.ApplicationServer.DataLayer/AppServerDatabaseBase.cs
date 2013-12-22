// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using MarcelJoachimKloubert.ApplicationServer.Data.Entities;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.ApplicationServer.DataLayer
{
    /// <summary>
    /// A basic application server database connection.
    /// </summary>
    public abstract class AppServerDatabaseBase : DisposableBase,
                                                  IAppServerDatabase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerDatabaseBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AppServerDatabaseBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServerDatabaseBase" /> class.
        /// </summary>
        protected AppServerDatabaseBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerDatabase.Query{E}()" />
        public abstract IQueryable<E> Query<E>() where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity;
        // Private Methods (1) 

        IQueryable<E> IQueryableDatabase.Query<E>()
        {
            throw new InvalidOperationException(string.Format("Please invoke as '{0}' object.",
                                                              typeof(IAppServerEntity).FullName));
        }

        #endregion Methods
    }
}
