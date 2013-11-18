using System;
using System.Threading.Tasks;
using System.Windows;
using MarcelJoachimKloubert.ScriptEngine.Editor.Windows;

namespace MarcelJoachimKloubert.ScriptEngine.Editor
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        #region Constructors (2)

        private App(string[] args)
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        public App()
            : this(new string[0])
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the command line arguments for that application.
        /// </summary>
        public string[] CommandLineArguments
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (3)

        // Private Methods (3) 

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }

        [STAThread]
        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            var app = new App(args);
            var mainWin = new MainWindow();

            return app.Run(mainWin);
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {

        }

        #endregion Methods

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Application.Current" />
        public static new global::MarcelJoachimKloubert.ScriptEngine.Editor.App Current
        {
            get { return (App)Application.Current; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Application.MainWindow" />
        public new global::MarcelJoachimKloubert.ScriptEngine.Editor.Windows.MainWindow MainWindow
        {
            get { return (MainWindow)base.MainWindow; }
        }
    }
}
