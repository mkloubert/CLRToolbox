// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Net.Mime;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm.WebInterface
{
    internal abstract class WebModuleBase : HttpModuleBase
    {
        #region Constructors (2)

        protected WebModuleBase(Guid id, object syncRoot)
            : base(id, syncRoot)
        {

        }

        protected WebModuleBase(Guid id)
            : base(id)
        {

        }

        #endregion Constructors

        #region Properties (1)

        [Import]
        public RemoteCommModule Module
        {
            get;
            protected set;
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnBeforeHandleRequest(IBeforeHandleRequestContext context)
        {
            context.HttpRequest
                   .Response
                   .ContentType = MediaTypeNames.Text.Html;

            context.HttpRequest
                   .Response
                   .Charset = Encoding.UTF8;
        }

        #endregion Methods
    }
}
