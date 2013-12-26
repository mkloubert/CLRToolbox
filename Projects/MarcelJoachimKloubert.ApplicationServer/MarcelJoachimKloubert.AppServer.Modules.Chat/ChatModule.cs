// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using MarcelJoachimKloubert.ApplicationServer.Modules;

namespace MarcelJoachimKloubert.AppServer.Modules.Chat
{
    /// <summary>
    /// Chat server module.
    /// </summary>
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.Modules.IAppServerModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class ChatModule : AppServerModuleBase
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatModule" /> class.
        /// </summary>
        public ChatModule()
            : base(id: new Guid("{E6356629-0227-4943-B0A2-96A434811EE1}"))
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
            get { return "chat"; }
        }

        #endregion Properties

        #region Methods (4)

        // Protected Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnGetDisplayName(CultureInfo)" />
        protected override IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return "Chat Server Module";
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
