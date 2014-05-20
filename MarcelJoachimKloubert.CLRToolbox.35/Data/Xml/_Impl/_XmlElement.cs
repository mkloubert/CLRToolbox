// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

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

        #region Properties (3)

        internal new XElement _Object
        {
            get { return (XElement)base._Object; }
        }

        public string LocalName
        {
            get { return this._Object.Name.LocalName; }
        }

        public override string NamespaceUri
        {
            get { return this._Object.Name.NamespaceName; }
        }

        #endregion Properties

        #region Methods (4)

        public virtual IEnumerable<IXmlAttribute> Attributes()
        {
            return this._Object
                       .Attributes()
                       .Select(a => (IXmlAttribute)new _XmlAttribute(a));
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        public override IEnumerable<IXmlNode> Nodes()
        {
            return this._Object
                       .Nodes()
                       .Select(n => (IXmlNode)new _XmlNode(n));
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            ((IXmlSerializable)this._Object).ReadXml(reader);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            ((IXmlSerializable)this._Object).WriteXml(writer);
        }

        #endregion
    }
}