// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Security.Cryptography.Passwords;
using MarcelJoachimKloubert.MetalVZ.Classes.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Security.Cryptography;

namespace MarcelJoachimKloubert.MetalVZ.Classes._Impl.Security.Cryptography
{
    [Export(typeof(global::MarcelJoachimKloubert.MetalVZ.Classes.Security.Cryptography.IMVZPasswordHasher))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class MVZPasswordHasher : GeneralPasswordHasher, IMVZPasswordHasher
    {
        #region Constructors (1)

        [ImportingConstructor]
        internal MVZPasswordHasher(IConfigRepository repo)
            : base(algo: new MD5CryptoServiceProvider(),
                   salt: GetSalt(repo))
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Private Methods (1) 

        private static IEnumerable<byte> GetSalt(IConfigRepository repo)
        {
            string base64Salt;
            repo.TryGetValue<string>("password_salt", out base64Salt, "security");

            if (string.IsNullOrWhiteSpace(base64Salt))
            {
                return null;
            }

            return Convert.FromBase64String(base64Salt.Trim());
        }

        #endregion Methods
    }
}
