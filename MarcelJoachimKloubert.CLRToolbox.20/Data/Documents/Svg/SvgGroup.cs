// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Documents.Svg
{
    /// <summary>
    /// A group.
    /// </summary>
    public class SvgGroup : SvgChildBase
    {
        #region Constructors (1)

        internal SvgGroup(SvgDocument doc, XmlElement xml)
            : base(doc, xml, null)
        {
        }

        #endregion Constructors
    }
}