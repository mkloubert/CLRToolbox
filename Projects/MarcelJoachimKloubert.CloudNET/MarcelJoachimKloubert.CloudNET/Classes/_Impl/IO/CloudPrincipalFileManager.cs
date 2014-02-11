// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes._Impl.Security;
using MarcelJoachimKloubert.CloudNET.Classes.IO;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.IO
{
    internal sealed class CloudPrincipalFileManager : TMObject, IFileManager
    {
        #region Fields (1)

        private const string _PATH_SEPERATOR_EXPR = "/";

        #endregion Fields

        #region Constructors (1)

        ~CloudPrincipalFileManager()
        {
            this.Dispose(false);
        }

        #endregion Constructors

        #region Properties (4)

        internal string LocalRootDirectory
        {
            get;
            set;
        }

        internal SecureString Password
        {
            get;
            set;
        }

        internal CloudPrincipal Principal
        {
            get;
            set;
        }

        public object SyncRoot
        {
            get { return this._SYNC; }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (3) 

        public IDirectory GetDirectory(IEnumerable<char> path, bool createIfNotExist = false)
        {
            var dirPath = path.AsString();
            if (dirPath != null)
            {
                // remove beginning '/' chars
                while (dirPath.StartsWith(_PATH_SEPERATOR_EXPR))
                {
                    dirPath = dirPath.Substring(1)
                                     .Trim();
                }

                // remove encoding '/' chars
                while (dirPath.EndsWith(_PATH_SEPERATOR_EXPR))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1)
                                     .Trim();
                }
            }

            if (string.IsNullOrWhiteSpace(dirPath))
            {
                return this.GetRootDirectory();
            }

            var parts = dirPath.Split(new string[] { _PATH_SEPERATOR_EXPR },
                                      StringSplitOptions.None);

            IDirectory currentDir = this.GetRootDirectory();
            for (var i = 0; i < parts.Length; i++)
            {
                var p = parts[i].ToLower().Trim();

                var subDir = CollectionHelper.SingleOrDefault(currentDir.GetDirectories(),
                                                              d => p == (d.Name ?? string.Empty).ToLower().Trim());

                if (subDir == null)
                {
                    if (createIfNotExist)
                    {
                        subDir = currentDir.CreateDirectory(p);
                    }
                }

                currentDir = subDir;
                if (currentDir == null)
                {
                    // part does not exist
                    break;
                }
            }

            return currentDir;
        }

        public IFile GetFile(IEnumerable<char> path, bool createDirIfNotExist = false)
        {
            var filePath = path.AsString();
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            // remove beginning '/' chars
            while (filePath.StartsWith(_PATH_SEPERATOR_EXPR))
            {
                filePath = filePath.Substring(1)
                                   .Trim();
            }

            filePath = filePath.Trim();
            if (filePath == null)
            {
                throw new ArgumentException("filePath");
            }

            var parts = filePath.Split(new string[] { _PATH_SEPERATOR_EXPR },
                                       StringSplitOptions.None);

            // find directory
            IDirectory currentDir = this.GetRootDirectory();
            for (var i = 0; i < (parts.Length - 1); i++)
            {
                var p = parts[i].ToLower().Trim();

                currentDir = CollectionHelper.SingleOrDefault(currentDir.GetDirectories(),
                                                              d => p == (d.Name ?? string.Empty).ToLower().Trim());

                if (currentDir == null)
                {
                    // part does not exist
                    break;
                }
            }

            if (currentDir == null)
            {
                if (createDirIfNotExist)
                {
                    currentDir = this.GetDirectory(string.Join(_PATH_SEPERATOR_EXPR,
                                                               parts.Take(parts.Length - 1)),
                                                   true);
                }
            }

            IFile result = null;
            if (currentDir != null)
            {
                // find file...

                var p = parts.LastOrDefault();
                if (p != null)
                {
                    p = p.ToLower().Trim();

                    result = CollectionHelper.SingleOrDefault(currentDir.GetFiles(),
                                                              f => p == (f.Name ?? string.Empty).ToLower().Trim());
                }
            }

            return result;
        }

        public IDirectory GetRootDirectory()
        {
            var dir = new DirectoryInfo(this.LocalRootDirectory);

            return new CloudPrincipalDirectory()
                {
                    FileManager = this,
                    IsRoot = true,
                    LocalPath = dir.FullName,
                };
        }
        // Private Methods (1) 

        private void Dispose(bool disposing)
        {
            lock (this._SYNC)
            {
                using (var pwd = this.Password)
                { }
            }
        }

        #endregion Methods
    }
}
