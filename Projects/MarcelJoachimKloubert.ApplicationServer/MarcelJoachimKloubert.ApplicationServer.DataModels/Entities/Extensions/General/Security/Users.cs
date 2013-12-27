// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security
{
    partial class Users
    {
        #region Properties (1)

        /// <summary>
        /// Gets or sets the logic for generating hash from first parameter of <see cref="Users.CheckPassword(IEnumerable{char})" />.
        /// </summary>
        public Func<string, IEnumerable<byte>> CharacterPasswordHasher
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Validates a password string though <see cref="Users.Password" />
        /// by using logic from <see cref="Users.CharacterPasswordHasher" />.
        /// </summary>
        /// <param name="pwd">The password to check.</param>
        /// <returns>Is valid or not.</returns>
        public bool CheckPassword(IEnumerable<char> pwd)
        {
            var hashFunc = this.CharacterPasswordHasher;

            var hash = hashFunc(pwd.AsString()).AsArray();
            var usrPwdHash = this.Password;

            return (hash == null && usrPwdHash == null) ||
                   (hash != null && usrPwdHash != null && CollectionHelper.SequenceEqual(hash, usrPwdHash));
        }

        #endregion Methods
    }
}
