// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Linq;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.ScriptEngine.ECMA;

namespace MarcelJoachimKloubert.ScriptEngine
{
    internal static partial class Program
    {
        #region Methods (1)

        // Private Methods (1) 

        [STAThread]
        private static int Main(string[] args)
        {
            int result;

            try
            {
                result = 0;

                using (var executor = EcmaScriptExecutor.CreateCommonInstance())
                {
#if DEBUG
                    bool debug = true;
#else
                    bool debug = false;
#endif

                    // options
                    foreach (var a in args)
                    {
                        switch ((a ?? string.Empty).ToLower().Trim())
                        {
                            case "/debug":
                                debug = true;
                                break;

                            case "/nodebug":
                                debug = false;
                                break;
                        }
                    }

                    Console.WriteLine("Do it....");
                    Console.ReadLine();

                    TMApplication.Restart();

                    foreach (var a in args.Select(arg => arg.TrimStart()))
                    {
                        try
                        {
                            if (a.StartsWith("/"))
                            {
                                continue;
                            }

                            var file = new FileInfo(a);
                            var src = File.ReadAllText(file.FullName, Encoding.UTF8);

                            executor.Execute(src,
                                             true,
                                             debug);
                        }
                        catch (Exception ex)
                        {
                            // at least one script execution has been failed

                            result = -2;
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // unexpected global error

                result = -1;
                Console.WriteLine(ex);
            }

            return result;
        }

        #endregion Methods
    }
}
