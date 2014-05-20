// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlProcessingInstruction : _XmlNode, IXmlProcessingInstruction
    {
        #region Constructors (1)

        internal _XmlProcessingInstruction(XProcessingInstruction xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XProcessingInstruction _Object
        {
            get { return (XProcessingInstruction)base._Object; }
        }

        #endregion Properties
    }
}