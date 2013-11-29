// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Common implementation of <see cref="IAppServer" /> interface.
    /// </summary>
    public class ApplicationServer : AppServerBase
    {
        #region Properties (2)

        /// <summary>
        /// Gets the current list of modules.
        /// </summary>
        public IAppServerModule[] Modules
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get { return Environment.CurrentDirectory; }
        }

        #endregion Properties

        #region Methods (3)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnStart(AppServerBase.StartStopContext, ref bool)" />
        protected override void OnStart(AppServerBase.StartStopContext context, ref bool isRunning)
        {
            switch (context)
            {
                case StartStopContext.Start:
                    this.StartServer();
                    break;

                case StartStopContext.Restart:
                    var moduleList = this.Modules;
                    if (moduleList != null)
                    {
                        foreach (var module in moduleList.Where(m => m.CanRestart))
                        {
                            module.Restart();
                        }
                    }
                    else
                    {
                        this.StartServer();
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerBase.OnStop(AppServerBase.StartStopContext, ref bool)" />
        protected override void OnStop(AppServerBase.StartStopContext context, ref bool isRunning)
        {
            var moduleList = this.Modules;
            if (moduleList != null)
            {
                foreach (var module in moduleList.Where(m => m.CanStop))
                {
                    module.Stop();
                }
            }
        }
        // Private Methods (1) 

        private void StartServer()
        {
            var moduleList = this.Modules;
            if (moduleList == null)
            {
                return;
            }

            foreach (var dispModule in moduleList.OfType<IDisposable>())
            {
                var doDispose = true;

                var dispModuleEx = dispModule as ITMDisposable;
                if (dispModuleEx != null)
                {
                    doDispose = !dispModuleEx.IsDisposed;
                }

                if (doDispose)
                {
                    dispModule.Dispose();
                }
            }
        }

        #endregion Methods
    }
}
