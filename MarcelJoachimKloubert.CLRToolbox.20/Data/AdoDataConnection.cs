// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Stores the connection data for a ADO.NET connection.
    /// </summary>
    /// <typeparam name="P">Type of the provider.</typeparam>
    public class AdoDataConnection<P> : DataConnectionBase<P>,
                                        IAdoDataConnection
        where P : global::System.Data.IDbConnection, new()
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoDataConnection{P}" /> class.
        /// </summary>
        /// <param name="connStr">The value for <see cref="DataConnectionBase.ConnectionString" /> property.</param>
        public AdoDataConnection(IEnumerable<char> connStr)
            : base(connStr)
        {

        }

        #endregion Constructors

        #region Methods (4)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAdoDataConnection.GetConnection()" />
        public P GetConnection()
        {
            P result = Activator.CreateInstance<P>();
            result.ConnectionString = this.ConnectionString;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAdoDataConnection.OpenConnection()" />
        public P OpenConnection()
        {
            P result = this.GetConnection();
            result.Open();

            return result;
        }
        // Private Methods (2) 

        IDbConnection IAdoDataConnection.GetConnection()
        {
            return this.GetConnection();
        }

        IDbConnection IAdoDataConnection.OpenConnection()
        {
            return this.OpenConnection();
        }

        #endregion Methods
    }
}
