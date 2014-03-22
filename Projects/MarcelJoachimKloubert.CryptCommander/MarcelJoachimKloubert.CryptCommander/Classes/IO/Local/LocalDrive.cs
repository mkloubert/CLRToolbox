// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO.Local
{
    /// <summary>
    /// A local drive.
    /// </summary>
    public sealed class LocalDrive : LocalFileSystemObjectBase, IDrive
    {
        #region Fields (1)

        private readonly DriveInfo _DRIVE;

        #endregion Fields

        #region Constructors (1)

        internal LocalDrive(LocalFileSystem fs, DriveInfo di)
        {
            this.FileSystem = fs;
            this._DRIVE = di;
        }

        #endregion Constructors

        #region Properties (3)

        /// <inheriteddoc />
        public LocalFileSystem FileSystem
        {
            get;
            private set;
        }

        IFileSystem IDrive.FileSystem
        {
            get { return this.FileSystem; }
        }

        /// <inheriteddoc />
        public override string Name
        {
            get { return this._DRIVE.Name; }
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (1) 

        /// <inheriteddoc />
        public LocalDirectory GetRootDirectory()
        {
            try
            {
                return new LocalDirectory(drive: this,
                                          dir: this._DRIVE.RootDirectory);
            }
            catch
            {
                return null;
            }
        }
        // Private Methods (1) 

        IDirectory IDrive.GetRootDirectory()
        {
            return this.GetRootDirectory();
        }

        #endregion Methods
    }
}
