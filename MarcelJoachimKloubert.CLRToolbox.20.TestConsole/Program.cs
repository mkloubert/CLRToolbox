using System;

namespace MarcelJoachimKloubert.CLRToolbox._20.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TMConsole.Out = delegate(string txt, ConsoleColor? fc, ConsoleColor? bg)
            {
                Console.WriteLine();
            };

            TMConsole.Write("wurst");

            Console.ReadLine();
        }
    }
}
