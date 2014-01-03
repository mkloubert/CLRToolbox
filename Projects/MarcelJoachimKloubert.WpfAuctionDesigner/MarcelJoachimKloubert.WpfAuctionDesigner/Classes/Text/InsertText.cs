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
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Text
{
    #region TMInsertTextHandler

    /// <summary>
    /// A function / method for an request that wants to insert text.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The arguments for the event.</param>
    public delegate void TMInsertTextHandler(object sender,
                                             TMInsertTextEventArgs e);

    #endregion

    #region TMInsertTextEventArgs

    /// <summary>
    /// Stores data for a <see cref="TMInsertTextHandler" /> call.
    /// </summary>
    public sealed class TMInsertTextEventArgs : EventArgs
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMInsertTextEventArgs" /> class.
        /// </summary>
        /// <param name="text">The value for <see cref="TMInsertTextEventArgs.Text" /> property.</param>
        public TMInsertTextEventArgs(IEnumerable<char> text = null)
        {
            this.Text = text.AsString();
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        #endregion Properties
    }

    #endregion
}
