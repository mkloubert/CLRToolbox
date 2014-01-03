// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    #region CLASS: DataConnectionBase

    /// <summary>
    /// Basic object that stores the connection string for a data connection.
    /// </summary>
    public abstract class DataConnectionBase : TMObject, IDataConnection
    {
        #region Fields (1)

        private readonly string _CONNECTION_STRING;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="DataConnectionBase" /> class.
        /// </summary>
        /// <param name="connStr">The value for <see cref="DataConnectionBase.ConnectionString" /> property.</param>
        protected DataConnectionBase(IEnumerable<char> connStr)
        {
            this._CONNECTION_STRING = StringHelper.AsString(connStr);
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IDataConnection.ConnectionString" />
        public string ConnectionString
        {
            get { return this._CONNECTION_STRING; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IDataConnection.Provider" />
        public abstract Type Provider
        {
            get;
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: DataConnectionBase<P>

    /// <summary>
    /// Basic object that stores the connection string for a data connection.
    /// </summary>
    /// <typeparam name="P">The value for <see cref="DataConnectionBase{P}.Provider" /> property.</typeparam>
    public abstract class DataConnectionBase<P> : DataConnectionBase
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="DataConnectionBase" /> class.
        /// </summary>
        /// <param name="connStr">The value for <see cref="DataConnectionBase.ConnectionString" /> property.</param>
        protected DataConnectionBase(IEnumerable<char> connStr)
            : base(connStr)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataConnectionBase.Provider" />
        public override sealed Type Provider
        {
            get { return typeof(P); }
        }

        #endregion Properties
    }

    #endregion
}
