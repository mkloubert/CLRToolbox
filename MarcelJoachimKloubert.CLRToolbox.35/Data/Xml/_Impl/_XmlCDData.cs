// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlCDData : _XmlText, IXmlCData
    {
        #region Constructors (1)

        internal _XmlCDData(XCData xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XCData _Object
        {
            get { return (XCData)base._Object; }
        }

        #endregion Properties
    }
}