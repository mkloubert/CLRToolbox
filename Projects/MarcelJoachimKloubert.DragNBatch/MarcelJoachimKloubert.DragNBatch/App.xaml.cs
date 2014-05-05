// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.DragNBatch.Windows;
using System;
using System.IO;
using System.Windows;

namespace MarcelJoachimKloubert.DragNBatch
{
    /// <summary>
    /// Code behind for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructors (1)

        private App()
        {
        }

        #endregion Constructors

        #region Properties (4)

        /// <summary>
        /// Gets the list of command line arguments.
        /// </summary>
        public string[] CommandLineArgs
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the app configuration.
        /// </summary>
        public IConfigRepository Config
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public static new App Current
        {
            get { return (global::MarcelJoachimKloubert.DragNBatch.App)Application.Current; }
        }

        /// <inheriteddoc />
        public new MainWindow MainWindow
        {
            get { return (global::MarcelJoachimKloubert.DragNBatch.Windows.MainWindow)base.MainWindow; }
        }

        #endregion Properties

        #region Methods (1)

        // Private Methods (1) 

        [STAThread]
        private static int Main(string[] args)
        {
            var app = new App();
            app.CommandLineArgs = args;
            app.InitializeComponent();

            // config
            {
                var cfgFile = Path.Combine(Environment.CurrentDirectory, "config.ini");
                var cfg = new IniFileConfigRepository(filePath: cfgFile,
                                                      isReadOnly: false);

                app.Config = cfg;
            }

            return app.Run(new MainWindow());
        }

        #endregion Methods
    }
}