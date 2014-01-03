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


using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Windows
{
    partial class PreviewWindow
    {
        #region Nested Classes (1)

        /// <summary>
        /// View model for <see cref="PreviewWindow" /> class.
        /// </summary>
        public sealed class ViewModel : NotificationObjectBase
        {
            #region Fields (1)

            private MainWindow.ViewModel _baseViewModel;

            #endregion Fields

            #region Constructors (1)

            internal ViewModel(MainWindow.ViewModel baseViewModel)
            {
                this.BaseViewModel = baseViewModel;
            }

            #endregion Constructors

            #region Properties (1)

            /// <summary>
            /// Gets the base view model of main windows.
            /// </summary>
            public MainWindow.ViewModel BaseViewModel
            {
                get { return this._baseViewModel; }

                private set { this.SetProperty(ref this._baseViewModel, value); }
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}
