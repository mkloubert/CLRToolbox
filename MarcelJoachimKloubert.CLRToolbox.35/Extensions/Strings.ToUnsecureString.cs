// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


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
        /// <see cref="StringHelper.ToUnsecureString(SecureString)" />
        public static string ToUnsecureString(this SecureString secStr)
        {
            return StringHelper.ToUnsecureString(secStr);
        }

        #endregion Methods
    }
}
