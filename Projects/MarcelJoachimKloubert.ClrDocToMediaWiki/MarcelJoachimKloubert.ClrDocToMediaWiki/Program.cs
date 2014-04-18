// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using DotNetWikiBot;
using MarcelJoachimKloubert.ClrDocToMediaWiki.Classes;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.IO;
using System;
using System.IO;
using System.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki
{
    internal static class Program
    {
        #region Fields (1)

        // Private Fields (1) 

        private static readonly object _SYNC_CONSOLE = new object();
        
        #endregion Fields

        #region Methods (3)

        // Private Methods (3) 

        private static IConsole InvokeForConsoleColor(this IConsole console,
                                                      Action<IConsole> action,
                                                      ConsoleColor? foreColor = null, ConsoleColor? bgColor = null)
        {
            return InvokeForConsoleColor<object>(console,
                                                 (c, state) => action(c),
                                                 actionState: null,
                                                 foreColor: foreColor, bgColor: bgColor);
        }

        private static IConsole InvokeForConsoleColor<T>(this IConsole console,
                                                         Action<IConsole, T> action,
                                                         T actionState,
                                                         ConsoleColor? foreColor = null, ConsoleColor? bgColor = null)
        {
            lock (_SYNC_CONSOLE)
            {
                var oldForeColor = console.ForegroundColor;
                var oldBgColor = console.BackgroundColor;

                try
                {
                    if (foreColor.HasValue)
                    {
                        console.ForegroundColor = foreColor.Value;
                    }

                    if (bgColor.HasValue)
                    {
                        console.BackgroundColor = bgColor.Value;
                    }

                    action(console, actionState);
                }
                finally
                {
                    console.ForegroundColor = oldForeColor;
                    console.BackgroundColor = oldBgColor;
                }
            }

            return console;
        }

        private static int Main(string[] args)
        {
            try
            {
                var configFile = new FileInfo(Path.Combine(Environment.CurrentDirectory,
                                                           "config.ini"));

                var ini = new IniFileConfigRepository(configFile);

                foreach (var settings in DocumentationSettings.FromConfig(ini)
                                                              .Where(ds => ds.IsActive))
                {
                    try
                    {
                        var site = new Site(settings.WikiUrl,
                                            settings.Username, settings.Password.ToUnsecureString());

                        var doc = AssemblyDocumentation.FromFile(new FileInfo(settings.AssemblyFile));
                        doc.UpdateSettings(settings);

                        var asmPageName = doc.GetWikiPageName();

                        var asmPage = new Page(site, asmPageName);
                        asmPage.LoadWithMetadata();
                        asmPage.ResolveRedirect();

                        asmPage.text = doc.ToMediaWiki();
                        asmPage.Save();
                    }
                    catch (Exception ex)
                    {
                        GlobalConsole.Current
                                     .InvokeForConsoleColor((c, state) => c.WriteLine(state.Exception),
                                                            new
                                                            {
                                                                Exception = ex.GetBaseException() ?? ex,
                                                            }, foreColor: ConsoleColor.Red
                                                             , bgColor: ConsoleColor.Black);
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                GlobalConsole.Current
                             .InvokeForConsoleColor((c, state) => c.WriteLine(state.Exception),
                                                    new
                                                    {
                                                        Exception = ex.GetBaseException() ?? ex,
                                                    }, foreColor: ConsoleColor.Yellow
                                                     , bgColor: ConsoleColor.Red);

                return 1;
            }
            finally
            {
#if DEBUG
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.WriteLine();
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.WriteLine();
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.WriteLine("===== ENTER ====");
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.ReadLine();
#endif
            }
        }

        #endregion Methods
    }
}