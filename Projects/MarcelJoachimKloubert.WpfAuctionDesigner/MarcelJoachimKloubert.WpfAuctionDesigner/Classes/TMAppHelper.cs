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
using System.IO;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes
{
    /// <summary>
    /// Application helper class.
    /// </summary>
    public static class TMAppHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Returns the data directory of that application.
        /// </summary>
        /// <param name="createIfNotExists">
        /// Create if directory does not exist or simply return it.
        /// </param>
        /// <returns>The data directory of that application.</returns>
        public static DirectoryInfo GetAppDataDirectory(bool createIfNotExists = false)
        {
            var myAppDataDir =
                new DirectoryInfo(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.ApplicationData));

            var auctionDesignerAppDir = new DirectoryInfo(Path.Combine(myAppDataDir.FullName,
                                                                       "MJKAuctionDesigner"));
            if (createIfNotExists &&
                !auctionDesignerAppDir.Exists)
            {
                auctionDesignerAppDir.Create();
                auctionDesignerAppDir.Refresh();
            }

            return auctionDesignerAppDir;
        }

        #endregion Methods
    }
}
