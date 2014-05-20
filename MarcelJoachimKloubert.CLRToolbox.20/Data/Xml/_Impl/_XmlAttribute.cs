// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlAttribute : _XmlObject, IXmlAttribute
    {
        #region Constructors (1)

        internal _XmlAttribute(object xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XmlAttribute _Object
        {
            get { return (XmlAttribute)base._Object; }
        }

        #endregion Properties
    }
}