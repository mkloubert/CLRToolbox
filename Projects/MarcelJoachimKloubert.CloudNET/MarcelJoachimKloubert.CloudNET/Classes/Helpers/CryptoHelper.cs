// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace MarcelJoachimKloubert.CloudNET.Classes.Helpers
{
    /// <summary>
    /// Helper class for crypto operations.
    /// </summary>
    public sealed class CryptoHelper
    {
        #region Fields (2)

        /// <summary>
        /// Stores the default crypto salt.
        /// </summary>
        public static readonly byte[] DEFAULT_CRYTO_SALT = new byte[] { 0, 5, 9, 23, 9, 19, 79, 0 };
        /// <summary>
        /// Stores the default number of iterations.
        /// </summary>
        public const int DEFAULT_ITERATIONS = 1000;

        #endregion Fields

        #region Properties (2)

        /// <summary>
        /// Gets or sets the number of iterations.
        /// </summary>
        public int? Iterations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the crypto salt.
        /// </summary>
        public byte[] Salt
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Gets a <see cref="Stream" /> for a base stream that can be used for decryption operations.
        /// </summary>
        /// <param name="stream">The base stream.</param>
        /// <param name="pwd">The optional password.</param>
        /// <returns>
        /// The decryption stream or <see langword="null" /> if <paramref name="stream" /> is also <see langword="null" />.
        /// </returns>
        public Stream GetDecryptionStream(Stream stream, SecureString pwd)
        {
            if (stream == null)
            {
                return null;
            }

            var result = stream;

            if (pwd != null)
            {
                Rijndael alg = null;
                Rfc2898DeriveBytes pdb = null;
                try
                {
                    pdb = new Rfc2898DeriveBytes(Convert.FromBase64String(pwd.ToUnsecureString()),
                                                 this.Salt ?? DEFAULT_CRYTO_SALT,
                                                 this.Iterations ?? DEFAULT_ITERATIONS);

                    alg = Rijndael.Create();
                    alg.Key = pdb.GetBytes(32);
                    alg.IV = pdb.GetBytes(16);

                    result = new CryptoStream(stream,
                                              alg.CreateDecryptor(),
                                              CryptoStreamMode.Read);
                }
                finally
                {
                    alg = null;
                    pdb = null;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a <see cref="Stream" /> for a base stream that can be used for encryption operations.
        /// </summary>
        /// <param name="stream">The base stream.</param>
        /// <param name="pwd">The optional password.</param>
        /// <returns>
        /// The encryption stream or <see langword="null" /> if <paramref name="stream" /> is also <see langword="null" />.
        /// </returns>
        public Stream GetEncryptionStream(Stream stream, SecureString pwd)
        {
            if (stream == null)
            {
                return null;
            }

            var result = stream;

            if (pwd != null)
            {
                Rijndael alg = null;
                Rfc2898DeriveBytes pdb = null;
                try
                {
                    pdb = new Rfc2898DeriveBytes(Convert.FromBase64String(pwd.ToUnsecureString()),
                                                 this.Salt ?? DEFAULT_CRYTO_SALT,
                                                 this.Iterations ?? DEFAULT_ITERATIONS);

                    alg = Rijndael.Create();
                    alg.Key = pdb.GetBytes(32);
                    alg.IV = pdb.GetBytes(16);

                    result = new CryptoStream(stream,
                                              alg.CreateEncryptor(),
                                              CryptoStreamMode.Write);
                }
                finally
                {
                    alg = null;
                    pdb = null;
                }
            }

            return result;
        }

        #endregion Methods
    }
}
