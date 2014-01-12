// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Windows;
using System.Windows.Controls;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Behaviours
{
    /// <summary>
    /// Attached properties for <see cref="WebBrowser" /> controls.
    /// </summary>
    public static class WebBrowserBehavior
    {
        #region Fields (1)

        /// <summary>
        /// Attached property for binding HTML source code with a <see cref="WebBrowser" /> control.
        /// </summary>
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html",
            typeof(string),
            typeof(global::MarcelJoachimKloubert.CLRToolbox.Windows.Behaviours.WebBrowserBehavior),
            new FrameworkPropertyMetadata(OnHtmlChanged));

        #endregion Fields

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Returns the HTML source code of a <see cref="WebBrowser" /> control.
        /// </summary>
        /// <param name="browser">The underlying browser.</param>
        /// <returns>The current HTML source code of the control.</returns>
        [AttachedPropertyBrowsableForType(typeof(global::System.Windows.Controls.WebBrowser))]
        public static string GetHtml(WebBrowser browser)
        {
            return (string)browser.GetValue(HtmlProperty);
        }

        /// <summary>
        /// Sets the HTML source code of a <see cref="WebBrowser" /> control.
        /// </summary>
        /// <param name="browser">he underlying browser.</param>
        /// <param name="html">The source code to set.</param>
        public static void SetHtml(WebBrowser browser, string html)
        {
            browser.SetValue(HtmlProperty, html);
        }
        // Private Methods (1) 

        private static void OnHtmlChanged(DependencyObject obj,
                                          DependencyPropertyChangedEventArgs e)
        {
            var browser = (WebBrowser)obj;

            var html = e.NewValue
                        .AsString(true) ?? string.Empty;

            if (html.IsNullOrWhiteSpace())
            {
                browser.Navigate(new Uri("about:blank"));
            }
            else
            {
                browser.NavigateToString(html);
            }
        }

        #endregion Methods
    }
}
