// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal abstract class _XmlContainer : _XmlNode, IXmlContainer
    {
        #region Constructors (1)

        protected internal _XmlContainer(XContainer xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XContainer _Object
        {
            get { return (XContainer)base._Object; }
        }

        #endregion Properties

        #region Methods (1)

        public virtual IEnumerable<IXmlNode> Nodes()
        {
            return this._Object
                       .Nodes()
                       .Select(n => CreateByNode(n));
        }

        #endregion Methods
    }
}