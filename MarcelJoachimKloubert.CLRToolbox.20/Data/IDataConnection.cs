// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Describes an object that stores the connection string for a data connection.
    /// </summary>
    public interface IDataConnection : ITMObject
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the provider type.
        /// </summary>
        Type Provider { get; }

        #endregion Data Members
    }
}