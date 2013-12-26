using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.ApplicationServer.TestHost.Menus
{
    abstract class MenuHandlerBase : IMenuHandler
    {
        #region Properties (1)

        public virtual bool WaitsForInput
        {
            get { return true; }
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (2) 

        public abstract void DrawMenu();

        public bool HandleInput(IEnumerable<char> input, out IMenuHandler nextHandler)
        {
            var result = true;
            nextHandler = this;

            this.OnHandleInput((StringHelper.AsString(input) ?? string.Empty).ToLower().Trim(),
                               ref nextHandler,
                               ref result);

            if (!result)
            {
                nextHandler = this;
            }

            return result;
        }
        // Protected Methods (1) 

        protected abstract void OnHandleInput(string input,
                                              ref IMenuHandler nextHandler,
                                              ref bool isValid);

        #endregion Methods
    }
}
