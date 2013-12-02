// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;

namespace MarcelJoachimKloubert.AppServer.Funcs.Common
{
    internal abstract class CommonFunctionBase : FunctionBase
    {
        #region Constructors (2)

        protected CommonFunctionBase(Guid id, object syncRoot)
            : base(id, syncRoot)
        {

        }

        protected CommonFunctionBase(Guid id)
            : base(id)
        {

        }

        #endregion Constructors

        #region Properties (1)

        public override string Namespace
        {
            get
            {
                return "MarcelJoachimKloubert.Common";
            }
        }

        #endregion Properties
    }
}
