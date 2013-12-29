// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.IO;
using AppSrvImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.ApplicationServer.WinService
{
    /// <summary>
    /// The main service.
    /// </summary>
    public partial class MainService : ServiceBase
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainService"/> class.
        /// </summary>
        public MainService()
        {
            this.InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += this.TaskScheduler_UnobservedTaskException;
        }

        #endregion Constructors

        #region Properties (2)

        public string LogDirectory
        {
            get;
            private set;
        }

        public AppSrvImpl Server
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (6)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceBase.OnStart(string[])" />
        protected override void OnStart(string[] args)
        {
            var asmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var workDir = asmDir;
            foreach (var a in args)
            {
                if (a.ToLower().Trim().StartsWith("/rootdir:"))
                {
                    // custom root/working directory
                    var dir = new DirectoryInfo(a.Substring(a.IndexOf(':') + 1)
                                                 .TrimStart()).FullName;

                    if (Path.IsPathRooted(dir))
                    {
                        workDir = new DirectoryInfo(dir).CreateDirectoryDeep().FullName;
                    }
                    else
                    {
                        workDir = new DirectoryInfo(Path.Combine(asmDir,
                                                                 dir)).CreateDirectoryDeep().FullName;
                    }
                }
            }

            this.EventLog
                .WriteEntry(string.Format("Root directory is: {0}",
                                          workDir),
                            EventLogEntryType.Information);

            this.LogDirectory = new DirectoryInfo(Path.Combine(workDir,
                                                               "logs")).CreateDirectoryDeep()
                                                                       .FullName;

            this.EventLog
                .WriteEntry(string.Format("Log directory is: {0}",
                                          this.LogDirectory),
                            EventLogEntryType.Information);

            GlobalConsole.SetConsole(new ServiceConsole(this));

            var loggerFuncs = new DelegateLogger();
            loggerFuncs.Add(this.WriteEventLogMessage);

            var logger = new AggregateLogger();
            logger.Add(new AsyncLogger(loggerFuncs));

            var server = new ApplicationServer();
            try
            {
                if (!server.IsInitialized)
                {
                    var srvCtx = new SimpleAppServerContext(server);

                    var initCtx = new SimpleAppServerInitContext();
                    initCtx.Arguments = args;
                    initCtx.Logger = logger;
                    initCtx.ServerContext = srvCtx;
                    initCtx.WorkingDirectory = workDir;

                    try
                    {
                        server.Initialize(initCtx);

                        this.EventLog
                            .WriteEntry("Server has been initialized.",
                                        EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        this.EventLog
                            .WriteEntry(string.Format("Server could not be initialized!{0}{0}{1}",
                                                      Environment.NewLine,
                                                      ex.GetBaseException() ?? ex),
                                        EventLogEntryType.FailureAudit);

                        throw;
                    }

                    srvCtx.InnerServiceLocator = server.GlobalServiceLocator;
                }

                try
                {
                    server.Start();

                    this.EventLog
                        .WriteEntry("Server has been started.",
                                    EventLogEntryType.SuccessAudit);
                }
                catch (Exception ex)
                {
                    this.EventLog
                        .WriteEntry(string.Format("Server could not be started!{0}{0}{1}",
                                                  Environment.NewLine,
                                                  ex.GetBaseException() ?? ex),
                                    EventLogEntryType.FailureAudit);

                    throw;
                }

                this.Server = server;
            }
            catch
            {
                server.Dispose();

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceBase.OnStop()" />
        protected override void OnStop()
        {
            using (var server = this.Server)
            {
                this.Server = null;
            }
        }
        // Private Methods (4) 

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.HandleUnobservedException(e.ExceptionObject as Exception);
        }

        private void HandleUnobservedException(global::System.Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            this.EventLog
                .WriteEntry(string.Format("Unhandled exception:{0}{0}{1}",
                                          Environment.NewLine,
                                          ex.GetBaseException() ?? ex),
                            EventLogEntryType.Error);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            this.HandleUnobservedException(e.Exception);
            e.SetObserved();
        }

        private void WriteEventLogMessage(ILogMessage msg)
        {
#if !DEBUG
                if (msg.Categories.Contains(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.LoggerFacadeCategories.Debug))
                {
                    // the message is for debug mode only
                    return;
                }

#endif
            EventLogEntryType? type = null;
            if (msg.Categories.Contains(LoggerFacadeCategories.FatalErrors))
            {
                type = EventLogEntryType.Error;
            }
            else if (msg.Categories.Contains(LoggerFacadeCategories.Errors))
            {
                type = EventLogEntryType.FailureAudit;
            }
            else if (msg.Categories.Contains(LoggerFacadeCategories.Warnings))
            {
                type = EventLogEntryType.Warning;
            }
            else if (msg.Categories.Contains(LoggerFacadeCategories.Information))
            {
                type = EventLogEntryType.Information;
            }

            var eventMsg = string.Format(string.Format("CATEGORIES: {0}{1}TAG: {2}{1}{1}MESSAGE:{1}{3}",
                                                       string.Join(", ",
                                                                   msg.Categories),
                                                       Environment.NewLine,
                                                       msg.LogTag,
                                                       msg.Message.AsString(true)));

            if (type.HasValue)
            {
                this.EventLog
                    .WriteEntry(eventMsg,
                                type.Value);
            }
            else
            {
                this.EventLog
                    .WriteEntry(eventMsg);
            }
        }

        #endregion Methods
    }
}
