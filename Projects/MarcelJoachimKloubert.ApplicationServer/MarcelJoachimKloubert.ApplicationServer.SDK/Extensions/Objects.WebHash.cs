// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using System.Security.Cryptography;
using MarcelJoachimKloubert.CLRToolbox.Objects;

namespace MarcelJoachimKloubert.ApplicationServer.Extensions
{
    static partial class AppServerExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Calculates the (shorter) hash of an object context for use in a web (interface) context.
        /// </summary>
        /// <param name="ctx">The underlying object context.</param>
        /// <returns>The calculated hash.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctx" /> is <see langword="null" />.
        /// </exception>
        public static byte[] CalculateWebHash(this IObjectContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException("ctx");
            }

            var hash = ctx.CalculateHash();
            if (hash == null)
            {
                return null;
            }

            using (var md5 = new MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(hash);
            }
        }

        /// <summary>
        /// Gets the (shorter) hash of an object context for use in a web (interface) context
        /// as lower case hex string.
        /// </summary>
        /// <param name="ctx">The underlying object context.</param>
        /// <returns>The calculated hash as lower case hex string.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctx" /> is <see langword="null" />.
        /// </exception>
        public static string GetWebHashAsHexString(this IObjectContext ctx)
        {
            var hash = CalculateWebHash(ctx);
            if (hash == null)
            {
                return null;
            }

            return new string(hash.SelectMany(b => b.ToString("x2"))
                                  .ToArray());
        }

        #endregion Methods
    }
}
