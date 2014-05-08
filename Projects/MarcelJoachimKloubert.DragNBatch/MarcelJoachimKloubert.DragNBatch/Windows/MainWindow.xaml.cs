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

            // ViewModel
            {
                var vm = new MainViewModel();
                vm.Error += this.ViewModel_Error;
                vm.ReloadPlugIns();

                this.ViewModel = vm;
            }
        }

        #endregion Constructors

        #region Properties (1)

        public MainViewModel ViewModel
        {
            get { return this.DataContext as MainViewModel; }

            set { this.DataContext = value; }
        }

        #endregion Properties

        #region Methods (2)

        // Private Methods (2) 

        private void Border_Info_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
        }

        private void ViewModel_Error(object sender, ErrorEventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        #endregion Methods
    }
}