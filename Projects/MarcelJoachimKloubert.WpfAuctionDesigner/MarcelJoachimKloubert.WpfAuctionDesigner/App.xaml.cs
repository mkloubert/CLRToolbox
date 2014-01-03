// 
// WPF based tool to create product pages for auctions on eBay, e.g.
// Copyright (C) 2013  Marcel Joachim Kloubert
//     
// This library is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or (at
// your option) any later version.
//     
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// General Public License for more details.
//     
// You should have received a copy of the GNU General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301,
// USA.
// 


using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MarcelJoachimKloubert.WpfAuctionDesigner.Windows;

namespace MarcelJoachimKloubert.WpfAuctionDesigner
{
    /// <summary>
    /// Code behind for "App.xaml".
    /// </summary>
    public partial class App : Application
    {
        #region Methods (4)

        // Private Methods (4) 

        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //TODO


        }

        private static void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //TODO
        }

        [STAThread]
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            var app = new Application();
            app.DispatcherUnhandledException += Application_DispatcherUnhandledException;

            var win = new MainWindow();
            app.Run(win);
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //TODO
        }

        #endregion Methods
    }
}
