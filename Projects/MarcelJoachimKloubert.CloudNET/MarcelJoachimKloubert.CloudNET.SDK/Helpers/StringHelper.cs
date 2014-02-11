// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace MarcelJoachimKloubert.CloudNET.SDK.Helpers
{
    internal static class StringHelper
    {
        #region Methods (4)

        // Internal Methods (4) 

        internal static string AsString(object obj)
        {
            return AsString(obj, true);
        }

        internal static string AsString(object obj, bool dbNullAsNull)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj is string)
            {
                return (string)obj;
            }

            if (dbNullAsNull &&
                DBNull.Value.Equals(obj))
            {
                return null;
            }

            return obj.ToString();
        }

        internal static bool IsNullOrWhitespace(IEnumerable<char> chars)
        {
            if (chars == null)
            {
                return true;
            }

            foreach (char c in chars)
            {
                if (char.IsWhiteSpace(c) == false)
                {
                    // a non-whitespace character found
                    return false;
                }
            }

            return true;
        }

        internal static string ToUnsecureString(SecureString secStr)
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
