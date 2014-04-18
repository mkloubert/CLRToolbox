// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Describes an object that is documented.
    /// </summary>
    public interface IDocumentable
    {
        #region Data Members (2) 
        
        /// <summary>
        /// Gets the remarks part of <see cref="IDocumentable.Xml" />.
        /// </summary>
        XElement Remarks { get; }

        /// <summary>
        /// Gets the summary part of <see cref="IDocumentable.Xml" />.
        /// </summary>
        XElement Summary { get; }

        /// <summary>
        /// Gets the underlying XML data.
        /// </summary>
        XElement Xml { get; }

        #endregion Data Members 
    }
}