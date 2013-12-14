// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Web;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.Services.Templates.Text.Html
{
    partial class DotLiquidHtmlTemplate
    {
        #region Nested Classes (1)

        private static class CommonFilters
        {
            #region Methods (5)

            // Public Methods (5) 

            public static string encode_html(object input)
            {
                return HttpUtility.HtmlEncode(input.AsString(true) ?? string.Empty);
            }

            public static string encode_html_attrib(object input)
            {
                return HttpUtility.HtmlAttributeEncode(input.AsString(true) ?? string.Empty);
            }

            public static string encode_js(object input)
            {
                return HttpUtility.JavaScriptStringEncode(input.AsString(true) ?? string.Empty);
            }

            public static string encode_json(object input)
            {
                return ServiceLocator.Current
                                     .GetInstance<ISerializer>()
                                     .ToJson(input) ?? "null";
            }

            public static string encode_url(object input)
            {
                return HttpUtility.UrlEncode(input.AsString(true) ?? string.Empty);
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
