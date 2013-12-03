// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Text.RegularExpressions;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;

namespace MarcelJoachimKloubert.ApplicationServer.WebInterface
{
    internal sealed class UriHandler
    {
        #region Fields (2)

        private readonly HandlerAction _HANDLER;
        private readonly Regex _REGEX;

        #endregion Fields

        #region Constructors (1)

        internal UriHandler(Regex regex, HandlerAction handler)
        {
            this._REGEX = regex;
            this._HANDLER = handler;
        }

        #endregion Constructors

        #region Delegates and Events (1)

        // Delegates (1) 

        internal delegate void HandlerAction(Match match, HttpRequestEventArgs e, ref bool found);

        #endregion Delegates and Events

        #region Methods (1)

        // Internal Methods (1) 

        internal void Handle(HttpRequestEventArgs e, ref bool found)
        {
            string addr = null;
            if (e.Request.Address != null)
            {
                addr = e.Request.Address.AbsolutePath;
            }

            var match = this._REGEX.Match(addr ?? string.Empty);
            if (match.Success)
            {
                this._HANDLER(match, e, ref found);
            }
        }

        #endregion Methods
    }
}
