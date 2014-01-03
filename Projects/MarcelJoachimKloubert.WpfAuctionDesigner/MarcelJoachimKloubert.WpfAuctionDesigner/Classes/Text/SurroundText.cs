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
    #region SurroundTextEventHandler

    /// <summary>
    /// A function / method for an request that wants to surround text
    /// with a prefix and a suffix.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The arguments for the event.</param>
    public delegate void TMSurroundTextEventHandler(object sender,
                                                    TMSurroundTextEventArgs e);

    #endregion

    #region SurroundTextEventArgs

    /// <summary>
    /// Stores data for a <see cref="TMSurroundTextEventHandler" /> call.
    /// </summary>
    public sealed class TMSurroundTextEventArgs : EventArgs
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMSurroundTextEventArgs"/> class.
        /// </summary>
        /// <param name="prefix">The value for <see cref="TMSurroundTextEventArgs.Prefix" /> property.</param>
        /// <param name="suffix">The value for <see cref="TMSurroundTextEventArgs.Suffix" /> property.</param>
        public TMSurroundTextEventArgs(IEnumerable<char> prefix = null,
                                       IEnumerable<char> suffix = null)
        {
            this.Prefix = prefix.AsString();
            this.Suffix = suffix.AsString();
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        public string Prefix
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the suffix.
        /// </summary>
        public string Suffix
        {
            get;
            private set;
        }

        #endregion Properties
    }

    #endregion
}
