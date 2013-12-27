// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.AppServer.Modules.DocDB
{
    /// <summary>
    /// Document based storage module.
    /// </summary>
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.Modules.IAppServerModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class DocDBModule : AppServerModuleBase
    {
        #region Fields (1)

        private RequestHandler _currentHandler;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="DocDBModule" /> class.
        /// </summary>
        public DocDBModule()
            : base(id: new Guid("{FCD26B40-CAC9-4513-90A0-89CB36EF6B71}"))
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.Name" />.
        public override string Name
        {
            get { return "docdb"; }
        }

        #endregion Properties

        #region Methods (5)

        // Protected Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnGetDisplayName(CultureInfo)" />
        protected override IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return "Document Database";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnInitialize(IAppServerModuleInitContext, ref bool)" />
        protected override void OnInitialize(IAppServerModuleInitContext initContext,
                                             ref bool isInitialized)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnStart(AppServerModuleBase.StartStopContext, ref bool)" />
        protected override void OnStart(AppServerModuleBase.StartStopContext context, ref bool isRunning)
        {
            this.DisposeCurrentServer();

            var newServer = ServiceLocator.Current.GetInstance<IHttpServer>();

            // HTTPs?
            {
                bool? useHttps;
                this.Context
                    .Config
                    .TryGetValue<bool?>(category: "service",
                                        name: "use_https",
                                        value: out useHttps,
                                        defaultVal: false);

                newServer.UseSecureHttp = useHttps ?? false;
            }

            // port
            {
                int? port;
                this.Context
                    .Config
                    .TryGetValue<int?>(category: "service",
                                       name: "port",
                                       value: out port,
                                       defaultVal: 1781);

                newServer.Port = port ?? 1781;
            }


            if (newServer.UseSecureHttp)
            {
                // SSL thumbprint
                {
                    newServer.SetSslCertificateByThumbprint(this.Context
                                                                .Config
                                                                .GetValue<IEnumerable<char>>(category: "service",
                                                                                             name: "ssl_thumbprint"));
                }
            }

            var newHandler = new RequestHandler(this, newServer);
            try
            {
                newHandler.Start();
                this._currentHandler = newHandler;
            }
            catch
            {
                newHandler.Dispose();

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnStart(AppServerModuleBase.StartStopContext, ref bool)" />
        protected override void OnStop(AppServerModuleBase.StartStopContext context, ref bool isRunning)
        {
            this.DisposeCurrentServer();
        }
        // Private Methods (1) 

        private void DisposeCurrentServer()
        {
            try
            {
                using (var handler = this._currentHandler)
                {
                    this._currentHandler = null;
                }
            }
            catch
            {
                // ignore
            }
        }

        #endregion Methods
    }
}
