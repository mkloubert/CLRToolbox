// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlAttribute : _XmlObject, IXmlAttribute
    {
        #region Constructors (1)

        internal _XmlAttribute(XmlAttribute xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (2)

        internal new XmlAttribute _Object
        {
            get { return (XmlAttribute)base._Object; }
        }
        
        public string LocalName
        {
            get { return this._Object.LocalName; }
        }

        #endregion Properties
    }
}