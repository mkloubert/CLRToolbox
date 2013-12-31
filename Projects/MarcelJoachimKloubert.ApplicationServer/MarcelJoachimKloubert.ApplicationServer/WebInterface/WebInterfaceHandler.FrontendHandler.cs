// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.Templates.Text.Html;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    partial class WebInterfaceHandler
    {
        #region Nested Classes (1)

        private sealed class FrontendHandler
        {
            #region Fields (2)

            internal readonly IHttpRequest REQUEST;
            internal readonly IHttpResponse RESPONSE;

            #endregion Fields

            #region Constructors (1)

            internal FrontendHandler(IHttpRequest req, IHttpResponse resp)
            {
                this.REQUEST = req;
                this.RESPONSE = resp;
            }

            #endregion Constructors

            #region Methods (1)

            // Internal Methods (1) 

            internal void WriteVars(IHtmlTemplate tpl)
            {
                tpl["current_year"] = this.REQUEST.Time.Year;

                foreach (var v in this.RESPONSE.FrontendVars)
                {
                    tpl[v.Key] = v.Value;
                }
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
