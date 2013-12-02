// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.ApplicationServer.Services.Execution.Functions
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Execution.Functions.IFunctionLocator))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class FunctionLocator : FunctionLocatorBase
    {
        #region Fields (1)

        private readonly AppServerImpl _SERVER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal FunctionLocator(AppServerImpl server)
        {
            this._SERVER = server;
        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override IEnumerable<IFunction> OnGetAllFunctions()
        {
            return ServiceLocator.Current
                                 .GetAllInstances<IFunction>()
                                 .Concat(this._SERVER    // from modules
                                             .Modules
                                             .Select(m => m.Context)
                                             .SelectMany(c => c.GetAllInstances<IFunction>()));
        }

        #endregion Methods
    }
}
