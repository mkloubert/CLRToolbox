// 
// WPF based tool to create product pages for auctions on eBay, e.g.
// Copyright (C) 2013  Marcel Joachim Kloubert
//     
// This library is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or (at
// your option) any later version.
//     
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// General Public License for more details.
//     
// You should have received a copy of the GNU General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301,
// USA.
// 


using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ICSharpCode.AvalonEdit.Document;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Controls.Items;
using MarcelJoachimKloubert.WpfAuctionDesigner.Windows;
using Xceed.Wpf.Toolkit;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Controls
{
    /// <summary>
    /// Helper class for editor operations.
    /// </summary>
    public static class TMEditorHelper
    {
        #region Methods (7)

        // Public Methods (3) 

        /// <summary>
        /// Parses a string for HTML output.
        /// </summary>
        /// <param name="chars">The input text.</param>
        /// <param name="withTags">Prase tags or not.</param>
        /// <returns>The HTML output.</returns>
        public static string ParseForHtml(IEnumerable<char> chars,
                                          bool withTags)
        {
            if (chars == null)
            {
                return null;
            }

            var html = new StringBuilder(HttpUtility.HtmlEncode(chars.AsString()));

            if (withTags)
            {
                var textReplacers = new Dictionary<string, Func<string>>()
                {
                    { "{blocksatz}", () => @"<p style=""text-align:justify !important;"">" },
                    { "{/blocksatz}", AddParagraphHtmlEndTag },

                    { "{links}", () => @"<p style=""text-align:left !important;"">" },
                    { "{/links}", AddParagraphHtmlEndTag },

                    { "{rechts}", () => @"<p style=""text-align:right !important;"">" },
                    { "{/rechts}", AddParagraphHtmlEndTag },

                    { "{mitte}", () => @"<p style=""text-align:center !important;"">" },
                    { "{/mitte}", AddParagraphHtmlEndTag },

                    { "{fett}", () => @"<span style=""font-weight:bold !important;"">" },
                    { "{/fett}", AddSpanHtmlEndTag },

                    { "{kursiv}", () => @"<span style=""font-style:italic !important;"">" },
                    { "{/kursiv}", AddSpanHtmlEndTag },

                    { "{unterstreichen}", () => @"<span style=""text-decoration:underline !important;"">" },
                    { "{/unterstreichen}", AddSpanHtmlEndTag },

                    { "{durchstreichen}", () => @"<span style=""text-decoration:line-through !important;"">" },
                    { "{/durchstreichen}", AddSpanHtmlEndTag },

                    { "{nach_rechts}", () => @"<span style=""padding-left:2em !important;"">" },
                    { "{/nach_rechts}", AddSpanHtmlEndTag },

                    { "{*}", () => @"<li>" },
                    { "{/*}", () => "</li>" },

                    { "{bild}", () => @"<img class=""artikelBild"" src=""" },
                    { "{/bild}", () => @""" />" },
                };

                // font color tags
                foreach (var color in MainWindow.ViewModel.FONT_COLORS)
                {
                    var tagName = ToFontColorTagName(color);

                    textReplacers.Add(string.Format("{{{0}}}", tagName),
                                      CreateColorFunc(color));
                    textReplacers.Add(string.Format("{{/{0}}}", tagName),
                                      AddSpanHtmlEndTag);
                }

                // font size tags
                foreach (var size in MainWindow.ViewModel.FONT_SIZES)
                {
                    var tagName = size.Value.AsString(true);

                    textReplacers.Add(string.Format("{{{0}}}", tagName),
                                      CreateSizeFunc(size));
                    textReplacers.Add(string.Format("{{/{0}}}", tagName),
                                      AddSpanHtmlEndTag);
                }

                foreach (var tr in textReplacers)
                {
                    var expr = tr.Key;
                    var func = tr.Value;

                    html.Replace(expr,
                                 func());
                }
            }

            html.Replace("  ", "&nbsp;&nbsp;")
                .Replace("\r", string.Empty)
                .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;")
                .Replace("ä", "&auml;").Replace("Ä", "&Auml;")
                .Replace("ö", "&ouml;").Replace("Ö", "&Ouml;")
                .Replace("ü", "&uuml;").Replace("Ü", "&Uuml;")
                .Replace("ß", "&szlig;")
                .Replace("€", "&euro;")
                .Replace("\n", "<br />");

            return html.ToString();
        }

        /// <summary>
        /// Converts a <see cref="ColorItem" /> object to
        /// th name of its underlying tag in syntax editors.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>
        /// The tag name or <see langword="null" /> if
        /// <paramref name="color" /> is also <see langword="null" />.
        /// </returns>
        public static string ToFontColorTagName(ColorItem color)
        {
            var name = string.Empty;
            if (color != null)
            {
                name = (color.Name ?? string.Empty).ToLower()
                                                   .Replace("ä", "ae")
                                                   .Replace("ö", "oe")
                                                   .Replace("ü", "ue")
                                                   .Replace("ß", "ss")
                                                   .Trim();
            }

            return string.Format("farbe_{0}",
                                 name);
        }

        /// <summary>
        /// Returns the plain content of a <see cref="TextDocument" />.
        /// </summary>
        /// <param name="doc">The text document.</param>
        /// <returns>
        /// The content of <paramref name="doc" /> or <see langword="null" />
        /// if <paramref name="doc" /> is also a <see langword="null" /> reference.
        /// </returns>
        public static string ToString(TextDocument doc)
        {
            if (doc == null)
            {
                return null;
            }

            using (var reader = doc.CreateReader())
            {
                return reader.ReadToEnd();
            }
        }
        // Private Methods (4) 

        private static string AddParagraphHtmlEndTag()
        {
            return "</p>";
        }

        private static string AddSpanHtmlEndTag()
        {
            return "</span>";
        }

        private static Func<string> CreateColorFunc(ColorItem color)
        {
            if (color == null)
            {
                return null;
            }

            return new Func<string>(() => string.Format(@"<span style=""color:#{0:x2}{1:x2}{2:x2} !important"">",
                                                        color.Color.R,
                                                        color.Color.G,
                                                        color.Color.B));
        }

        private static Func<string> CreateSizeFunc(TMValueComboBoxItem fontSize)
        {
            if (fontSize == null)
            {
                return null;
            }

            return new Func<string>(() => string.Format(@"<span class=""{0}"">",
                                                        fontSize.Tag.AsString(true)));
        }

        #endregion Methods
    }
}
