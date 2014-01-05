// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net.Mime;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

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

        #region Properties (1)

        [Import]
        public AppServerImpl Server
        {
            get;
            protected set;
        }

        #endregion Properties

        #region Methods (3)

        // Protected Methods (3) 

        protected override void OnAfterHandleRequest(IAfterHandleRequestContext context)
        {
            if (!context.Http.Response.DirectOutput)
            {
                Stream jsStream = null;
                this.OnTryGetResourceStream(string.Format("Modules.js.{0}.js",
                                                          this.Name), ref jsStream);

                if (jsStream != null)
                {
                    using (jsStream)
                    {
                        using (var reader = new StreamReader(jsStream, Encoding.UTF8))
                        {
                            context.Http.Response
                                        .WriteJavaScript(reader.ReadToEnd());
                        }
                    }
                }

                if (!context.Http.Response.Compress.HasValue)
                {
                    context.Http.Response.Compress = true;
                }
            }
        }

        protected override void OnBeforeHandleRequest(IBeforeHandleRequestContext context)
        {
            context.Http
                   .Response
                   .ContentType = MediaTypeNames.Text.Html;

            context.Http
                   .Response
                   .Charset = Encoding.UTF8;
        }

        protected override void OnTryGetResourceStream(string resName, ref Stream stream)
        {
            stream = this.GetType()
                         .Assembly
                         .GetManifestResourceStream(string.Format("MarcelJoachimKloubert.AppServer.Web.Common.Resources.{0}",
                                                                  resName));
        }

        #endregion Methods
    }
}
