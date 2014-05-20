// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml
{
    /// <summary>
    /// Describes a XML node.
    /// </summary>
    public interface IXmlNode : IXmlObject
    {
        #region Methods (1)

        /// <summary>
        /// Returns a sequence of all nodes by XPath.
        /// </summary>
        /// <param name="xpath">The XPath string.</param>
        /// <returns>The sequences of attributes.</returns>
        IEnumerable<IXmlNode> SelectNodes(IEnumerable<char> xpath);

        #endregion Methods
    }
}