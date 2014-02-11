// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Helpers;
using MarcelJoachimKloubert.CloudNET.Classes.IO;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.IO
{
    internal sealed class CloudPrincipalDirectory : FileSystemItemBase, IDirectory
    {
        #region Fields (16)

        private DateTime? _creationTime;
        private DateTime? _lastWrite;
        private const string _MASKED_FILE_EXTENSION = "bin";
        private string _name;
        private const string _PATH_SEPARATOR = "/";
        private const string _XML_ATTRIB_METAFILE_MASKEDNAME = "maskedName";
        private const string _XML_ATTRIB_METAFILE_PASSWORD = "password";
        private const string _XML_ATTRIB_METAFILE_REALNAME = "realName";
        private const string _XML_ATTRIB_METAFILE_SIZE = "size";
        private const string _XML_TAG_METAFILE_DIR = "dir";
        private const string _XML_TAG_METAFILE_DIRLIST = "dirs";
        private const string _XML_TAG_METAFILE_FILE = "file";
        private const string _XML_TAG_METAFILE_FILELIST = "files";
        private const string _XML_TAG_METAFILE_ROOT = "directory";
        internal const string XML_ATTRIB_METAFILE_CREATIONTIME = "creationTime";
        internal const string XML_ATTRIB_METAFILE_LASTWRITETIME = "lastWriteTime";

        #endregion Fields

        #region Properties (12)

        public override DateTime? CreationTime
        {
            get { return this._creationTime; }

            set
            {
                if (value.HasValue)
                {
                    this.Xml
                        .SetAttributeValue(XML_ATTRIB_METAFILE_CREATIONTIME,
                                           value.Value.ToUniversalTime().Ticks);
                }
                else
                {
                    var attrib = this.Xml.Attribute(XML_ATTRIB_METAFILE_CREATIONTIME);
                    if (attrib != null)
                    {
                        attrib.Remove();
                    }
                }

                this.UpdateMetaData(this.Xml.Document);

                this._creationTime = value;
            }
        }

        public bool Exists
        {
            get { return Directory.Exists(this.LocalPath); }
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
                var parts = new List<string>();

                var currentDir = this;
                while (currentDir != null)
                {
                    parts.Insert(0, currentDir.Name);

                    currentDir = currentDir.Parent;
                }

                var result = string.Join(_PATH_SEPARATOR, parts).Trim();

                if (result.StartsWith(_PATH_SEPARATOR) == false)
                {
                    result = _PATH_SEPARATOR + result;
                }

                if (result.EndsWith(_PATH_SEPARATOR) == false)
                {
                    result = result + _PATH_SEPARATOR;
                }

                return result;
            }
        }

        bool IDirectory.IsRoot
        {
            get { return this.IsRoot; }
        }

        IDirectory IDirectory.Parent
        {
            get { return this.Parent; }
        }

        internal bool IsRoot
        {
            get;
            set;
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

        internal CloudPrincipalDirectory Parent
        {
            get;
            set;
        }

        public override DateTime? WriteTime
        {
            get { return this._lastWrite; }

            set
            {
                if (value.HasValue)
                {
                    this.Xml
                        .SetAttributeValue(XML_ATTRIB_METAFILE_LASTWRITETIME,
                                           value.Value.ToUniversalTime().Ticks);
                }
                else
                {
                    var attrib = this.Xml.Attribute(XML_ATTRIB_METAFILE_LASTWRITETIME);
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

        #region Methods (13)

        // Public Methods (6) 

        public IDirectory CreateDirectory(IEnumerable<char> name)
        {
            lock (this.FileManager.SyncRoot)
            {
                var realName = name.AsString();
                if (realName == null)
                {
                    throw new ArgumentNullException("realName");
                }

                realName = realName.Trim();
                if (realName == string.Empty)
                {
                    throw new ArgumentException("realName");
                }

                var deleteMaskDirOnFailure = false;
                var maskedDir = this.GetNextMaskedDirectory();
                try
                {
                    var meta = this.TryGetMetaData();
                    if (meta == null)
                    {
                        // current meta data could not be read or was not found
                        meta = new XDocument(new XDeclaration("1.0", Encoding.UTF8.WebName, "yes"));
                    }

                    var directoryElement = meta.Root;
                    if (directoryElement == null)
                    {
                        // root element does not exist

                        directoryElement = new XElement(_XML_TAG_METAFILE_ROOT);
                        meta.Add(directoryElement);
                    }

                    var dirListElement = directoryElement.Elements(_XML_TAG_METAFILE_DIRLIST).SingleOrDefault();
                    if (dirListElement == null)
                    {
                        // directory list element does not exist

                        dirListElement = new XElement(_XML_TAG_METAFILE_DIRLIST);
                        directoryElement.Add(dirListElement);
                    }

                    // try find existing entry
                    var dirElement = CollectionHelper.SingleOrDefault(dirListElement.Elements(_XML_TAG_METAFILE_DIR),
                                                                      e =>
                                                                      {
                                                                          var realNameAttrib = e.Attribute(_XML_ATTRIB_METAFILE_MASKEDNAME);
                                                                          if (realNameAttrib != null)
                                                                          {
                                                                              return (realNameAttrib.Value ?? string.Empty).ToLower().Trim() ==
                                                                                     realName.ToLower().Trim();
                                                                          }

                                                                          return false;
                                                                      });

                    if (dirElement == null)
                    {
                        // no entry found => create new one

                        maskedDir = Directory.CreateDirectory(maskedDir.FullName);
                        deleteMaskDirOnFailure = true;

                        dirElement = new XElement(_XML_TAG_METAFILE_DIR);
                        dirElement.SetAttributeValue(_XML_ATTRIB_METAFILE_REALNAME, realName);
                        dirElement.SetAttributeValue(_XML_ATTRIB_METAFILE_MASKEDNAME, maskedDir.Name);

                        dirListElement.Add(dirElement);
                    }
                    else
                    {
                        var maskedNameAttrib = dirElement.Attribute(_XML_ATTRIB_METAFILE_MASKEDNAME);
                        if (maskedNameAttrib != null &&
                            string.IsNullOrWhiteSpace(maskedNameAttrib.Value) == false)
                        {
                            maskedDir = new DirectoryInfo(Path.Combine(this.LocalPath,
                                                                       maskedNameAttrib.Value.Trim()));
                        }
                    }

                    // update meta data
                    this.UpdateMetaData(meta);

                    var result = new CloudPrincipalDirectory();
                    result.FileManager = this.FileManager;
                    result.LocalPath = maskedDir.FullName;
                    result.Parent = this;
                    result.Xml = directoryElement;
                    result.SetName(realName);

                    // try update timestamps
                    try
                    {
                        maskedDir.Refresh();

                        var actions = new Action<CloudPrincipalDirectory, DirectoryInfo>[]
                        {
                            (cloudDir, localDir) => cloudDir.CreationTime = localDir.CreationTimeUtc,
                            (cloudDir, localDir) => cloudDir.WriteTime = localDir.LastWriteTimeUtc,
                        };
                        actions.ForAll(ctx => ctx.Item(result, maskedDir),
                                       throwExceptions: false);
                    }
                    catch
                    {
                        // ignore errors here
                    }

                    return result;
                }
                catch
                {
                    if (deleteMaskDirOnFailure)
                    {
                        if (maskedDir != null)
                        {
                            maskedDir.Refresh();
                            if (maskedDir.Exists)
                            {
                                maskedDir.Delete();
                                maskedDir.Refresh();
                            }
                        }
                    }

                    throw;
                }
            }
        }

        public void Delete()
        {
            lock (this._SYNC)
            {
                if (this.IsRoot)
                {
                    throw new InvalidOperationException();
                }

                var doc = this.Xml.Document;
                this.Xml.Remove();

                this.Parent.UpdateMetaData(doc);

                Deltree(new DirectoryInfo(this.LocalPath));
            }
        }

        public IEnumerable<IDirectory> GetDirectories()
        {
            var meta = this.TryGetMetaData();
            if (meta == null)
            {
                yield break;
            }

            var dir = new DirectoryInfo(this.LocalPath);

            foreach (var dirElement in meta.XPathSelectElements("//" + _XML_TAG_METAFILE_ROOT + "/" + _XML_TAG_METAFILE_DIRLIST + "/" + _XML_TAG_METAFILE_DIR))
            {
                var maskedNameAttrib = dirElement.Attribute(_XML_ATTRIB_METAFILE_MASKEDNAME);
                if (maskedNameAttrib == null ||
                    string.IsNullOrWhiteSpace(maskedNameAttrib.Value))
                {
                    continue;
                }

                var maskedName = maskedNameAttrib.Value.Trim();
                string realName;

                var realNameAttrib = dirElement.Attribute(_XML_ATTRIB_METAFILE_REALNAME);
                if (realNameAttrib == null ||
                    string.IsNullOrWhiteSpace(realNameAttrib.Value))
                {
                    realName = maskedName;
                }
                else
                {
                    realName = realNameAttrib.Value.Trim();
                }

                var maskedDir = new DirectoryInfo(Path.Combine(dir.FullName, maskedName));
                if (maskedDir.Exists == false)
                {
                    continue;
                }

                var newDir = new CloudPrincipalDirectory()
                    {
                        FileManager = this.FileManager,
                        IsRoot = false,
                        LocalPath = maskedDir.FullName,
                        Parent = this,
                        Xml = dirElement,
                    };
                newDir.SetName(realName);

                newDir.RefreshTimestamps();

                yield return newDir;
            }
        }

        public IEnumerable<IFile> GetFiles()
        {
            var meta = this.TryGetMetaData();
            if (meta == null)
            {
                yield break;
            }

            var dir = new DirectoryInfo(this.LocalPath);

            foreach (var fileElement in meta.XPathSelectElements("//" + _XML_TAG_METAFILE_ROOT + "/" + _XML_TAG_METAFILE_FILELIST + "/" + _XML_TAG_METAFILE_FILE))
            {
                var maskedNameAttrib = fileElement.Attribute(_XML_ATTRIB_METAFILE_MASKEDNAME);
                if (maskedNameAttrib == null ||
                    string.IsNullOrWhiteSpace(maskedNameAttrib.Value))
                {
                    continue;
                }

                var maskedName = maskedNameAttrib.Value.Trim();
                string realName;

                var realNameAttrib = fileElement.Attribute(_XML_ATTRIB_METAFILE_REALNAME);
                if (realNameAttrib == null ||
                    string.IsNullOrWhiteSpace(realNameAttrib.Value))
                {
                    realName = maskedName;
                }
                else
                {
                    realName = realNameAttrib.Value.Trim();
                }

                var maskedFile = new FileInfo(Path.Combine(dir.FullName, maskedName));
                if (maskedFile.Exists == false)
                {
                    continue;
                }

                SecureString pwd = null;

                var passwordAttrib = fileElement.Attribute(_XML_ATTRIB_METAFILE_PASSWORD);
                if (passwordAttrib != null &&
                    string.IsNullOrWhiteSpace(passwordAttrib.Value) == false)
                {
                    pwd = CryptoHelper.ToSecureString(passwordAttrib.Value);
                    if (pwd != null)
                    {
                        pwd.MakeReadOnly();
                    }
                }

                var newFile = new CloudPrincipalFile()
                    {
                        Directory = this,
                        FileManager = this.FileManager,
                        LocalPath = maskedFile.FullName,
                        Password = pwd,
                        Xml = fileElement,
                    };

                // try get file size
                var sizeAttrib = fileElement.Attribute(_XML_ATTRIB_METAFILE_SIZE);
                if (sizeAttrib != null &&
                    string.IsNullOrWhiteSpace(sizeAttrib.Value) == false)
                {
                    newFile.Size = GlobalConverter.Current
                                                  .ChangeType<long>(sizeAttrib.Value.Trim(),
                                                                    CultureInfo.InvariantCulture);
                }

                newFile.SetName(realName);

                newFile.RefreshTimestamps();

                yield return newFile;
            }
        }

        public override void RefreshTimestamps()
        {
            var creationTimeAttrib = this.Xml.Attribute(XML_ATTRIB_METAFILE_CREATIONTIME);
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

            var writeTimeAttrib = this.Xml.Attribute(XML_ATTRIB_METAFILE_CREATIONTIME);
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

        public IFile SaveFile(IEnumerable<char> name, Stream data)
        {
            lock (this.FileManager.SyncRoot)
            {
                var realName = name.AsString();
                if (realName == null)
                {
                    throw new ArgumentNullException("realName");
                }

                realName = realName.Trim();
                if (realName == string.Empty)
                {
                    throw new ArgumentException("realName");
                }

                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                if (data.CanRead == false)
                {
                    throw new ArgumentException("data");
                }

                var deleteMaskFileOnFailure = false;
                var maskedFile = this.GetNextMaskedFile();
                try
                {
                    var meta = this.TryGetMetaData();
                    if (meta == null)
                    {
                        // current meta data could not be read or was not found
                        meta = new XDocument(new XDeclaration("1.0", Encoding.UTF8.WebName, "yes"));
                    }

                    var directoryElement = meta.Root;
                    if (directoryElement == null)
                    {
                        // root element does not exist

                        directoryElement = new XElement(_XML_TAG_METAFILE_ROOT);
                        meta.Add(directoryElement);
                    }

                    var fileListElement = directoryElement.Elements(_XML_TAG_METAFILE_FILELIST).SingleOrDefault();
                    if (fileListElement == null)
                    {
                        // file list element does not exist

                        fileListElement = new XElement(_XML_TAG_METAFILE_FILELIST);
                        directoryElement.Add(fileListElement);
                    }

                    // try find existing entry
                    var fileElement = CollectionHelper.SingleOrDefault(fileListElement.Elements(_XML_TAG_METAFILE_FILE),
                                                                       e =>
                                                                       {
                                                                           var realNameNameAttrib = e.Attribute(_XML_ATTRIB_METAFILE_REALNAME);
                                                                           if (realNameNameAttrib != null)
                                                                           {
                                                                               return (realNameNameAttrib.Value ?? string.Empty).ToLower().Trim() ==
                                                                                      realName.ToLower().Trim();
                                                                           }

                                                                           return false;
                                                                       });

                    if (fileElement == null)
                    {
                        // no entry found => create new one

                        using (var s = new FileStream(maskedFile.FullName,
                                                      FileMode.CreateNew,
                                                      FileAccess.ReadWrite))
                        { /* create 0 byte file */ }

                        deleteMaskFileOnFailure = true;

                        fileElement = new XElement(_XML_TAG_METAFILE_FILE);
                        fileElement.SetAttributeValue(_XML_ATTRIB_METAFILE_REALNAME, realName);
                        fileElement.SetAttributeValue(_XML_ATTRIB_METAFILE_MASKEDNAME, maskedFile.Name);

                        fileListElement.Add(fileElement);
                    }
                    else
                    {
                        var maskedNameAttrib = fileElement.Attribute(_XML_ATTRIB_METAFILE_MASKEDNAME);
                        if (maskedNameAttrib != null &&
                            string.IsNullOrWhiteSpace(maskedNameAttrib.Value) == false)
                        {
                            maskedFile = new FileInfo(Path.Combine(this.LocalPath,
                                                                   maskedNameAttrib.Value.Trim()));
                        }
                    }

                    Rijndael alg;
                    Rfc2898DeriveBytes pdb;
                    var pwd = new byte[48];
                    try
                    {
                        // create encrypted file
                        using (var fs = new FileStream(maskedFile.FullName,
                                                       FileMode.OpenOrCreate,
                                                       FileAccess.ReadWrite))
                        {
                            // make file empty
                            fs.SetLength(0);

                            // generate password
                            {
                                var rng = new RNGCryptoServiceProvider();
                                rng.GetBytes(pwd);

                                fileElement.SetAttributeValue(_XML_ATTRIB_METAFILE_PASSWORD,
                                                              Convert.ToBase64String(pwd));

                                pdb = new Rfc2898DeriveBytes(pwd,
                                                             CryptoHelper.DEFAULT_CRYTO_SALT,
                                                             CryptoHelper.DEFAULT_ITERATIONS);
                            }

                            alg = Rijndael.Create();
                            alg.Key = pdb.GetBytes(32);
                            alg.IV = pdb.GetBytes(16);

                            using (var cs = new CryptoStream(fs,
                                                             alg.CreateEncryptor(),
                                                             CryptoStreamMode.Write))
                            {
                                data.CopyTo(cs);

                                cs.Flush();
                                cs.Close();
                            }
                        }

                        // file size
                        try
                        {
                            fileElement.SetAttributeValue(_XML_ATTRIB_METAFILE_SIZE,
                                                          data.Length);
                        }
                        catch
                        {
                            // ignore
                        }

                        // update meta file
                        this.UpdateMetaData(meta);

                        var secPwd = CryptoHelper.ToSecureString(fileElement.Attribute(_XML_ATTRIB_METAFILE_PASSWORD).Value);
                        if (secPwd != null)
                        {
                            secPwd.MakeReadOnly();
                        }

                        var result = new CloudPrincipalFile();
                        result.Directory = this;
                        result.FileManager = this.FileManager;
                        result.LocalPath = maskedFile.FullName;
                        result.Password = secPwd;
                        result.Xml = fileElement;
                        result.SetName(realName);

                        // store file size
                        var sizeAttrib = fileElement.Attribute(_XML_ATTRIB_METAFILE_SIZE);
                        if (sizeAttrib != null &&
                            string.IsNullOrWhiteSpace(sizeAttrib.Value) == false)
                        {
                            result.Size = GlobalConverter.Current.ChangeType<long>(sizeAttrib.Value.Trim(),
                                                                                   CultureInfo.InvariantCulture);
                        }

                        // try update timestamps
                        try
                        {
                            maskedFile.Refresh();

                            var actions = new Action<CloudPrincipalFile, FileInfo>[]
                            {
                                (cloudFile, localFile) => cloudFile.CreationTime = localFile.CreationTimeUtc,
                                (cloudFile, localFile) => cloudFile.WriteTime = localFile.LastWriteTimeUtc,
                            };
                            actions.ForAll(ctx => ctx.Item(result, maskedFile),
                                           throwExceptions: false);
                        }
                        catch
                        {
                            // ignore errors here
                        }

                        return result;
                    }
                    finally
                    {
                        alg = null;
                        pwd = null;
                        pdb = null;
                    }
                }
                catch
                {
                    if (deleteMaskFileOnFailure)
                    {
                        if (maskedFile != null)
                        {
                            maskedFile.Refresh();
                            if (maskedFile.Exists)
                            {
                                maskedFile.Delete();
                                maskedFile.Refresh();
                            }
                        }
                    }

                    throw;
                }
            }
        }
        // Private Methods (4) 

        private static void Deltree(DirectoryInfo dir)
        {
            foreach (var subDir in dir.GetDirectories())
            {
                Deltree(subDir);
            }

            foreach (var file in dir.GetFiles())
            {
                file.Delete();
            }

            dir.Delete();
        }

        private DirectoryInfo GetNextMaskedDirectory()
        {
            var dir = new DirectoryInfo(this.LocalPath);

            // find non existing directory
            for (ulong i = 0; i <= ulong.MaxValue; i++)
            {
                var dirToCheck = new DirectoryInfo(Path.Combine(dir.FullName,
                                                                i.ToString()));

                if (dirToCheck.Exists == false &&
                    File.Exists(dirToCheck.FullName) == false)
                {
                    return dirToCheck;
                }
            }

            return null;
        }

        private FileInfo GetNextMaskedFile()
        {
            var dir = new DirectoryInfo(this.LocalPath);

            // find non existing file
            for (ulong i = 1; i <= ulong.MaxValue; i++)
            {
                var fileToCheck = new FileInfo(Path.Combine(dir.FullName,
                                                            i + "." + _MASKED_FILE_EXTENSION));

                if (fileToCheck.Exists == false)
                {
                    return fileToCheck;
                }
            }

            return null;
        }

        private XDocument TryGetMetaData()
        {
            try
            {
                var metaFile = this.GetMetaFile();
                if (metaFile.Exists)
                {
                    using (var stream = new CryptoHelper().GetDecryptionStream(metaFile.OpenRead(),
                                                                               this.FileManager.Password))
                    {
                        return XDocument.Load(stream);
                    }
                }
            }
            catch
            {
                // ignore here
            }

            return null;
        }
        // Internal Methods (3) 

        internal FileInfo GetMetaFile()
        {
            return new FileInfo(Path.Combine(this.LocalPath, "0." + _MASKED_FILE_EXTENSION));
        }

        internal void SetName(IEnumerable<char> name)
        {
            this._name = name.AsString();
        }

        internal void UpdateMetaData(XDocument xmlDoc)
        {
            var deleteBackupFile = false;
            var metaFile = this.GetMetaFile();
            var metaFileBackup = new FileInfo(metaFile.FullName + ".bak");

            try
            {
                if (metaFile.Exists)
                {
                    // create backup of old file

                    File.Copy(metaFile.FullName,
                              metaFileBackup.FullName,
                              true);

                    metaFile.Delete();
                    metaFile.Refresh();
                }

                using (var stream = new CryptoHelper().GetEncryptionStream(new FileStream(metaFile.FullName,
                                                                                          FileMode.CreateNew,
                                                                                          FileAccess.ReadWrite),
                                                                           this.FileManager.Password))
                {
                    if (xmlDoc != null)
                    {
                        xmlDoc.Save(stream);
                    }

                    stream.Flush();
                    stream.Close();
                }

                deleteBackupFile = true;
            }
            catch
            {
                metaFileBackup.Refresh();
                if (metaFileBackup.Exists)
                {
                    // try restore backup
                    File.Copy(metaFileBackup.FullName,
                              metaFile.FullName,
                              true);

                    // succeeded, so delete backup file
                    deleteBackupFile = true;
                }

                throw;
            }
            finally
            {
                if (deleteBackupFile)
                {
                    metaFileBackup.Refresh();
                    if (metaFileBackup.Exists)
                    {
                        metaFileBackup.Delete();
                    }
                }
            }
        }

        #endregion Methods
    }
}
