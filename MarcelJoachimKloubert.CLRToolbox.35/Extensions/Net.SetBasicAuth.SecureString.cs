// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Net;
using System.Security;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="NetHelper.SetBasicAuth(WebRequest, IEnumerable{char}, SecureString)" />
        public static void SetBasicAuth(this WebRequest request, IEnumerable<char> userName, SecureString pwd)
        {
            NetHelper.SetBasicAuth(request, userName, pwd);
        }

        #endregion Methods
    }
}
