// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.ApplicationServer.Services.Net.Http.Modules
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.IHttpModuleLocator))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class HttpModuleLocator : HttpModuleLocatorBase
    {
        #region Fields (1)

        private readonly AppServerImpl _SERVER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal HttpModuleLocator(AppServerImpl server)
        {
            this._SERVER = server;
        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override IEnumerable<IHttpModule> OnGetAllModules()
        {
            return ServiceLocator.Current
                                 .GetAllInstances<IHttpModule>()
                                 .Concat(this._SERVER    // from modules
                                             .Modules
                                             .Select(m => m.Context)
                                             .SelectMany(c => c.GetAllInstances<IHttpModule>()));
        }

        #endregion Methods
    }
}
