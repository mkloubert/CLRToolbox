// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using MarcelJoachimKloubert.ApplicationServer.Security.Cryptography;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Security.Cryptography;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.ApplicationServer.Services.Security.Cryptography
{
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.Security.Cryptography.IPasswordHasher))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class PasswordHasher : HasherBase, IPasswordHasher
    {
        #region Fields (1)

        private readonly IPasswordHasher _INNER_HASHER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal PasswordHasher(AppServerImpl server)
        {
            byte[] pwdSalt = null;
            {
                IEnumerable<char> salt;
                server.StartupConfig.TryGetValue(category: "security",
                                                 name: "password_salt",
                                                 value: out salt,
                                                 defaultVal: null);

                var strSalt = salt.AsString();
                if (!string.IsNullOrEmpty(strSalt))
                {
                    pwdSalt = Encoding.UTF8.GetBytes(strSalt);
                }
            }

            this._INNER_HASHER = new GeneralPasswordHasher(pwdSalt);
        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnHash(byte[] data, Stream targetStream)
        {
            var hash = this._INNER_HASHER.Hash(data);

            targetStream.Write(hash, 0, hash.Length);
        }

        #endregion Methods
    }
}
