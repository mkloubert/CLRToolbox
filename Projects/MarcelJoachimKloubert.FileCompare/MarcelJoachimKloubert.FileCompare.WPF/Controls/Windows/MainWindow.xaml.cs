// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MahApps.Metro.Controls;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows;
using MarcelJoachimKloubert.FileCompare.WPF.Classes;
using MarcelJoachimKloubert.FileCompare.WPF.ViewModels;
using System;
using System.IO;
using System.Linq;
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

            this.ViewModel = new MainViewModel();
            this.ViewModel.Error += this.ViewModel_Error;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public MainViewModel ViewModel
        {
            get { return this.DataContext as MainViewModel; }

            private set { this.DataContext = value; }
        }

        #endregion Properties

        #region Methods (2)

        // Private Methods (2) 

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var cfgFile = new FileInfo(@"./config.ini");
            if (cfgFile.Exists)
            {
                var config = new IniFileConfigRepository(cfgFile);

                var list = new SynchronizedObservableCollection<CompareTask>();
                list.AddRange(CompareTask.FromConfig(config)
                                         .OrderBy(c => c.DisplayName, StringComparer.CurrentCultureIgnoreCase)
                                         .ThenBy(c => c.Name, StringComparer.CurrentCultureIgnoreCase));

                this.ViewModel.Tasks = list;
            }
        }

        private void ViewModel_Error(object sender, ErrorEventArgs e)
        {
            try
            {
                this.BeginInvoke((win, state) =>
                    {
                        try
                        {
                            MessageBox.Show(state.Error.ToString(),
                                            string.Format("[ERROR] {0}",
                                                          state.Error.GetType().FullName),
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        catch
                        {
                            // ignore
                        }
                    }, actionState: new
                    {
                        Error = e.GetException(),
                    });
            }
            catch
            {
                // ignore
            }
        }

        #endregion Methods
    }
}