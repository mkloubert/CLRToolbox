// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml
{
    /// <summary>
    /// Describes a XML attribute.
    /// </summary>
    public interface IXmlAttribute : IXmlObject
    {
        #region Properties (1)

        /// <summary>
        /// Gets the local name of that attribute.
        /// </summary>
        string LocalName { get; }

        #endregion Properties
    }
}