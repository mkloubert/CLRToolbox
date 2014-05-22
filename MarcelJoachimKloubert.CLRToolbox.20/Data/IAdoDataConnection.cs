// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Describes an object that stores the connection string for a data connection.
    /// </summary>
    public interface IAdoDataConnection : IDataConnection
    {
        #region Operations (2)

        /// <summary>
        /// Returns a new connection object that is based on the data of that object.
        /// </summary>
        /// <returns>The new connection object.</returns>
        IDbConnection GetConnection();

        /// <summary>
        /// Opens a new connection based of the data of that object.
        /// </summary>
        /// <returns>The new connection.</returns>
        IDbConnection OpenConnection();

        #endregion Operations
    }
}