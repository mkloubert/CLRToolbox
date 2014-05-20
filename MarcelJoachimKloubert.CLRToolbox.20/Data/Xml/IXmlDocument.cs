// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml
{
    /// <summary>
    /// Describes a XML document.
    /// </summary>
    public interface IXmlDocument : IXmlContainer
    {
        #region Properties (1)

        /// <summary>
        /// Gets the root element (if available).
        /// </summary>
        IXmlElement Root { get; }

        #endregion Properties
    }
}