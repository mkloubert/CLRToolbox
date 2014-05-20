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
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IXmlNode.SelectElements(IEnumerable{char})" />
        IEnumerable<IXmlElement> this[IEnumerable<char> xpath] { get; }

        #endregion Methods

        #region Methods (1)

        /// <summary>
        /// Returns a sequence of elements by XPath.
        /// </summary>
        /// <param name="xpath">The XPath string.</param>
        /// <returns>The sequences of elements.</returns>
        IEnumerable<IXmlElement> SelectElements(IEnumerable<char> xpath);

        #endregion Methods
    }
}