using System;
using System.Threading;
using MarcelJoachimKloubert.CLRToolbox.Caching;
using MarcelJoachimKloubert.CLRToolbox.IO;

namespace MarcelJoachimKloubert.CLRToolbox._20.TestConsole
{
    class Program
    {
        #region Methods (2)

        // Private Methods (2) 

        private static DateTimeOffset GetNow()
        {
            return DateTimeOffset.Now;
        }

        static void Main(string[] args)
        {
            // IniFileConfigRepository repo = new IniFileConfigRepository("./my.ini");

            // GlobalConsole.Current.WriteLine(StringHelper.UppercaseWords("Lorem  ipsUm dolor sit amet,consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."));

            DelegateCache c = new DelegateCache();
            c.SaveFunc(GetNow);

            long i = 0;
            while (true)
            {
                DateTimeOffset now = c.InvokeFunc(GetNow);
                GlobalConsole.Current.WriteLine(now);

                Thread.Sleep(1000);
                ++i;

                if ((i % 21) == 0)
                {
                    Console.WriteLine("!!!SAVE (5)!!!");
                    c.SaveFunc(GetNow, TimeSpan.FromSeconds(5));
                }
                else if ((i % 14) == 0)
                {
                    Console.WriteLine("!!!SAVE (3)!!!");
                    c.SaveFunc(GetNow, TimeSpan.FromSeconds(3));
                }
                else if ((i % 7) == 0)
                {
                    Console.WriteLine("!!!RESET!!!");
                    c.ResetFunc(GetNow);
                }
            }

            GlobalConsole.Current.WriteLine("===== ENTER =====");
            GlobalConsole.Current.ReadLine();
        }

        #endregion Methods
    }
}
