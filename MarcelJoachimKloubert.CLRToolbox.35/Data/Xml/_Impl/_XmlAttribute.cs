// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlAttribute : _XmlObject, IXmlAttribute
    {
        #region Constructors (1)

        internal _XmlAttribute(XAttribute xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (3)

        internal new XAttribute _Object
        {
            get { return (XAttribute)base._Object; }
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
    }
}