// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;
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
            var l = ServiceLocator.Current.GetInstance<IFunctionLocator>();

            var func = l.GetAllFunctions().Single(f => f.Name == "Echo");

            var p = new Dictionary<string, object>()
                {
                    { "a", 1 },
                };

            var r = func.Execute(p, false);

            r.Start();

            var a = r.ResultParameters["A"];
        }

        #endregion Methods
    }
}
