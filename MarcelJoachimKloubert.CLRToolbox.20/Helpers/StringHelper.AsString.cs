// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.IO;
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
                // hex string
                return AsHexString(obj as IEnumerable<byte>, true);
            }
            else if (obj is Stream)
            {
                // handle blob as UTF-8 string data
                using (StreamReader reader = new StreamReader(new NonDisposableStream(obj as Stream),
                                                              Encoding.UTF8))
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
            else if (obj is IContentProvider)
            {
                IContentProvider provider = (IContentProvider)obj;

                Stream stream = provider.OpenStream();
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream,
                                                                  provider.Encoding ?? Encoding.UTF8,
                                                                  false))
                    {
                        return AsString(reader, true);
                    }
                }

                return null;
            }
            else if ((obj is IEnumerable<string>) ||
                     (obj is IEnumerable<IEnumerable<char>>))
            {
                // concat string sequence

                IEnumerable seq = (IEnumerable)obj;
                IEnumerable<object> objSeq = CollectionHelper.Cast<object>(seq);

                StringBuilder concatedStrList = new StringBuilder();
                foreach (object item in objSeq)
                {
                    concatedStrList.Append(AsString(item, true));
                }

                return concatedStrList.ToString();
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


        private static void AsStringExtension(object obj, ref bool handled, ref global::System.Text.StringBuilder result)
        {
#if !WINDOWS_PHONE
            if (obj is global::System.Xml.XmlReader)
            {
                global::System.Xml.XmlReader xmlReader = (global::System.Xml.XmlReader)obj;

                global::System.Xml.XmlDocument xmlDoc = new global::System.Xml.XmlDocument();
                xmlDoc.Load(xmlReader);

                result.Append(AsString(xmlDoc, true));
                handled = true;
            }
            else if (obj is global::System.Xml.XmlNode)
            {
                // old skool XML

                result.Append(((global::System.Xml.XmlNode)obj).OuterXml);
                handled = true;
            }
#endif
        }

        #endregion Methods
    }
}
