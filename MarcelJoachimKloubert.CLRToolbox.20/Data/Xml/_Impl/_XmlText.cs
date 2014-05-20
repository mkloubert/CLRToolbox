// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlText : _XmlNode, IXmlText
    {
        #region Constructors (1)

        internal _XmlText(XmlText xmlObject)
            : base(xmlObject)
        {
        }

        protected internal _XmlText(XmlNode xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XmlText _Object
        {
            get { return (XmlText)base._Object; }
        }

        #endregion Properties
    }
}