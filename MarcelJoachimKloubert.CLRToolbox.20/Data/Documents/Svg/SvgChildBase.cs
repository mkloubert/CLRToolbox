// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Documents.Svg
{
    /// <summary>
    /// A basic class for a child of a <see cref="SvgChildBase" /> or another child.
    /// </summary>
    public abstract class SvgChildBase
    {
        #region Fields (3)

        private readonly SvgDocument _DOCUMENT;
        private readonly SvgChildBase _PARENT;
        private readonly XmlElement _XML;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of a <see cref="SvgChildBase" /> based class.
        /// </summary>
        /// <param name="doc">The value for the <see cref="SvgChildBase.Document" /> property.</param>
        /// <param name="xml">The value for the <see cref="SvgChildBase.Xml" /> property.</param>
        /// <param name="parent">The value for the <see cref="SvgChildBase.Parent" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="doc" /> and/or <paramref name="xml" /> are <see langword="null" /> references.
        /// </exception>
        protected SvgChildBase(SvgDocument doc, XmlElement xml, SvgChildBase parent)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }

            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            this._DOCUMENT = doc;
            this._XML = xml;
            this._PARENT = parent;
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the underlying document.
        /// </summary>
        public SvgDocument Document
        {
            get { return this._DOCUMENT; }
        }

        /// <summary>
        /// Gets the underlying parent (if available).
        /// If <see langword="null" />, the instance of <see cref="SvgChildBase.Document" /> is the parent.
        /// </summary>
        public SvgChildBase Parent
        {
            get { return this._PARENT; }
        }

        /// <summary>
        /// Gets the underlying XML data.
        /// </summary>
        public XmlElement Xml
        {
            get { return this._XML; }
        }

        #endregion Properties
    }
}