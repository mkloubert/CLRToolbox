// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial interface IConsole
    {
        #region Operations (1)

        /// <summary>
        /// Reads data as secure string from standard input.
        /// </summary>
        /// <returns>The read password.</returns>
        string ReadPassword();

        #endregion Operations
    }
}
