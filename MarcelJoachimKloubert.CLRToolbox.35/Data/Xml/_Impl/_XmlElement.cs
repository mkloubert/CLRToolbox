// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlElement : _XmlContainer, IXmlElement
    {
        #region Constructors (1)

        internal _XmlElement(XElement xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XElement _Object
        {
            get { return (XElement)base._Object; }
        }

        #endregion Properties

        #region Methods (2)

        public virtual IEnumerable<IXmlAttribute> Attributes()
        {
            return this._Object
                       .Attributes()
                       .Select(a => (IXmlAttribute)new _XmlAttribute(a));
        }

        public override IEnumerable<IXmlNode> Nodes()
        {
            return this._Object
                       .Nodes()
                       .Select(n => (IXmlNode)new _XmlNode(n));
        }

        #endregion
    }
}