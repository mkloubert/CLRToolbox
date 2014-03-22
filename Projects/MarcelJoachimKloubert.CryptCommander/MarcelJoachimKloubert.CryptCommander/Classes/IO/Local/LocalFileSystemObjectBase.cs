// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System;
using System.Globalization;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO.Local
{
    /// <summary>
    /// A basic object for handing local file system.
    /// </summary>
    public abstract class LocalFileSystemObjectBase : TMObject, IHasName
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalFileSystemObjectBase"/> class.
        /// </summary>
        protected LocalFileSystemObjectBase()
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <inheriteddoc />
        public string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentUICulture); }
        }

        /// <inheriteddoc />
        public abstract string Name
        {
            get;
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (2) 

        /// <inheriteddoc />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return this.Name;
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return this.DisplayName ?? string.Empty;
        }

        #endregion Methods
    }
}
