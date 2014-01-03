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


using System.ComponentModel;
using System.Windows;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Windows
{
    /// <summary>
    /// Code behind of "PreviewWindow.xaml".
    /// </summary>
    public partial class PreviewWindow : Window
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewWindow"/> class.
        /// </summary>
        public PreviewWindow()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets if that window shlow be hided (<see langword="false" />)
        /// or really be closed (<see langword="true" />).
        /// </summary>
        public bool ForceClose
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Window.Close()" />
        public new void Close()
        {
            this.Close(false);
        }

        /// <summary>
        /// Extension of <see cref="Window.Close()" /> method.
        /// </summary>
        /// Hide (<see langword="false" />) or really close that window
        /// (<see langword="true" />).
        public void Close(bool forceClose)
        {
            this.ForceClose = forceClose;

            base.Close();
        }
        // Private Methods (1) 

        private void PreviewWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!this.ForceClose)
            {
                e.Cancel = true;
                this.Hide();

                return;
            }

            this.DataContext = null;
        }

        #endregion Methods
    }
}
