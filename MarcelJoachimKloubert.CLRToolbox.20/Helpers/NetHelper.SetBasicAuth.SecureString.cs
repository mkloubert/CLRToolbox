// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Net;
using System.Security;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class NetHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Sets up a <see cref="WebRequest" /> for basic authorization.
        /// </summary>
        /// <param name="request">The underlying request.</param>
        /// <param name="userName">The username.</param>
        /// <param name="pwd">The password.</param>
        /// <exception cref="ArgumentNullException"><paramref name="request" /> is <see langword="null" />.</exception>
        /// <exception cref="FormatException"><paramref name="userName" /> contains invalid char(s).</exception>
        public static void SetBasicAuth(WebRequest request, IEnumerable<char> userName, SecureString pwd)
        {
            SetBasicAuth(request,
                         userName,
                         StringHelper.ToUnsecureString(pwd));
        }

        #endregion Methods
    }
}
