// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;

namespace MarcelJoachimKloubert.AppServer.Web.Common.Modules
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.IHttpModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal sealed class test : WebInterfaceModuleBase
    {
        #region Constructors (1)

        internal test()
            : base(new Guid("{DD9AA757-FBC8-423B-9839-AEFCBA9F818A}"))
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnHandleRequest(IHandleRequestContext context)
        {

        }

        #endregion Methods
    }
}
