using System;
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

            Console.WriteLine(StringHelper.UppercaseWords("Lorem  ipsUm dolor sit amet,consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."));

            Console.WriteLine("===== ENTER =====");
            Console.ReadLine();
        }

        #endregion Methods
    }
}
