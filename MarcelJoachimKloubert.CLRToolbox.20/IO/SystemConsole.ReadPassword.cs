// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Security;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class SystemConsole
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.OnReadPassword(ref SecureString)" />
        protected override void OnReadPassword(ref SecureString pwd)
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = global::System.Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (keyInfo.Key != ConsoleKey.Backspace)
                {
                    // append char

                    pwd.AppendChar(keyInfo.KeyChar);
                    this.Write("*");
                }
                else
                {
                    // remove last char

                    if (pwd.Length > 0)
                    {
                        pwd.RemoveAt(pwd.Length - 1);
                        this.Write("\b \b");
                    }
                }
            }
        }

        #endregion Methods
    }
}
