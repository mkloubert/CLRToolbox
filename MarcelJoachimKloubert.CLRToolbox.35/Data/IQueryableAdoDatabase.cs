// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    #region INTERFACE: IQueryableAdoDatabase

    /// <summary>
    /// Describes an queryable database connection based on ADO.NET.
    /// </summary>
    public interface IQueryableAdoDatabase : IQueryableDatabase,
                                             IAdoDatabase
    {

    }

    #endregion

    #region INTERFACE: IQueryableAdoDatabase<TConn>

    /// <summary>
    /// Describes an queryable database connection based on ADO.NET.
    /// </summary>
    /// <typeparam name="TConn">Type of the underlying connection.</typeparam>
    public interface IQueryableAdoDatabase<TConn> : IQueryableAdoDatabase,
                                                    IAdoDatabase<TConn>
        where TConn : global::System.Data.IDbConnection
    {

    }

    #endregion
}
