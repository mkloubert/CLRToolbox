// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Windows.Controls;
using MarcelJoachimKloubert.DragNBatch.ViewModel;
using System.IO;
using System.Windows;

namespace MarcelJoachimKloubert.DragNBatch.Windows
{
    /// <summary>
    /// Code behind for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BorderlessMoveableWindow
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Methods (3)

        // Private Methods (3) 

        private void Border_Info_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
        }

        private void MainViewModel_Error(object sender, ErrorEventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = new MainViewModel();
            vm.Error += this.MainViewModel_Error;
            vm.ReloadPlugIns();

            this.DataContext = vm;
        }

        #endregion Methods
    }
}