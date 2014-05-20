// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlNode : _XmlObject, IXmlNode
    {
        #region Constructors (1)

        internal _XmlNode(object xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Methods (1)

        public virtual IEnumerable<IXmlNode> SelectNodes(IEnumerable<char> xpath)
        {
            XmlNode node = (XmlNode)this._Object;

            return GetNodesFromList(node.SelectNodes(StringHelper.AsString(xpath)));
        }

        #endregion Methods
    }
}