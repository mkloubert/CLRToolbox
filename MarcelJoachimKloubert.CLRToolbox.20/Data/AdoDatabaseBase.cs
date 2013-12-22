// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    #region CLASS: AdoDatabaseBase<TConn>

    /// <summary>
    /// A basic ADO.NET database connection.
    /// </summary>
    /// <typeparam name="TConn">Type of the underlying connection.</typeparam>
    public abstract class AdoDatabaseBase<TConn> : DatabaseBase,
                                                   IAdoDatabase<TConn>
        where TConn : global::System.Data.IDbConnection
    {
        #region Fields (1)

        private readonly TConn _CONNECTION;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoDatabaseBase{TConn}" /> class.
        /// </summary>
        /// <param name="conn">The value for the <see cref="AdoDatabaseBase{TConn}.Connection" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conn" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        protected AdoDatabaseBase(TConn conn, object syncRoot)
            : base(syncRoot)
        {
            if (conn == null)
            {
                throw new ArgumentNullException("conn");
            }

            this._CONNECTION = conn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoDatabaseBase{TConn}" /> class.
        /// </summary>
        /// <param name="conn">The value for the <see cref="AdoDatabaseBase{TConn}.Connection" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conn" /> is <see langword="null" />.
        /// </exception>
        protected AdoDatabaseBase(TConn conn)
            : this(conn, new object())
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAdoDatabase.Connection" />
        public TConn Connection
        {
            get { return this._CONNECTION; }
        }

        IDbConnection IAdoDatabase.Connection
        {
            get { return this.Connection; }
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DisposableBase.OnDispose(bool)" />
        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                this._CONNECTION.Dispose();
            }
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: AdoDatabaseBase

    /// <summary>
    /// A basic general ADO.NET database connection.
    /// </summary>
    public abstract class AdoDatabaseBase : AdoDatabaseBase<global::System.Data.IDbConnection>
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoDatabaseBase" /> class.
        /// </summary>
        /// <param name="conn">The value for the <see cref="AdoDatabaseBase{TConn}.Connection" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conn" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        protected AdoDatabaseBase(IDbConnection conn, object syncRoot)
            : base(conn, syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoDatabaseBase" /> class.
        /// </summary>
        /// <param name="conn">The value for the <see cref="AdoDatabaseBase{TConn}.Connection" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conn" /> is <see langword="null" />.
        /// </exception>
        protected AdoDatabaseBase(IDbConnection conn)
            : base(conn)
        {

        }

        #endregion Constructors
    }

    #endregion
}
