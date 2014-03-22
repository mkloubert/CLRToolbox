// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CryptCommander.Properties;
using System;
using System.Drawing;
using System.IO;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO.Local
{
    /// <summary>
    /// A local file.
    /// </summary>
    public sealed class LocalFile : LocalFileSystemObjectBase, IFile
    {
        #region Fields (1)

        private readonly FileInfo _FILE;

        #endregion Fields

        #region Constructors (1)

        internal LocalFile(LocalDirectory dir, FileInfo file)
        {
            this.Directory = dir;
            this._FILE = file;
        }

        #endregion Constructors

        #region Properties (6)

        /// <inheriteddoc />
        public LocalDirectory Directory
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public string FullPath
        {
            get { return this._FILE.FullName; }
        }

        IDirectory IFile.Directory
        {
            get { return this.Directory; }
        }

        /// <inheriteddoc />
        public DateTime LastWriteTime
        {
            get { return this._FILE.LastWriteTimeUtc; }
        }

        /// <inheriteddoc />
        public override string Name
        {
            get { return this._FILE.Name; }
        }

        /// <inheriteddoc />
        public long Size
        {
            get { return this._FILE.Length; }
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (2) 

        /// <inheriteddoc />
        public Image GetIcon()
        {
            return Resources.icon_file.Clone() as Image;
        }

        /// <inheriteddoc />
        public bool Refresh()
        {
            try
            {
                this._FILE.Refresh();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Methods
    }
}
