// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes._Impl.IO;
using MarcelJoachimKloubert.CloudNET.Classes.Helpers;
using MarcelJoachimKloubert.CloudNET.Classes.Security;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.Security
{
    internal sealed class PrincipalRepository : TMObject, IPrincipalRepository
    {
        #region Fields (1)

        private static readonly byte[] _CRYTO_SALT = new byte[] { 5, 9, 23, 9, 19, 79 };

        #endregion Fields

        #region Properties (3)

        internal string LocalDataDirectory
        {
            get;
            set;
        }

        internal IList<CloudPrincipal> PrincipalTemplates
        {
            get;
            private set;
        }

        internal IConfigRepository UserRepository
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (1) 

        public ICloudPrincipal TryFindPrincipalByLogin(IEnumerable<char> username, IEnumerable<char> password)
        {
            var user = username.AsString();
            var pwd = password.AsString();
            ParseUsernameAndPassword(ref user, ref pwd);

            string md5Pwd = null;
            if (pwd != null)
            {
                // convert to MD5 hash

                using (var md5 = new MD5CryptoServiceProvider())
                {
                    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));

                    md5Pwd = StringHelper.AsHexString(hash);
                }
            }

            var templates = this.PrincipalTemplates;
            if (templates == null)
            {
                return null;
            }

            SecureString cryptFilePwd = null;
            try
            {
                if (pwd != null)
                {
                    cryptFilePwd = new SecureString();
                    cryptFilePwd.Append(pwd);
                    cryptFilePwd.MakeReadOnly();
                }

                return templates.Where(tpl => tpl.Identity.Name == user &&
                                              ((CloudIdentity)tpl.Identity).Password.ToUnsecureString() == md5Pwd)
                                .Select(tpl => new
                                    {
                                        CryptFilePassword = cryptFilePwd,
                                        DataDirectory = this.LocalDataDirectory,
                                        Template = tpl,
                                        Username = user,
                                    })
                                .Select(x =>
                                    {
                                        var tplClone = x.Template.Clone(markAsAuthenticated: true);

                                        var fileManager = tplClone.Files;
                                        lock (fileManager)
                                        {


                                            SecureString secPwd = null;
                                            var createNewCryptFile = false;

                                            // read real password from file
                                            var cryptFile = new FileInfo(Path.Combine(x.DataDirectory, x.Username + ".crypt"));
                                            if (cryptFile.Exists == false)
                                            {
                                                // does not exist => create new one
                                                createNewCryptFile = true;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    secPwd = LoadPasswordFromFile(cryptFile, cryptFilePwd);
                                                }
                                                catch
                                                {
                                                    // reading password file failed => create new one
                                                    createNewCryptFile = true;
                                                }
                                            }

                                            if (createNewCryptFile)
                                            {
                                                // create password file

                                                cryptFile.Refresh();
                                                if (cryptFile.Exists)
                                                {
                                                    // delete existing one

                                                    cryptFile.Delete();
                                                    cryptFile.Refresh();
                                                }

                                                var pwdBuffer = new byte[48];
                                                try
                                                {
                                                    var rng = new RNGCryptoServiceProvider();
                                                    rng.GetBytes(pwdBuffer);

                                                    // password to file file
                                                    using (var stream = new CryptoHelper().GetEncryptionStream(new FileStream(cryptFile.FullName,
                                                                                                                              FileMode.CreateNew,
                                                                                                                              FileAccess.ReadWrite),
                                                                                                               cryptFilePwd))
                                                    {
                                                        stream.Write(pwdBuffer, 0, pwdBuffer.Length);

                                                        stream.Flush();
                                                        stream.Close();
                                                    }

                                                    secPwd = LoadPasswordFromFile(cryptFile, cryptFilePwd);
                                                }
                                                finally
                                                {
                                                    pwdBuffer = null;
                                                }
                                            }

                                            fileManager.Password = secPwd;
                                        }

                                        return tplClone;
                                    })
                                .SingleOrDefault();
            }
            finally
            {
                using (var cpwd = cryptFilePwd)
                { }
            }
        }
        // Private Methods (2) 

        private static SecureString LoadPasswordFromFile(FileInfo cryptFile, SecureString cryptFilePwd)
        {
            using (var temp = new MemoryStream())
            {
                using (var fs = cryptFile.OpenRead())
                {
                    using (var stream = new CryptoHelper().GetDecryptionStream(fs,
                                                                               cryptFilePwd))
                    {
                        stream.CopyTo(temp);
                    }
                }

                var result = new SecureString();
                result.Append(Convert.ToBase64String(temp.ToArray()));
                result.MakeReadOnly();

                return result;
            }
        }

        private static void ParseUsernameAndPassword(ref string user, ref string pwd)
        {
            user = (user ?? string.Empty).ToLower().Trim();
            if (user == string.Empty)
            {
                user = null;
            }

            if (string.IsNullOrEmpty(pwd))
            {
                pwd = null;
            }
        }
        // Internal Methods (1) 

        internal void Reload()
        {
            IList<CloudPrincipal> loadedPrincipals = new SynchronizedCollection<CloudPrincipal>();

            var userRepo = this.UserRepository;
            if (userRepo != null)
            {
                foreach (var category in userRepo.GetCategoryNames())
                {
                    if (string.IsNullOrWhiteSpace(category))
                    {
                        continue;
                    }

                    if (category.ToLower().Trim().StartsWith("user") == false)
                    {
                        continue;
                    }

                    string user;
                    if (userRepo.TryGetValue<string>("name", out user, category) == false)
                    {
                        // no user name defined
                        continue;
                    }

                    // get MD5 hased password
                    string hashedPwd;
                    userRepo.TryGetValue<string>("password", out hashedPwd, category);

                    ParseUsernameAndPassword(ref user, ref hashedPwd);

                    SecureString secHashedPwd = null;
                    if (hashedPwd != null)
                    {
                        hashedPwd = hashedPwd.ToLower().Trim();

                        secHashedPwd = new SecureString();
                        secHashedPwd.Append(hashedPwd);
                        secHashedPwd.MakeReadOnly();
                    }

                    var id = new CloudIdentity();
                    id.AuthenticationType = "Basic";
                    id.IsAuthenticated = true;
                    id.Name = user;
                    id.Password = secHashedPwd;

                    var acl = new SimpleAcl();

                    var rootDir = new DirectoryInfo(Path.Combine(this.LocalDataDirectory, user));
                    if (rootDir.Exists == false)
                    {
                        // no root directory

                        rootDir.Create();
                        rootDir.Refresh();
                    }

                    var princ = new CloudPrincipal(id, acl);
                    princ.Files = new CloudPrincipalFileManager()
                        {
                            LocalRootDirectory = rootDir.FullName,
                        };

                    loadedPrincipals.Add(princ);
                }
            }

            this.PrincipalTemplates = loadedPrincipals;
        }

        #endregion Methods
    }
}
