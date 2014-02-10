// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;
using System.Security;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="StringHelper.Append(SecureString, IEnumerable{char})" />
        public static SecureString Append(this SecureString secStr, IEnumerable<char> charsToAppend)
        {
            StringHelper.Append(secStr, charsToAppend);
            return secStr;
        }

        #endregion Methods
    }
}
