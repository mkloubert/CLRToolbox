// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    #region INTERFACE: IAdoDatabase

    /// <summary>
    /// Describes an ADO.NET database (connection).
    /// </summary>
    public interface IAdoDatabase : IDatabase
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        IDbConnection Connection { get; }

        #endregion Data Members
    }

    #endregion

    #region INTERFACE: IAdoDatabase

    /// <summary>
    /// Describes an ADO.NET database (connection).
    /// </summary>
    /// <typeparam name="TConn">Type of the underlying connection.</typeparam>
    public interface IAdoDatabase<TConn> : IAdoDatabase
        where TConn : global::System.Data.IDbConnection
    {
        #region Data Members (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAdoDatabase.Connection" />
        new TConn Connection { get; }

        #endregion Data Members
    }

    #endregion
}
