// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (3)

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
            else if (obj is TextReader)
            {
                // read text to end
                return ((TextReader)obj).ReadToEnd();
            }
            else if (obj is IEnumerable<byte>)
            {
                // handle blob as UTF-8 string
                byte[] utf8Blob = CollectionHelper.AsArray(obj as IEnumerable<byte>);

                return Encoding.UTF8.GetString(utf8Blob, 0, utf8Blob.Length);
            }
            else if (obj is Stream)
            {
                // handle blob as UTF-8 string data
                using (StreamReader reader = new StreamReader((Stream)obj, Encoding.UTF8, true))
                {
                    return AsString(reader, true);
                }
            }
            else if (obj is IXmlSerializable)
            {
                // XML object

                IXmlSerializable xmlObj = (IXmlSerializable)obj;

                StringBuilder xml = new StringBuilder();

                using (StringWriter strWriter = new StringWriter(xml))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(strWriter))
                    {
                        xmlObj.WriteXml(xmlWriter);

                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }

                return AsString(xml, true);
            }
            else if ((obj is IEnumerable<string>) ||
                     (obj is IEnumerable<IEnumerable<char>>))
            {
                // concat string sequence

                IEnumerable seq = (IEnumerable)obj;
                IEnumerable<object> objSeq = CollectionHelper.Cast<object>(seq);
                IEnumerable<string> strSeq = CollectionHelper.Cast<string>(objSeq);

                return string.Concat(CollectionHelper.AsArray(strSeq));
            }

            bool extensionHandled = false;
            StringBuilder extensionResultBuilder = new StringBuilder();
            AsStringExtension(obj,
                              ref extensionHandled,
                              ref extensionResultBuilder);

            if (extensionHandled)
            {
                return extensionResultBuilder != null ? extensionResultBuilder.ToString() : null;
            }

            // use default
            return obj.ToString();
        }
        // Private Methods (1) 

        static partial void AsStringExtension(object obj, ref bool handled, ref StringBuilder result);

        #endregion Methods
    }
}
