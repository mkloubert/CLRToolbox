// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlDocumentType : _XmlNode, IXmlDocumentType
    {
        #region Constructors (1)

        internal _XmlDocumentType(XDocumentType xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XDocumentType _Object
        {
            get { return (XDocumentType)base._Object; }
        }

        #endregion Properties
    }
}