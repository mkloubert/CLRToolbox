// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlElement : _XmlContainer, IXmlElement
    {
        #region Constructors (1)

        internal _XmlElement(object xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XmlElement _Object
        {
            get { return (XmlElement)base._Object; }
        }

        #endregion Properties

        #region Methods (2)

        public virtual IEnumerable<IXmlAttribute> Attributes()
        {
            return GetAttributesFromList(this._Object.Attributes);
        }

        public override IEnumerable<IXmlNode> Nodes()
        {
            return GetNodesFromList(this._Object.ChildNodes);
        }

        #endregion
    }
}