// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal abstract class _XmlContainer : _XmlNode, IXmlContainer
    {
        #region Constructors (1)

        protected internal _XmlContainer(object xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Methods (1)

        public IEnumerator<IXmlNode> GetEnumerator()
        {
            return this.Nodes()
                       .GetEnumerator();
        }

        public abstract IEnumerable<IXmlNode> Nodes();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods
    }
}