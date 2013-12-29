// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class NetHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Sets up a <see cref="WebRequest" /> for basic authorization.
        /// </summary>
        /// <param name="request">The underlying request.</param>
        /// <param name="userName">The username.</param>
        /// <param name="pwd">The password.</param>
        /// <exception cref="ArgumentNullException"><paramref name="request" /> is <see langword="null" />.</exception>
        /// <exception cref="FormatException"><paramref name="userName" /> contains invalid char(s).</exception>
        public static void SetBasicAuth(WebRequest request, IEnumerable<char> userName, IEnumerable<char> pwd)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }

            string user = StringHelper.AsString(userName) ?? string.Empty;
            if (user.Contains(":"))
            {
                throw new FormatException();
            }

            string authInfo = string.Format("{0}:{1}",
                                            user,
                                            StringHelper.AsString(pwd));

            request.Headers["Authorization"] = string.Format("Basic {0}",
                                                             Convert.ToBase64String(Encoding.GetEncoding("ASCII")
                                                                                            .GetBytes(authInfo)));
        }

        /// <summary>
        /// Sets up a <see cref="WebRequest" /> for basic authorization.
        /// </summary>
        /// <param name="request">The underlying request.</param>
        /// <param name="userName">The username.</param>
        /// <param name="pwd">The password as ASCII binary data.</param>
        /// <exception cref="ArgumentNullException"><paramref name="request" /> is <see langword="null" />.</exception>
        /// <exception cref="FormatException"><paramref name="userName" /> contains invalid char(s).</exception>
        public static void SetBasicAuth(WebRequest request, IEnumerable<char> userName, IEnumerable<byte> pwd)
        {
            byte[] pwdArray = CollectionHelper.AsArray(pwd);

            SetBasicAuth(request,
                         userName,
                         pwdArray != null ? Encoding.GetEncoding("ASCII")
                                                    .GetString(pwdArray, 0, pwdArray.Length) : null);
        }

        #endregion Methods
    }
}
