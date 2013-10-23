// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Converts or casts an object to a string.
        /// </summary>
        /// <param name="obj">The string to cast / convert.</param>
        /// <returns>The converted / casted string.</returns>
        /// <remarks>
        /// If <paramref name="obj" /> is <see langword="null" /> the result will also be <see langword="null" />.
        /// <see cref="DBNull" /> objects will be handled as <see langword="null" />.
        /// If <paramref name="obj" /> is already a <see cref="string" />, it is simply casted.
        /// </remarks>
        public static string AsString(object obj)
        {
            return AsString(obj, true);
        }

        /// <summary>
        /// Converts or casts an object to a string.
        /// </summary>
        /// <param name="obj">The string to cast / convert.</param>
        /// <param name="handleDBNullAsNull">
        /// Handle <see cref="DBNull" /> objects as <see langword="null" /> or not.
        /// </param>
        /// <returns>The converted / casted string.</returns>
        /// <remarks>
        /// If <paramref name="obj" /> is <see langword="null" /> the result will also be <see langword="null" />.
        /// If <paramref name="obj" /> is already a <see cref="string" />, it is simply casted.
        /// </remarks>
        public static string AsString(object obj, bool handleDBNullAsNull)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj is string)
            {
                return (string)obj;
            }

            if (handleDBNullAsNull &&
                DBNull.Value.Equals(obj))
            {
                return null;
            }

            if (obj is IEnumerable<char>)
            {
                // char sequence / array
                return new string(CollectionHelper.AsArray(obj as IEnumerable<char>));
            }
            else if (obj is XmlNode)
            {
                // old skool XML
                return ((XmlNode)obj).OuterXml;
            }
            else if (obj is TextReader)
            {
                // read text to end
                return ((TextReader)obj).ReadToEnd();
            }

            // use default
            return obj.ToString();
        }

        #endregion Methods
    }
}
