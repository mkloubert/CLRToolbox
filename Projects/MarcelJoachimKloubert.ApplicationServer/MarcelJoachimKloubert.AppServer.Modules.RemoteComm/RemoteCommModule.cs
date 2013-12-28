// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm
{
    /// <summary>
    /// Remote Application Server Communicatior module.
    /// </summary>
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.Modules.IAppServerModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class RemoteCommModule : AppServerModuleBase
    {
        #region Fields (1)

        private RequestHandler _currentHandler;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteCommModule" /> class.
        /// </summary>
        public RemoteCommModule()
            : base(id: new Guid("{EF712F5C-9B6C-4C23-9243-A23D816E6C0B}"))
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
            get { return "remote_comm"; }
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
            return "Remote Application Server Communicator";
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
            this.DisposeCurrentHandler();

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
                                       defaultVal: 23979);

                newServer.Port = port ?? 23979;
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
            this.DisposeCurrentHandler();
        }
        // Private Methods (1) 

        private void DisposeCurrentHandler()
        {
            try
            {
                using (var h = this._currentHandler)
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
