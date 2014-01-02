// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel;
using System.IO;
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
            vm.Error += this.ViewModel_Error;
            vm.Closing += this.ViewModel_Closing;

            this.DataContext = vm;
        }

        #endregion Constructors

        #region Methods (2)

        // Private Methods (2) 

        private void ViewModel_Closing(object sender, CancelEventArgs e)
        {
            this.Dispatcher
                .BeginInvoke(new Action<MainWindow>((win) =>
                     {
                         win.Close();
                     }), this);
        }

        private void ViewModel_Error(object sender, ErrorEventArgs e)
        {
            var exception = e.GetException();

            this.Dispatcher
                .BeginInvoke(new Action<MainWindow, Exception>((win, ex) =>
                     {
                         MessageBox.Show(win,
                                         ex.Message,
                                         ex.GetType().FullName,
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                     }), this
                       , exception.GetBaseException() ?? exception);
        }

        #endregion Methods
    }
}
