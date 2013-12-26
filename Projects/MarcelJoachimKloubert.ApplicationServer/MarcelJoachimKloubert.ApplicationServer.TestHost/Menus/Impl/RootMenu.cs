using MarcelJoachimKloubert.ApplicationServer.TestHost.Menus.Impl.Modules;
using MarcelJoachimKloubert.CLRToolbox.IO;

namespace MarcelJoachimKloubert.ApplicationServer.TestHost.Menus.Impl
{
    sealed class RootMenu : MenuHandlerBase
    {
        #region Methods (2)

        // Public Methods (1) 

        public override void DrawMenu()
        {
            GlobalConsole.Current.WriteLine("[1] Modules");
            GlobalConsole.Current.WriteLine();
            GlobalConsole.Current.WriteLine("[x] Exit");
        }
        // Protected Methods (1) 

        protected override void OnHandleInput(string input,
                                              ref IMenuHandler nextHandler,
                                              ref bool isValid)
        {
            switch (input)
            {
                case "1":
                    nextHandler = new ModuleMenu();
                    break;

                case "x":
                    nextHandler = null;
                    break;

                default:
                    isValid = false;
                    break;
            }
        }

        #endregion Methods
    }
}
