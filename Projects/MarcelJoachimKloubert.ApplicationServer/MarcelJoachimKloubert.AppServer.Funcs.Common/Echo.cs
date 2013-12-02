// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;

namespace MarcelJoachimKloubert.AppServer.Funcs.Common
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Execution.Functions.IFunction))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal sealed class Echo : CommonFunctionBase
    {
        #region Constructors (1)

        internal Echo()
            : base(new Guid("{6EBFF3D1-9F11-42B9-AC0A-7B85C23CE206}"))
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnExecute(FunctionBase.OnExecuteContext context)
        {
            foreach (var item in context.InputParameters)
            {
                context.ResultParameters[item.Key] = item.Value;
            }
        }

        #endregion Methods
    }
}
