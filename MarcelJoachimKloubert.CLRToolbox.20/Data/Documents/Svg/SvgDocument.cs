// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Documents.Svg
{
    /// <summary>
    /// A document that contains data of an SVG image.
    /// </summary>
    public class SvgDocument : DocumentBase
    {
        #region Fields (2)

        /// <summary>
        /// Stores the namespace URI.
        /// </summary>
        public const string NAMESPACE_URI = "http://www.w3.org/2000/svg";
        private readonly XmlDocument _XML;

        #endregion Fields

        #region Constructors (1)

        private SvgDocument(XmlDocument xml)
        {
            this._XML = xml;
        }

        #endregion Constructors

        #region Properties (4)

        /// <inheriteddoc />
        public override string ContentType
        {
            get { return "image/svg+xml"; }
        }

        /// <inheriteddoc />
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        /// <inheriteddoc />
        public override string Name
        {
            get { return this.GetTitle(); }
        }

        /// <inheriteddoc />
        public XmlDocument Xml
        {
            get { return this._XML; }
        }

        #endregion Properties

        #region Methods (6)

        // Public Methods (2) 

        /// <summary>
        /// Creates a new instance from a stream.
        /// </summary>
        /// <param name="stream">The stream from where to read the data from.</param>
        /// <returns>The created instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="stream" /> is not readable.
        /// </exception>
        public static SvgDocument FromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (stream.CanRead == false)
            {
                throw new IOException();
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(stream);

            return new SvgDocument(xml);
        }

        /// <summary>
        /// Returns the version of the SVG specification the document was created for.
        /// </summary>
        /// <returns>The SVG specification version.</returns>
        public Version GetSvgVersion()
        {
            Version result = null;

            XmlElement root = this.Xml.DocumentElement;
            if (root != null)
            {
                foreach (XmlAttribute attr in root.Attributes)
                {
                    if (StringHelper.IsNullOrWhiteSpace(attr.NamespaceURI) &&
                        attr.LocalName == "version")
                    {
                        string varStr = attr.InnerText;
                        if (StringHelper.IsNullOrWhiteSpace(varStr) == false)
                        {
                            try
                            {
                                result = new Version(varStr.Trim());
                            }
                            catch
                            {
                                result = null;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the title of that document.
        /// </summary>
        /// <returns>The title.</returns>
        public string GetTitle()
        {
            string result = null;

            XmlElement root = this.Xml.DocumentElement;
            if (root != null)
            {
                foreach (XmlElement e in CollectionHelper.OfType<XmlElement>(root.ChildNodes))
                {
                    if (IsNamespace(e) &&
                        e.LocalName == "title")
                    {
                        result = e.InnerText;
                    }
                }
            }

            return result;
        }

        /// <inheriteddoc />
        public override Stream OpenStream()
        {
            MemoryStream result = new MemoryStream();
            this.Xml.Save(result);

            return result;
        }

        // Protected Methods (2) 
        
        /// <summary>
        /// Checks if a XML node is part of the namespace in <see cref="SvgDocument.NAMESPACE_URI" />.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <returns>Is part of namespace or not.</returns>
        protected static bool IsNamespace(XmlNode node)
        {
            return IsNamespace(node != null ? node.NamespaceURI : null);
        }

        /// <summary>
        /// Checks if an URI is part of the namespace in <see cref="SvgDocument.NAMESPACE_URI" />.
        /// </summary>
        /// <param name="uri">The uri to check.</param>
        /// <returns>Is part of namespace or not.</returns>
        protected static bool IsNamespace(IEnumerable<char> uri)
        {
            if (uri != null)
            {
                return StringHelper.AsString(uri)
                                   .ToLower()
                                   .Trim()
                                   .StartsWith(NAMESPACE_URI);
            }

            return false;
        }

        #endregion Methods
    }
}