// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml
{
    /// <summary>
    /// Describes a XML element.
    /// </summary>
    public interface IXmlElement : IXmlContainer
    {
        #region Methods (1)

        /// <summary>
        /// Returns a sequence of all attributes.
        /// </summary>
        /// <returns>The sequences of attributes.</returns>
        IEnumerable<IXmlAttribute> Attributes();

        #endregion Methods
    }
}