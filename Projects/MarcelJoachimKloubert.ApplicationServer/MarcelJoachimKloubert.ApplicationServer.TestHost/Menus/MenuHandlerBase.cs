using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.IO;

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

        #region Methods (5)

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
        // Protected Methods (3) 

        protected void ExecuteAnWaitOnError(Action action)
        {
            this.ExecuteAnWaitOnError<object>((s) => action(),
                                              null);
        }

        protected void ExecuteAnWaitOnError<T>(Action<T> action, T state)
        {
            try
            {
                action(state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException() ?? ex);

                GlobalConsole.Current.ReadLine();
            }
        }

        protected abstract void OnHandleInput(string input,
                                              ref IMenuHandler nextHandler,
                                              ref bool isValid);

        #endregion Methods
    }
}
