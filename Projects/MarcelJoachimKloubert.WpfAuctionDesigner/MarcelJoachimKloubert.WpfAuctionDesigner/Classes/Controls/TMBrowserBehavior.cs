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


using System.Windows;
using System.Windows.Controls;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Controls
{
    /// <summary>
    /// Contains attached properties for <see cref="WebBrowser" /> control.
    /// </summary>
    public sealed class TMBrowserBehavior
    {
        #region Fields (1)

        /// <summary>
        /// HTML property.
        /// </summary>
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html",
                                                typeof(string),
                                                typeof(TMBrowserBehavior),
                                                new FrameworkPropertyMetadata(OnHtmlChanged));

        #endregion Fields

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Gets the current HTML value.
        /// </summary>
        /// <param name="browser">The underlying browser.</param>
        /// <returns>The current HTML value.</returns>
        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser browser)
        {
            return browser.GetValue(HtmlProperty).AsString(true);
        }

        /// <summary>
        /// Sets the current HTML value.
        /// </summary>
        /// <param name="browser">The underlying browser.</param>
        /// <param name="value">The new HTML value.</param>
        public static void SetHtml(WebBrowser browser,
                                   string value)
        {
            browser.SetValue(HtmlProperty, value);
        }
        // Private Methods (1) 

        private static void OnHtmlChanged(DependencyObject dependencyObject,
                                          DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = dependencyObject as WebBrowser;
            if (webBrowser == null)
            {
                return;
            }

            var html = e.NewValue.AsString(true);
            if (string.IsNullOrWhiteSpace(html))
            {
                return;
            }

            webBrowser.NavigateToString(html);
        }

        #endregion Methods
    }
}
