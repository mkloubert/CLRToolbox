// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    /// <summary>
    /// Assert operations.
    /// </summary>
    public static partial class Assert
    {
        #region Methods (7)
        
        private static string AreEqual_ToObjectDisplayText(object obj)
        {
            if (obj == null)
            {
                return "(null)";
            }

            return string.Format("[{0}] '{1}'; '{2}'",
                                 obj.GetHashCode(),
                                 obj.GetType().FullName,
                                 obj.GetType().Assembly.FullName);
        }

        private static string AreSame_ToObjectDisplayText(object obj)
        {
            if (obj == null)
            {
                return "(null)";
            }

            return string.Format("[{0}] '{1}'; '{2}'",
                                 obj.GetHashCode(),
                                 obj.GetType().FullName,
                                 obj.GetType().Assembly.FullName);
        }

        private static string IsNull_ToObjectDisplayText(object obj)
        {
            if (obj == null)
            {
                return "(null)";
            }

            return string.Format("[{0}] '{1}'; '{2}'",
                                 obj.GetHashCode(),
                                 obj.GetType().FullName,
                                 obj.GetType().Assembly.FullName);
        }

        private static void ThrowAssertException(string message)
        {
            ThrowAssertException(message, null);
        }

        private static void ThrowAssertException(string message, Func<string> ifMessageIsNull)
        {
            if (message == null)
            {
                if (ifMessageIsNull != null)
                {
                    message = ifMessageIsNull();
                }
            }

            if (message == null)
            {
                throw new AssertException();
            }
            else
            {
                throw new AssertException(message);
            }
        }

        private static object ToEquatableValue(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            if ((obj is sbyte) || 
                (obj is short) ||
                (obj is int))
            {
                return Convert.ToInt64(obj);
            }

            if ((obj is byte) ||
                (obj is ushort) ||
                (obj is uint))
            {
                return Convert.ToUInt64(obj);
            }

            if ((obj is float) ||
                (obj is double))
            {
                return Convert.ToDecimal(obj);
            }

            if (obj is IEnumerable<char>)
            {
                string str = obj as string;
                if (str == null)
                {
                    char[] charArray = obj as char[];
                    if (charArray != null)
                    {
                        str = new string(charArray);
                    }
                    else
                    {
                        StringBuilder temp = new StringBuilder();
                        using (IEnumerator<char> e = ((IEnumerable<char>)obj).GetEnumerator())
                        {
                            while (e.MoveNext())
                            {
                                temp.Append(e.Current);
                            }
                        }

                        str = temp.ToString();
                    }
                }

                return str;
            }

            return obj;
        }

        #endregion
    }
}