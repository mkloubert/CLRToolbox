// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers
{
    /// <summary>
    /// Helper class for wiki operations.
    /// </summary>
    public static class WikiHelper
    {
        #region Fields (1)

        private const string _XML_ATTRIB_HREF = "href";

        #endregion Fields

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Converts a string for using in a table cell of a wiki page.
        /// </summary>
        /// <param name="chars">The char sequence / string to convert.</param>
        /// <returns>The converted data.</returns>
        public static string ToTableCellValue(IEnumerable<char> chars)
        {
            var str = chars.AsString();
            if (str == null)
            {
                return string.Empty;
            }

            return str.Replace("\r", "<br />")
                      .Replace("\n", "<br />");
        }

        /// <summary>
        /// Converts the content of a <see cref="" /> to MediaWiki markup.
        /// </summary>
        /// <param name="xml">The data to convert.</param>
        /// <returns>The converted data.</returns>
        public static string ToWikiMarkup(XElement xml)
        {
            var builder = new StringBuilder();
            ToWikiMarkup(xml, builder);

            var result = builder.ToString();

            var lines = result.Replace("\r", string.Empty)
                              .Split('\n');

            // normalize lines
            {
                // get common number of leading
                // whitespaces for each line
                int? kgv = null;
                for (var i = 0; i < lines.Length; i++)
                {
                    var l = lines[i];

                    var beginningWhitespaces = l.Length - l.TrimStart().Length;
                    kgv = Math.Min(beginningWhitespaces, kgv ?? int.MaxValue);
                }

                // remove that number of common
                // leading whitespaces
                if (kgv.HasValue)
                {
                    for (var i = 0; i < lines.Length; i++)
                    {
                        var l = lines[i];

                        lines[i] = l.Substring(kgv.Value).Trim();
                    }
                }
            }

            return string.Join("\n", lines);
        }

        // Private Methods (1) 

        private static void ToWikiMarkup(XElement xml, StringBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            if (xml == null)
            {
                return;
            }

            foreach (var node in xml.Nodes())
            {
                if (node is XText)
                {
                    var txt = (XText)node;

                    builder.Append(txt.Value);
                }
                else if (node is XElement)
                {
                    var element = (XElement)node;

                    switch (element.Name.LocalName)
                    {
                        case "see":
                            {
                                if (element.Attribute("langword") != null)
                                {
                                }
                                else if (element.Attribute("cref") != null)
                                {
                                }
                                else if (element.Attribute(_XML_ATTRIB_HREF) != null)
                                {
                                    var hrefAttrib = element.Attribute(_XML_ATTRIB_HREF);

                                    builder.AppendFormat(hrefAttrib.Value ?? string.Empty);
                                }
                            }
                            break;
                    }
                }
            }
        }

        #endregion Methods
    }
}