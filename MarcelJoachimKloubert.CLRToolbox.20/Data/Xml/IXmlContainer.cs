// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml
{
    /// <summary>
    /// Describes a node that contains other nodes.
    /// </summary>
    public interface IXmlContainer : IXmlNode
    {
        #region Methods (1)

        /// <summary>
        /// Returns a sequence of all nodes.
        /// </summary>
        /// <returns>The sequences of nodes.</returns>
        IEnumerable<IXmlNode> Nodes();

        #endregion Methods
    }
}