// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;
using System.Xml.Linq;

#if WINDOWS_PHONE
#else

using System.Linq;
using System.Xml.XPath;

#endif

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlNode : _XmlObject, IXmlNode
    {
        #region Constructors (1)

        internal _XmlNode(XNode xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (2)

        public IEnumerable<IXmlElement> this[IEnumerable<char> xpath]
        {
            get { return this.SelectElements(xpath); }
        }

        internal new XNode _Object
        {
            get { return (XNode)base._Object; }
        }

        #endregion Properties

        #region Methods (3)

        internal static IXmlNode CreateByNode(XNode node)
        {
            if (node == null)
            {
                return null;
            }

            if (node is XDocument)
            {
                return new _XmlDocument(node as XDocument);
            }

            if (node is XElement)
            {
                return new _XmlElement(node as XElement);
            }

            if (node is XCData)
            {
                return new _XmlCDData(node as XCData);
            }

            if (node is XText)
            {
                return new _XmlText(node as XText);
            }

            if (node is XComment)
            {
                return new _XmlComment(node as XComment);
            }

            return new _XmlNode(node);
        }

        public virtual IEnumerable<IXmlElement> SelectElements(IEnumerable<char> xpath)
        {
#if !WINDOWS_PHONE
            return this._Object
                       .XPathSelectElements(global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(xpath))
                       .Select(n => CreateByNode(n))
                       .OfType<IXmlElement>();
#else
            throw new global::System.NotSupportedException();
#endif
        }

        public override string ToString()
        {
            return this._Object.ToString();
        }

        #endregion Methods
    }
}