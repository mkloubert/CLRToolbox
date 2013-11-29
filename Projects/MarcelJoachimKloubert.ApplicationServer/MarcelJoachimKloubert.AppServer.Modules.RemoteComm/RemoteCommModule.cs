// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using MarcelJoachimKloubert.ApplicationServer.Modules;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm
{
    /// <summary>
    /// Remote Application Server Communicatior module.
    /// </summary>
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.Modules.IAppServerModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class RemoteCommModule : AppServerModuleBase
    {
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
            get { return "remote_server_communicator"; }
        }

        #endregion Properties

        #region Methods (3)

        // Protected Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnGetDisplayName(CultureInfo)" />
        protected override IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return "Remote Application Server Communicatior";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnStart(AppServerModuleBase.StartStopContext, ref bool)" />
        protected override void OnStart(AppServerModuleBase.StartStopContext context, ref bool isRunning)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnStart(AppServerModuleBase.StartStopContext, ref bool)" />
        protected override void OnStop(AppServerModuleBase.StartStopContext context, ref bool isRunning)
        {

        }

        #endregion Methods
    }
}
