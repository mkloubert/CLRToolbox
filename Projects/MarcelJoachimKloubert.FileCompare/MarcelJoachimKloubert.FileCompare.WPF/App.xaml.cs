// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.FileCompare.WPF.Controls.Windows;
using System;
using System.Windows;

namespace MarcelJoachimKloubert.FileCompare.WPF
{
    /// <summary>
    /// Code behind for "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        #region Constructors (1)

        private App()
        {
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the list of command line arguments.
        /// </summary>
        public string[] CommandLineArguments
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public static new App Current
        {
            get { return (global::MarcelJoachimKloubert.FileCompare.WPF.App)Application.Current; }
        }

        /// <inheriteddoc />
        public new MainWindow MainWindow
        {
            get { return (global::MarcelJoachimKloubert.FileCompare.WPF.Controls.Windows.MainWindow)base.MainWindow; }
        }

        #endregion Properties

        #region Methods (1)

        // Private Methods (1) 

        [STAThread]
        private static int Main(string[] args)
        {
            var a = new App()
            {
                CommandLineArguments = args,
            };
            a.InitializeComponent();

            return a.Run(new MainWindow());
        }

        #endregion Methods
    }
}