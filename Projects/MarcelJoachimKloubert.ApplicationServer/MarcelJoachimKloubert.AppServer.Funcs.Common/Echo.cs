// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

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

        #region Methods (2)

        // Protected Methods (1) 

        protected override void OnExecute(FunctionBase.OnExecuteContext context)
        {
            foreach (var item in context.InputParameters)
            {
                var value = item.Value;
                switch (LettersAndDigitsOnly(value))
                {
                    case "HALLOECHO":
                        value = "Hallo, Ottoooooooooo!!!";
                        break;
                }

                context.ResultParameters[item.Key] = value;
            }
        }
        // Private Methods (1) 

        private static string LettersAndDigitsOnly(object obj)
        {
            var str = obj.AsString(true);
            if (str == null)
            {
                return null;
            }

            return new string(str.Select(c => char.ToUpper(c))
                                 .Where(c => char.IsLetterOrDigit(c))
                                 .ToArray());
        }

        #endregion Methods
    }
}
