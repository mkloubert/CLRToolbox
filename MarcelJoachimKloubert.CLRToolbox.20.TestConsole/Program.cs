using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox._20.TestConsole
{
    class Program
    {
        #region Methods (1)

        // Private Methods (1) 

        static void Main(string[] args)
        {
            IniFileConfigRepository repo = new IniFileConfigRepository("./my.ini");

            Console.WriteLine("===== ENTER =====");
            Console.ReadLine();
        }

        #endregion Methods
    }
}
