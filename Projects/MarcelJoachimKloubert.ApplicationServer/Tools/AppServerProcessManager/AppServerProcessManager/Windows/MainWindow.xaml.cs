// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Windows;
using AppServerProcessManager.Views;

namespace AppServerProcessManager.Windows
{
    /// <summary>
    /// Code behind of "MainWindow.xaml"
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            var vm = new MainViewModel();

            this.DataContext = vm;
        }

        #endregion Constructors
    }
}
