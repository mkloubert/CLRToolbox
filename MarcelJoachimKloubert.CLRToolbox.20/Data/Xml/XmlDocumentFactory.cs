// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml
{
    /// <summary>
    /// Creates <see cref="IXmlDocument" /> objects.
    /// </summary>
    public static class XmlDocumentFactory
    {
        #region Methods (5)

        /// <summary>
        /// Loads a XML document from a text reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The loaded document.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        public static IXmlDocument Load(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            return _XmlDocument.Load(reader);
        }

        /// <summary>
        /// Loads a XML document from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The loaded document.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        public static IXmlDocument Load(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            return _XmlDocument.Load(stream);
        }
        
        /// <summary>
        /// Loads a XML document from a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The loaded document.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// File does not exist.
        /// </exception>
        public static IXmlDocument Load(IEnumerable<char> path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            string filePath = StringHelper.AsString(path);
            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException();
            }

            return _XmlDocument.Parse(filePath);
        }
        
        /// <summary>
        /// Parses <see cref="StringBuilder" /> to a document.
        /// </summary>
        /// <param name="builder">The string builder that contains XML data.</param>
        /// <returns>The new document.</returns>
        public static IXmlDocument Parse(StringBuilder builder)
        {
            return _XmlDocument.Parse(builder);
        }

        /// <summary>
        /// Parses an XML string to a document.
        /// </summary>
        /// <param name="xml">The XML data.</param>
        /// <returns>The new document.</returns>
        public static IXmlDocument Parse(IEnumerable<char> xml)
        {
            return _XmlDocument.Parse(xml);
        }

        #endregion Methods
    }
}