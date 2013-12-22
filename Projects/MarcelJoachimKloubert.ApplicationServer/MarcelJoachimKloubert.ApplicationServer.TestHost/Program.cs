// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Threading.Tasks;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using MarcelJoachimKloubert.CLRToolbox.IO;

namespace MarcelJoachimKloubert.ApplicationServer.TestHost
{
    internal static class Program
    {
        #region Methods (5)

        // Private Methods (5) 

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleUnobservedException(e.ExceptionObject as Exception);
        }

        private static void HandleUnobservedException(global::System.Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            // TODO
        }

        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            var loggerFuncs = new DelegateLogger();
            loggerFuncs.Add(WriteLogMessageToConsole);

            var logger = new AggregateLogger();
            logger.Add(loggerFuncs);

            try
            {
                using (var server = new ApplicationServer())
                {
                    if (!server.IsInitialized)
                    {
                        GlobalConsole.Current.WriteLine("Initializing server... ");

                        var srvCtx = new SimpleAppServerContext(server);

                        var initCtx = new SimpleAppServerInitContext();
                        initCtx.Arguments = args;
                        initCtx.Logger = logger;
                        initCtx.ServerContext = srvCtx;

                        foreach (var a in args)
                        {
                            if (a.ToLower().Trim().StartsWith("/rootdir:"))
                            {
                                // custom root/working directory
                                initCtx.WorkingDirectory = new DirectoryInfo(a.Substring(a.IndexOf(':') + 1)
                                                                              .TrimStart()).FullName;
                            }
                        }

                        server.Initialize(initCtx);
                        GlobalConsole.Current.WriteLine("Server has been initialized.");

                        srvCtx.InnerServiceLocator = server.GlobalServiceLocator;
                    }

                    GlobalConsole.Current.WriteLine("Starting server... ");
                    server.Start();
                    GlobalConsole.Current.WriteLine("Server has been started.");

                    GlobalConsole.Current.WriteLine();
                    GlobalConsole.Current.WriteLine("===== ENTER to quit... =====");
                    GlobalConsole.Current.ReadLine();

                    GlobalConsole.Current.Write("Shutting down server... ");
                }
                GlobalConsole.Current.WriteLine("[OK]");

                return 0;
            }
            catch (Exception ex)
            {
                GlobalConsole.Current
                             .WriteLine(ex.GetBaseException() ?? ex);

                return 1;
            }
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleUnobservedException(e.Exception);
            e.SetObserved();
        }

        private static void WriteLogMessageToConsole(ILogMessage msg)
        {
            var oldTextColor = GlobalConsole.Current.ForegroundColor;
            var oldBackColor = GlobalConsole.Current.BackgroundColor;

            try
            {
#if !DEBUG
                if (msg.Categories.Contains(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.LoggerFacadeCategories.Debug))
                {
                    // the message is for debug mode only
                    return;
                }

#endif
                var textColor = ConsoleColor.Gray;
                ConsoleColor? backColor = null;
                object objToWrite = msg.Message;

                if (msg.Categories.Contains(LoggerFacadeCategories.FatalErrors))
                {
                    textColor = ConsoleColor.Yellow;
                    backColor = ConsoleColor.Red;
                }
                else if (msg.Categories.Contains(LoggerFacadeCategories.Errors))
                {
                    textColor = ConsoleColor.Red;
                }
                else if (msg.Categories.Contains(LoggerFacadeCategories.Warnings))
                {
                    textColor = ConsoleColor.Yellow;
                }
                else if (msg.Categories.Contains(LoggerFacadeCategories.Information))
                {
                    textColor = ConsoleColor.White;
                }

                GlobalConsole.Current.ForegroundColor = textColor;
                GlobalConsole.Current.BackgroundColor = backColor;

                GlobalConsole.Current.WriteLine(objToWrite);
            }
            finally
            {
                GlobalConsole.Current.ForegroundColor = oldTextColor;
                GlobalConsole.Current.BackgroundColor = oldBackColor;
            }
        }

        #endregion Methods
    }
}
