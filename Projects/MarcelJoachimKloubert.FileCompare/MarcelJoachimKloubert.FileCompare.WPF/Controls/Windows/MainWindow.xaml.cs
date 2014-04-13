// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MahApps.Metro.Controls;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.FileCompare.WPF.Classes;
using MarcelJoachimKloubert.FileCompare.WPF.ViewModels;
using System.IO;
using System.Windows;

namespace MarcelJoachimKloubert.FileCompare.WPF.Controls.Windows
{
    /// <summary>
    /// Code behind for "MainWindow.xaml"
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        public MainViewModel ViewModel
        {
            get { return this.DataContext as MainViewModel; }

            set { this.DataContext = value; }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel = new MainViewModel();

            var cfgFile = new FileInfo(@"./config.ini");
            if (cfgFile.Exists)
            {
                var config = new IniFileConfigRepository(cfgFile);

                var list = new SynchronizedObservableCollection<CompareTask>();
                list.AddRange(CompareTask.FromConfig(config));

                this.ViewModel.Tasks = list;
            }
        }
    }
}