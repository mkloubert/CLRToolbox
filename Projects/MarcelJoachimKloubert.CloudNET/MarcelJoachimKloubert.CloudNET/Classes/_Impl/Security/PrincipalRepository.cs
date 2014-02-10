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

        #region Properties (2)

        internal string LocalDataDirectory
        {
            get;
            set;
        }

        internal IConfigRepository UserRepository
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (3)

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

            var matchingUsers = new List<ICloudPrincipal>();

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

                    string iniUser;
                    if (userRepo.TryGetValue<string>("name", out iniUser, category) == false)
                    {
                        // no user name defined
                        continue;
                    }

                    // get MD5 hased password
                    string iniPwd;
                    userRepo.TryGetValue<string>("password", out iniPwd, category);

                    ParseUsernameAndPassword(ref iniUser, ref iniPwd);

                    if (iniPwd != null)
                    {
                        iniPwd = iniPwd.ToLower().Trim();
                    }

                    if ((user == iniUser) && (md5Pwd == iniPwd))
                    {
                        // found

                        var id = new CloudIdentity();
                        id.AuthenticationType = "Basic";
                        id.IsAuthenticated = true;
                        id.Name = iniUser;

                        var acl = new SimpleAcl();

                        var rootDir = new DirectoryInfo(Path.Combine(this.LocalDataDirectory, user));
                        if (rootDir.Exists == false)
                        {
                            // no root directory

                            rootDir.Create();
                            rootDir.Refresh();
                        }

                        SecureString cryptFilePwd = null;
                        if (pwd != null)
                        {
                            cryptFilePwd = new SecureString();
                            cryptFilePwd.Append(pwd);
                            cryptFilePwd.MakeReadOnly();
                        }

                        SecureString secPwd = null;
                        var createNewCryptFile = false;

                        // read real password from file
                        var cryptFile = new FileInfo(Path.Combine(this.LocalDataDirectory, user + ".crypt"));
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

                            Rijndael alg;
                            Rfc2898DeriveBytes pdb;
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
                                alg = null;
                                pdb = null;
                                pwdBuffer = null;
                            }
                        }

                        var princ = new CloudPrincipal(id, acl);
                        princ.Files = new CloudPrincipalFileManager()
                            {
                                LocalRootDirectory = rootDir.FullName,
                                Password = secPwd,
                                Principal = princ,
                            };

                        matchingUsers.Add(princ);
                    }
                }
            }

            return matchingUsers.SingleOrDefault();
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

        #endregion Methods
    }
}
