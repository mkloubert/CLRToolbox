// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlElement : _XmlContainer, IXmlElement
    {
        #region Constructors (1)

        internal _XmlElement(XmlElement xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (2)

        internal new XmlElement _Object
        {
            get { return (XmlElement)base._Object; }
        }

        public string LocalName
        {
            get { return this._Object.LocalName; }
        }

        #endregion Properties

        #region Methods (5)

        public virtual IEnumerable<IXmlAttribute> Attributes()
        {
            return GetAttributesFromList(this._Object.Attributes);
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        public override IEnumerable<IXmlNode> Nodes()
        {
            return GetNodesFromList(this._Object.ChildNodes);
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);

            this._InnerObject = xmlDoc.DocumentElement;
            if (this._InnerObject == null)
            {
                throw new NullReferenceException();
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            this._Object.WriteTo(writer);
        }

        #endregion
    }
}