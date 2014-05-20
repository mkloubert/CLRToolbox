// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlNode : _XmlObject, IXmlNode
    {
        #region Constructors (1)

        internal _XmlNode(XmlNode xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors
        
        #region Properties (1)

        public IEnumerable<IXmlElement> this[IEnumerable<char> xpath]
        {
            get { return this.SelectElements(xpath); }
        }

        #endregion Properties

        #region Methods (2)

        internal static IXmlNode CreateByNode(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }

            if (node is XmlDocument)
            {
                return new _XmlDocument(node as XmlDocument);
            }

            if (node is XmlElement)
            {
                return new _XmlElement(node as XmlElement);
            }

            if (node is XmlCDataSection)
            {
                return new _XmlCDData(node as XmlCDataSection);
            }

            if (node is XmlText)
            {
                return new _XmlText(node as XmlText);
            }

            if (node is XmlComment)
            {
                return new _XmlComment(node as XmlComment);
            }

            return new _XmlNode(node);
        }

        public virtual IEnumerable<IXmlElement> SelectElements(IEnumerable<char> xpath)
        {
            IEnumerable<IXmlNode> selectedNodes = GetNodesFromList(this._Object
                                                                       .SelectNodes(StringHelper.AsString(xpath)));

            return CollectionHelper.OfType<IXmlElement>(selectedNodes);
        }

        #endregion Methods
    }
}