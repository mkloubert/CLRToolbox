// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CryptCommander.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO.Local
{
    /// <summary>
    /// A local directory.
    /// </summary>
    public sealed class LocalDirectory : LocalFileSystemObjectBase, IDirectory
    {
        #region Fields (1)

        private readonly DirectoryInfo _DIR;

        #endregion Fields

        #region Constructors (1)

        internal LocalDirectory(LocalDrive drive, DirectoryInfo dir,
                                LocalDirectory parent = null)
        {
            this.Drive = drive;
            this.Parent = parent;

            this._DIR = dir;
        }

        #endregion Constructors

        #region Properties (7)

        /// <inheriteddoc />
        public LocalDrive Drive
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public string FullPath
        {
            get { return this._DIR.FullName; }
        }

        IDrive IDirectory.Drive
        {
            get { return this.Drive; }
        }

        IDirectory IDirectory.Parent
        {
            get { return this.Parent; }
        }

        /// <inheriteddoc />
        public DateTime LastWriteTime
        {
            get { return this._DIR.LastWriteTimeUtc; }
        }

        /// <inheriteddoc />
        public override string Name
        {
            get { return this._DIR.Name; }
        }

        /// <inheriteddoc />
        public LocalDirectory Parent
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (6)

        // Public Methods (4) 

        /// <inheriteddoc />
        public IEnumerable<LocalDirectory> GetDirectories()
        {
            foreach (var dir in this._DIR.GetDirectories())
            {
                yield return new LocalDirectory(drive: this.Drive,
                                                dir: dir,
                                                parent: this);
            }
        }

        /// <inheriteddoc />
        public IEnumerable<LocalFile> GetFiles()
        {
            foreach (var file in this._DIR.GetFiles())
            {
                yield return new LocalFile(dir: this,
                                           file: file);
            }
        }

        /// <inheriteddoc />
        public Image GetIcon()
        {
            return Resources.icon_directory.Clone() as Image;
        }

        /// <inheriteddoc />
        public bool Refresh()
        {
            try
            {
                this._DIR.Refresh();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // Private Methods (2) 

        IEnumerable<IDirectory> IDirectory.GetDirectories()
        {
            return this.GetDirectories();
        }

        IEnumerable<IFile> IDirectory.GetFiles()
        {
            return this.GetFiles();
        }

        #endregion Methods
    }
}
