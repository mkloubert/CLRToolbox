// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlComment : _XmlNode, IXmlComment
    {
        #region Constructors (1)

        internal _XmlComment(object xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XmlComment _Object
        {
            get { return (XmlComment)base._Object; }
        }

        #endregion Properties
    }
}