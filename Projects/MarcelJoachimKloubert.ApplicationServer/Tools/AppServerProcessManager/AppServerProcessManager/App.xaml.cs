// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using System.Windows;
using AppServerProcessManager.Windows;

namespace AppServerProcessManager
{
    /// <summary>
    /// Code behind of "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the list of command line arguments for that app.
        /// </summary>
        public string[] CommandLineArguments
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Application.Current" />
        public static new App Current
        {
            get { return (global::AppServerProcessManager.App)Application.Current; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Application.MainWindow" />
        public new MainWindow MainWindow
        {
            get { return (global::AppServerProcessManager.Windows.MainWindow)base.MainWindow; }
        }

        #endregion Properties

        #region Methods (1)

        // Private Methods (1) 

        [STAThread]
        private static int Main(string[] args)
        {
            var app = new App();
            app.CommandLineArguments = args.Where(a => !string.IsNullOrWhiteSpace(a))
                                           .ToArray();

            return app.Run(new MainWindow());
        }

        #endregion Methods
    }
}
