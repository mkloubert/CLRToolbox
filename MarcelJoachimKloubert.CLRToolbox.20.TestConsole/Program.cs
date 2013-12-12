using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox._20.TestConsole
{
    class Program
    {
        #region Methods (1)

        // Private Methods (1) 

        static void Main(string[] args)
        {
            var test = new List<int> { 3, 1, 2 }.ToArray();

            CollectionHelper.Sort(test, i => i.ToString());
            foreach (var i in test)
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }

        #endregion Methods
    }
}
