// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System;
using System.Globalization;

namespace MarcelJoachimKloubert.CloudNET.Classes.IO
{
    /// <summary>
    /// A base object for a file system element.
    /// </summary>
    public abstract class FileSystemItemBase : TMObject, IFileSystemItem
    {
        #region Constructors (2)

        /// <inheriteddoc />
        protected FileSystemItemBase(object sync)
            : base(sync)
        {

        }

        /// <inheriteddoc />
        protected FileSystemItemBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (5)

        /// <inheriteddoc />
        public abstract DateTime? CreationTime
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentCulture); }
        }

        /// <inheriteddoc />
        public abstract string FullPath
        {
            get;
        }

        /// <inheriteddoc />
        public abstract string Name
        {
            get;
        }

        /// <inheriteddoc />
        public abstract DateTime? WriteTime
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (3) 

        /// <inheriteddoc />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return this.Name;
        }

        /// <summary>
        /// Updates the properties that store the timestamp values.
        /// </summary>
        public abstract void RefreshTimestamps();

        /// <inheriteddoc />
        public override string ToString()
        {
            return this.Name ?? string.Empty;
        }

        #endregion Methods
    }
}
