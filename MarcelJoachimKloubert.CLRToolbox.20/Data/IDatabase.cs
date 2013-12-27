// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Describes a database (connection).
    /// </summary>
    public interface IDatabase : ITMDisposable
    {
        #region Data Members (1)

        /// <summary>
        /// Gets if that database can be updated or not.
        /// </summary>
        bool CanUpdate { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Updates changes.
        /// </summary>
        /// <exception cref="InvalidOperationException">Database cannot be updated.</exception>
        void Update();

        #endregion Operations
    }
}
