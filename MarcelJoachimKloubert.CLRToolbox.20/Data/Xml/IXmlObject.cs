// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml
{
    /// <summary>
    /// Describes an XML object.
    /// </summary>
    public interface IXmlObject : ITMObject
    {
        #region Properties (1)

        /// <summary>
        /// Gets the URI of the namespace (if available).
        /// </summary>
        string NamespaceUri { get; }

        #endregion Properties
    }
}