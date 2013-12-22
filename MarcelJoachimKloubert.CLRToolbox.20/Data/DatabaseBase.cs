// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// A basic database connection.
    /// </summary>
    public abstract class DatabaseBase : DisposableBase,
                                         IDatabase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected DatabaseBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseBase" /> class.
        /// </summary>
        protected DatabaseBase()
            : base()
        {

        }

        #endregion Constructors
    }
}
