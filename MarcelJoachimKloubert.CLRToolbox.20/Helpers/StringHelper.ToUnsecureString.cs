// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Converts a <see cref="SecureString" /> back to a <see cref="string" /> object.
        /// </summary>
        /// <param name="secStr">the secure string.</param>
        /// <returns>
        /// The UNsecure string or <see langword="null" /> id <paramref name="secStr" />
        /// is also <see langword="null" />.
        /// </returns>
        public static string ToUnsecureString(SecureString secStr)
        {
            if (secStr == null)
            {
                return null;
            }

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secStr);
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }

        #endregion Methods
    }
}
