// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows;
using MarcelJoachimKloubert.CLRToolbox.Windows.Controls;
using MarcelJoachimKloubert.DragNBatch.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// Gets or sets the ViewModel for that window.
        /// </summary>
        public MainViewModel ViewModel
        {
            get { return this.DataContext as MainViewModel; }

            set { this.DataContext = value; }
        }

        #endregion Properties

        #region Methods (5)

        // Private Methods (5) 

        private void MainWindow_Info_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                var effect = DragDropEffects.None;

                var vm = this.ViewModel;
                if (vm != null)
                {
                    var plugIn = vm.SelectedPlugIn;
                    if (plugIn != null)
                    {
                        if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                            sender != e.Source)
                        {
                            effect = DragDropEffects.Link;
                        }
                    }
                }

                e.Effects = effect;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void MainWindow_Info_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var vm = this.ViewModel;
                if (vm == null)
                {
                    return;
                }

                var plugIn = vm.SelectedPlugIn;
                if (plugIn == null)
                {
                    return;
                }

                if (e.Data.GetDataPresent(DataFormats.FileDrop) == false)
                {
                    return;
                }

                e.Handled = true;
                var paths = e.Data.GetData(DataFormats.FileDrop) as IEnumerable<string>;

                var ctx = new HandleFilesContext();
                ctx.Culture = CultureInfo.CurrentUICulture;
                ctx.Directories = paths.Where(dp =>
                    {
                        try
                        {
                            return Directory.Exists(dp);
                        }
                        catch
                        {
                        }

                        return false;
                    });
                ctx.Files = paths.Where(fp =>
                                        {
                                            try
                                            {
                                                return File.Exists(fp);
                                            }
                                            catch
                                            {
                                            }

                                            return false;
                                        });

                vm.HandleFiles(plugIn, ctx);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void ShowErrorMessage(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            try
            {
                App.Current
                   .BeginInvoke((app, appState) =>
                   {
                       MessageBox.Show(appState.Window,
                                       appState.Error.ToString(),
                                       appState.Error.GetType().FullName,
                                       MessageBoxButton.OK);
                   }, new
                   {
                       Error = ex.GetBaseException() ?? ex,
                       Window = this,
                   });
            }
            catch
            {
            }
        }

        private void ViewModel_Error(object sender, ErrorEventArgs e)
        {
            this.ShowErrorMessage(e.GetException());
        }

        #endregion Methods
    }
}