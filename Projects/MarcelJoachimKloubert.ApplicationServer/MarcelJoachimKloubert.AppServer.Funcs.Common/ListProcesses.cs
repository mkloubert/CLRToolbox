// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;

namespace MarcelJoachimKloubert.AppServer.Funcs.Common
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Execution.Functions.IFunction))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal sealed class ListProcesses : CommonFunctionBase
    {
        #region Constructors (1)

        internal ListProcesses()
            : base(id: new Guid("{0F4EC862-D337-46E1-BB6D-A54734DF0D7B}"))
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnExecute(FunctionBase.OnExecuteContext context)
        {

        }

        #endregion Methods
    }
}
