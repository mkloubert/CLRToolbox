// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlDocument : _XmlContainer, IXmlDocument
    {
        #region Constructors (1)

        internal _XmlDocument(XmlDocument xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (2)

        internal new XmlDocument _Object
        {
            get { return (XmlDocument)base._Object; }
        }

        public IXmlElement Root
        {
            get
            {
                XmlElement rootElement = this._Object.DocumentElement;

                return rootElement != null ? new _XmlElement(rootElement) : null;
            }
        }

        #endregion Properties

        #region Methods (6)

        public override IEnumerable<IXmlNode> Nodes()
        {
            return GetNodesFromList(this._Object.ChildNodes);
        }

        internal static _XmlDocument Load(TextReader reader)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);

            return new _XmlDocument(xmlDoc);
        }

        internal static _XmlDocument Load(Stream stream)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            return new _XmlDocument(xmlDoc);
        }

        internal static _XmlDocument Load(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            return new _XmlDocument(xmlDoc);
        }

        internal static _XmlDocument Parse(StringBuilder builder)
        {
            return Parse(builder.ToString());
        }

        internal static _XmlDocument Parse(IEnumerable<char> xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(StringHelper.AsString(xml) ?? string.Empty);

            return new _XmlDocument(xmlDoc);
        }

        #endregion Methods
    }
}