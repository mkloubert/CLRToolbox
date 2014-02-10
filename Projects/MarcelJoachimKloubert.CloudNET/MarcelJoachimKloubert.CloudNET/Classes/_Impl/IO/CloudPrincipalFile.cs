// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Helpers;
using MarcelJoachimKloubert.CloudNET.Classes.IO;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.IO
{
    internal sealed class CloudPrincipalFile : FileSystemItemBase, IFile
    {
        #region Fields (1)

        private string _name;

        #endregion Fields

        #region Properties (10)

        internal CloudPrincipalDirectory Directory
        {
            get;
            set;
        }

        public bool Exists
        {
            get { return File.Exists(this.LocalPath); }
        }

        internal CloudPrincipalFileManager FileManager
        {
            get;
            set;
        }

        public override string FullPath
        {
            get
            {
                var dirPath = this.Directory.FullPath;
                if (string.IsNullOrWhiteSpace(dirPath) == false)
                {
                    return dirPath.Trim() + this.Name;
                }

                return this.Name;
            }
        }

        IDirectory IFile.Directory
        {
            get { return this.Directory; }
        }

        internal string LocalPath
        {
            get;
            set;
        }

        public override string Name
        {
            get { return this._name; }
        }

        internal SecureString Password
        {
            get;
            set;
        }

        public long Size
        {
            get;
            internal set;
        }

        internal XElement Xml
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (2) 

        public void Delete()
        {
            File.Delete(this.LocalPath);

            var doc = this.Xml.Document;
            this.Xml.Remove();

            this.UpdateMetaData(doc);
        }

        public Stream OpenRead()
        {
            return new CryptoHelper().GetDecryptionStream(File.OpenRead(this.LocalPath),
                                                          this.Password);
        }
        // Private Methods (1) 

        private void UpdateMetaData(XDocument xmlDoc)
        {
            this.Directory.UpdateMetaData(xmlDoc);
        }
        // Internal Methods (1) 

        internal void SetName(IEnumerable<char> name)
        {
            this._name = name.AsString();
        }

        #endregion Methods
    }
}
