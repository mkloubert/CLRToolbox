// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Helpers;
using MarcelJoachimKloubert.CloudNET.Classes.IO;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.IO
{
    internal sealed class CloudPrincipalFile : FileSystemItemBase, IFile
    {
        #region Fields (3)

        private DateTime? _creationTime;
        private DateTime? _lastWrite;
        private string _name;

        #endregion Fields

        #region Constructors (1)

        ~CloudPrincipalFile()
        {
            this.Dispose(false);
        }

        #endregion Constructors

        #region Properties (12)

        public override DateTime? CreationTime
        {
            get { return this._creationTime; }

            set
            {
                if (value.HasValue)
                {
                    this.Xml
                        .SetAttributeValue(CloudPrincipalDirectory.XML_ATTRIB_METAFILE_CREATIONTIME,
                                           TimeHelper.NormalizeValue(value).Value.ToUniversalTime().Ticks);
                }
                else
                {
                    var attrib = this.Xml.Attribute(CloudPrincipalDirectory.XML_ATTRIB_METAFILE_CREATIONTIME);
                    if (attrib != null)
                    {
                        attrib.Remove();
                    }
                }

                this.UpdateMetaData(this.Xml.Document);

                this._creationTime = value;
            }
        }

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

        public long? Size
        {
            get;
            internal set;
        }

        public override DateTime? WriteTime
        {
            get { return this._lastWrite; }

            set
            {
                if (value.HasValue)
                {
                    this.Xml
                        .SetAttributeValue(CloudPrincipalDirectory.XML_ATTRIB_METAFILE_LASTWRITETIME,
                                           TimeHelper.NormalizeValue(value).Value.ToUniversalTime().Ticks);
                }
                else
                {
                    var attrib = this.Xml.Attribute(CloudPrincipalDirectory.XML_ATTRIB_METAFILE_LASTWRITETIME);
                    if (attrib != null)
                    {
                        attrib.Remove();
                    }
                }

                this.UpdateMetaData(this.Xml.Document);

                this._lastWrite = value;
            }
        }

        internal XElement Xml
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (6)

        // Public Methods (3) 

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

        public override void RefreshTimestamps()
        {
            var creationTimeAttrib = this.Xml.Attribute(CloudPrincipalDirectory.XML_ATTRIB_METAFILE_CREATIONTIME);
            if (creationTimeAttrib != null &&
                string.IsNullOrWhiteSpace(creationTimeAttrib.Value) == false)
            {
                var ticks = GlobalConverter.Current.ChangeType<long>(creationTimeAttrib.Value.Trim(),
                                                                     CultureInfo.InvariantCulture);

                this._creationTime = new DateTime(ticks, DateTimeKind.Utc);
            }
            else
            {
                this._creationTime = null;
            }

            var writeTimeAttrib = this.Xml.Attribute(CloudPrincipalDirectory.XML_ATTRIB_METAFILE_CREATIONTIME);
            if (writeTimeAttrib != null &&
                string.IsNullOrWhiteSpace(writeTimeAttrib.Value) == false)
            {
                var ticks = GlobalConverter.Current.ChangeType<long>(writeTimeAttrib.Value.Trim(),
                                                                     CultureInfo.InvariantCulture);

                this._lastWrite = new DateTime(ticks, DateTimeKind.Utc);
            }
            else
            {
                this._lastWrite = null;
            }
        }
        // Private Methods (2) 

        private void Dispose(bool disposing)
        {
            lock (this._SYNC)
            {
                using (var pwd = this.Password)
                { }
            }
        }

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
