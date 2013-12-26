// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Net;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="NetHelper.SetBasicAuth(WebRequest, IEnumerable{char}, IEnumerable{char})" />
        public static void SetBasicAuth(this WebRequest request, IEnumerable<char> userName, IEnumerable<char> pwd)
        {
            NetHelper.SetBasicAuth(request, userName, pwd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="NetHelper.SetBasicAuth(WebRequest, IEnumerable{char}, IEnumerable{byte})" />
        public static void SetBasicAuth(this WebRequest request, IEnumerable<char> userName, IEnumerable<byte> pwd)
        {
            NetHelper.SetBasicAuth(request, userName, pwd);
        }

        #endregion Methods
    }
}
