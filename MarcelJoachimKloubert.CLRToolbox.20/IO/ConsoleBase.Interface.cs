// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class ConsoleBase
    {
        #region Methods (8)

        // Private Methods (8) 

        IConsole IConsole.Clear()
        {
            return this.Clear();
        }

        IConsole IConsole.Write(object obj)
        {
            return this.Write(obj);
        }

        IConsole IConsole.Write(IEnumerable<char> chars)
        {
            return this.Write(chars);
        }

        IConsole IConsole.Write(IEnumerable<char> format, params object[] args)
        {
            return this.Write(format, args);
        }

        IConsole IConsole.WriteLine()
        {
            return this.WriteLine();
        }

        IConsole IConsole.WriteLine(object obj)
        {
            return this.WriteLine(obj);
        }

        IConsole IConsole.WriteLine(IEnumerable<char> chars)
        {
            return this.WriteLine(chars);
        }

        IConsole IConsole.WriteLine(IEnumerable<char> format, params object[] args)
        {
            return this.WriteLine(format, args);
        }

        #endregion Methods
    }
}
