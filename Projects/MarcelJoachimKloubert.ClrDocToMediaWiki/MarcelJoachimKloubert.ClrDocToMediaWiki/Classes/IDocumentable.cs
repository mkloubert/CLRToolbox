// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Text;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Describes an object that is documented.
    /// </summary>
    public interface IDocumentable
    {
        #region Data Members (3)

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

        #region Operations (3)

        /// <summary>
        /// Returns the name of the wiki page for that object.
        /// </summary>
        /// <returns>The name of the wiki page or <see langword="null" /> if not defined.</returns>
        string GetWikiPageName();

        /// <summary>
        /// Generates MediaWiki markup from the data of that instance.
        /// </summary>
        /// <returns>The generated markup.</returns>
        string ToMediaWiki();

        /// <summary>
        /// Generates MediaWiki markup from the data of that instance
        /// and writes it to a <see cref="StringBuilder" /> object.
        /// </summary>
        /// <param name="builder">The builder where to write the markup data to.</param>
        void ToMediaWiki(StringBuilder builder);

        #endregion Operations
    }
}