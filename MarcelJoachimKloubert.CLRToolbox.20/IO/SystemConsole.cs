// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Wrapper for <see cref="Console" />.
    /// </summary>
    public partial class SystemConsole : ConsoleBase
    {
        #region Methods (3)

        // Protected Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.GetNewLineForOutput()" />
        protected override string GetNewLineForOutput()
        {
            return global::System.Console.Out.NewLine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.OnReadLine(TextWriter)" />
        protected override void OnReadLine(TextWriter line)
        {
            line.Write(global::System.Console.ReadLine());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.OnWrite(string)" />
        protected override void OnWrite(string text)
        {
            global::System.Console.Write(text);
        }

        #endregion Methods
    }
}
