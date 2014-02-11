// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Collections.Generic;
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

        #region Methods (6)

        // Public Methods (5) 

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
                    this.CreateRijndaelObjects(pwd,
                                               out pdb, out alg);

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
                    this.CreateRijndaelObjects(pwd,
                                               out pdb, out alg);

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

        /// <summary>
        /// Converts a <see cref="SecureString" /> to a byte array.
        /// </summary>
        /// <param name="secStr">The secure string to convert.</param>
        /// <returns>
        /// <paramref name="secStr" /> as byte array or <see langword="null" /> if
        /// <paramref name="secStr" /> is also <see langword="null" /> or does not contain any data.
        /// </returns>
        public static byte[] ToByteArray(SecureString secStr)
        {
            if (secStr == null)
            {
                return null;
            }

            var base64 = secStr.ToUnsecureString();
            if (string.IsNullOrWhiteSpace(base64))
            {
                return null;
            }

            return Convert.FromBase64String(base64.Trim());
        }

        /// <summary>
        /// Converts binary data to a Base64 encoded <see cref="SecureString" /> object.
        /// </summary>
        /// <param name="pwd">The data to convert.</param>
        /// <returns>
        /// <paramref name="pwd" /> as <see cref="SecureString" /> or <see langword="null" /> if
        /// <paramref name="pwd" /> is also <see langword="null" />.
        /// </returns>
        public static SecureString ToSecureString(IEnumerable<byte> pwd)
        {
            var pwdArray = pwd.AsArray();
            if (pwdArray == null || pwdArray.Length < 1)
            {
                return null;
            }

            var result = new SecureString();
            result.Append(Convert.ToBase64String(pwdArray));

            return result;
        }

        /// <summary>
        /// Converts Base64 encoded binary data to a <see cref="SecureString" /> object.
        /// </summary>
        /// <param name="chars">The data to convert.</param>
        /// <returns>
        /// <paramref name="chars" /> as <see cref="SecureString" /> or <see langword="null" /> if
        /// <paramref name="chars" /> is also <see langword="null" /> or contains no data.
        /// </returns>
        public static SecureString ToSecureString(IEnumerable<char> chars)
        {
            var str = chars.AsString();
            if (string.IsNullOrWhiteSpace(str))
            {
                // no data
                return null;
            }

            // keep sure that string is Base64
            return ToSecureString(Convert.FromBase64String(str.Trim()));
        }
        // Private Methods (1) 

        private void CreateRijndaelObjects(SecureString pwd,
                                           out Rfc2898DeriveBytes pdb, out Rijndael alg)
        {
            pdb = new Rfc2898DeriveBytes(ToByteArray(pwd),
                                         this.Salt ?? DEFAULT_CRYTO_SALT,
                                         this.Iterations ?? DEFAULT_ITERATIONS);

            alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
        }

        #endregion Methods
    }
}
