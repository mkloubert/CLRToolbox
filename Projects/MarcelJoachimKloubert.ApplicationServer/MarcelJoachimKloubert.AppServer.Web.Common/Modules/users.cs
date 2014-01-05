// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;

namespace MarcelJoachimKloubert.AppServer.Web.Common.Modules
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.IHttpModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal sealed class users : WebInterfaceModuleBase
    {
        #region Constructors (1)

        internal users()
            : base(new Guid("{26C780E7-E554-449F-8D05-5C0790E5EEA3}"))
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
