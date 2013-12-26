using System.Collections.Generic;

namespace MarcelJoachimKloubert.ApplicationServer.TestHost.Menus
{
    public interface IMenuHandler
    {
        #region Data Members (1)

        bool WaitsForInput { get; }

        #endregion Data Members

        #region Operations (2)

        void DrawMenu();

        bool HandleInput(IEnumerable<char> input, out IMenuHandler nextHandler);

        #endregion Operations
    }
}
