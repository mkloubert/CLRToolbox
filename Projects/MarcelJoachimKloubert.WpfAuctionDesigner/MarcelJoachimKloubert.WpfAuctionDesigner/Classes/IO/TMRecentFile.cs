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
using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.IO
{
    /// <summary>
    /// Stores the data of a recent file.
    /// </summary>
    public sealed class TMRecentFile : IEquatable<TMRecentFile>
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMRecentFile"/> class.
        /// </summary>
        /// <param name="path">The path of the underlying file.</param>
        public TMRecentFile(IEnumerable<char> path)
        {
            this.FullPath = Path.GetFullPath(path.AsString());
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the full path of the underlying file.
        /// </summary>
        public string FullPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the filename part of <see cref="TMRecentFile.FullPath" />.
        /// </summary>
        public string Name
        {
            get { return Path.GetFileName(this.FullPath); }
        }

        #endregion Properties

        #region Methods (7)

        // Public Methods (7) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(TMRecentFile other)
        {
            if (other == null)
            {
                return false;
            }

            return (other.FullPath ?? string.Empty).ToLower().Trim() ==
                   (this.FullPath ?? string.Empty).ToLower().Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
        public override bool Equals(object other)
        {
            if (other is TMRecentFile)
            {
                return this.Equals((TMRecentFile)other);
            }

            return base.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()" />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a new <see cref="FileInfo" /> object based on
        /// the value of <see cref="TMRecentFile.FullPath" /> property.
        /// </summary>
        /// <returns>The file info object.</returns>
        public FileInfo GetInfo()
        {
            return new FileInfo(this.FullPath);
        }

        /// <summary>
        /// Converts a <see cref="string" /> object to a <see cref="TMRecentFile" /> instance.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The converted target object.</returns>
        public static implicit operator TMRecentFile(string source)
        {
            return source != null ? new TMRecentFile(source) : null;
        }

        /// <summary>
        /// Converts a <see cref="FileInfo" /> object to a <see cref="TMRecentFile" /> instance.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The converted target object.</returns>
        public static explicit operator TMRecentFile(FileInfo source)
        {
            return source != null ? new TMRecentFile(source.FullName) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return this.FullPath;
        }

        #endregion Methods
    }
}
