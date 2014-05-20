// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal abstract class _XmlObject : TMObject, IXmlObject
    {
        #region Constructors (1)

        protected internal _XmlObject(XObject xmlObject)
        {
            this._Object = xmlObject;
        }

        #endregion Constructors

        #region Properties (1)

        internal XObject _Object
        {
            get;
            private set;
        }

        #endregion Properties
    }
}