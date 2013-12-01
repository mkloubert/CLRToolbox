// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;

namespace MarcelJoachimKloubert.AppServer.Web.Common
{
    internal abstract class WebInterfaceModuleBase : HttpModuleBase
    {
        #region Constructors (2)

        protected WebInterfaceModuleBase(Guid id, object syncRoot)
            : base(id, syncRoot)
        {

        }

        protected WebInterfaceModuleBase(Guid id)
            : base(id)
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnBeforeHandleRequest(IBeforeHandleRequestContext context)
        {
            context.HttpRequest
                   .Response
                   .ContentType = "text/html";

            context.HttpRequest
                   .Response
                   .Charset = Encoding.UTF8;
        }

        #endregion Methods
    }
}
