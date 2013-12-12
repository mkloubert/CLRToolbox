// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Linq;
using MarcelJoachimKloubert.ApplicationServer.Data.SQLite;
using MarcelJoachimKloubert.ApplicationServer.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.AppServer.Web.Common.Modules
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.IHttpModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [DefaultHttpModule]
    internal sealed class index : WebInterfaceModuleBase
    {
        #region Constructors (1)

        internal index()
            : base(new Guid("{E4CF7189-6EC2-4704-8389-B23C40150133}"))
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnHandleRequest(IHandleRequestContext context)
        {
            //var l = ServiceLocator.Current.GetInstance<IFunctionLocator>();

            //var func = l.GetAllFunctions().Single(f => f.Name == "Echo");

            //var p = new Dictionary<string, object>()
            //    {
            //        { "a", 1 },
            //    };

            //var r = func.Execute(p, false);

            //r.Start();

            //var a = r.ResultParameters["A"];

            context.HttpRequest.Response.Write("<ul>");

            foreach (var ctx in this.Server
                                    .Modules
                                    .Select(m => m.Context)
                                    .Where(ctx => ctx != null)
                                    .OrderBy(ctx => ctx.Object.DisplayName))
            {
                var hash = ctx.GetWebHashAsHexString();

                context.HttpRequest
                       .Response
                       .Write("<li>")
                       .Write(string.Format(@"<a target=""_blank"" href=""{0}/"">",
                                            hash))
                       .Write(ctx.Object.DisplayName)
                       .Write("</a>")
                       .Write("</li>");
            }

            context.HttpRequest.Response.Write("</ul>");

            using (var db = ServiceLocator.Current.GetInstance<ISqliteDatabaseFactory>().OpenDatabase("wurst"))
            {

            }
        }

        #endregion Methods
    }
}
