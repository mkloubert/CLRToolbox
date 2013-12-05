// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using MarcelJoachimKloubert.ApplicationServer.Modules;
using MonoTorrent.Client;

namespace MarcelJoachimKloubert.AppServer.Modules.Torrent
{
    /// <summary>
    /// Torrent module.
    /// </summary>
    [Export(typeof(global::MarcelJoachimKloubert.ApplicationServer.Modules.IAppServerModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class TorrentModule : AppServerModuleBase
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TorrentModule" /> class.
        /// </summary>
        public TorrentModule()
            : base(id: new Guid("{D33B3EA6-98E6-4ECB-8A2B-A60FD853B9BF}"))
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
            get { return "torrent"; }
        }

        #endregion Properties

        #region Methods (4)

        private ClientEngine _torrentClient;

        public ClientEngine TorrentClient
        {
            get { return this._torrentClient; }

            private set
            {
                this.OnPropertyChanging(() => this.TorrentClient);
                this._torrentClient = value;
                this.OnPropertyChanged(() => this.TorrentClient);
            }
        }

        // Protected Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnGetDisplayName(CultureInfo)" />
        protected override IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return "Torrent Module";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnInitialize(IAppServerModuleInitContext, ref bool)" />
        protected override void OnInitialize(IAppServerModuleInitContext initContext,
                                             ref bool isInitialized)
        {

        }

        private void DisposeOldTrackerClient()
        {
            using (var client = this.TorrentClient)
            {
                this.TorrentClient = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnStart(AppServerModuleBase.StartStopContext, ref bool)" />
        protected override void OnStart(AppServerModuleBase.StartStopContext context, ref bool isRunning)
        {
            // client
            //TODO
            /*
            {
                var port = 11111;

                // Create the settings which the engine will use
                // downloadsPath - this is the path where we will save all the files to
                // port - this is the port we listen for connections on
                var settings = new EngineSettings();
                settings.PreferEncryption = false;
                settings.AllowedEncryption = EncryptionTypes.All;

                // Create the default settings which a torrent will have.
                // 4 Upload slots - a good ratio is one slot per 5kB of upload speed
                // 50 open connections - should never really need to be changed
                // Unlimited download speed - valid range from 0 -> int.Max
                // Unlimited upload speed - valid range from 0 -> int.Max
                var torrentDefaults = new TorrentSettings(4, 150, 0, 0);

                var newClient = new ClientEngine(settings);
                newClient.ChangeListenEndpoint(new IPEndPoint(IPAddress.Any, port));

                var torrent = TorrentFile.Load(@"E:\Dev\debian-7.2.0-i386-netinst.iso.torrent");

                var manager = new TorrentManager(torrent,
                                                 @"E:\Dev\debian-7.2.0-i386-netinst.iso",
                                                 torrentDefaults);

                long test = 0;
                manager.PieceManager.BlockReceived += (sender, e) =>
                    {
                        ++test;
                        if (e != null)
                        {
                            GlobalConsole.Current
                                         .WriteLine(torrent.Files[0].BitField.PercentComplete);
                        }
                    };

                // Every time a piece is hashed, this is fired.
                manager.PeersFound += (sender, e) =>
                    {
                        if (e != null)
                        {

                        }
                    };

                // Every time the state changes (Stopped -> Seeding -> Downloading -> Hashing) this is fired
                manager.TorrentStateChanged += (sender, e) =>
                    {
                        if (e != null)
                        {

                        }
                    };

                newClient.Register(manager);

                newClient.StartAll();
                this.TorrentClient = newClient;
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AppServerModuleBase.OnStart(AppServerModuleBase.StartStopContext, ref bool)" />
        protected override void OnStop(AppServerModuleBase.StartStopContext context, ref bool isRunning)
        {
            this.DisposeOldTrackerClient();
        }

        #endregion Methods
    }
}
