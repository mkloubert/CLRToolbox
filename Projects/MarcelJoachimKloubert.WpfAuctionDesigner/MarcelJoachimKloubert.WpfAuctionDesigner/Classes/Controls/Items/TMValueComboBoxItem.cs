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

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Controls.Items
{
    /// <summary>
    /// A ComboBox item that handles an inner value and a custom
    /// delegate for generating the string representation of the value.
    /// </summary>
    public sealed class TMValueComboBoxItem : IEquatable<TMValueComboBoxItem>
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMValueComboBoxItem"/> class.
        /// </summary>
        /// <param name="value">
        /// The value for <see cref="TMValueComboBoxItem.Value" /> property.
        /// </param>
        /// <param name="valueToString">
        /// The value for <see cref="TMValueComboBoxItem.ValueToStringFunc" /> property.
        /// </param>
        public TMValueComboBoxItem(object value,
                                   Func<TMValueComboBoxItem, IEnumerable<char>> valueToString = null)
        {
            this.Value = value;
            this.ValueToStringFunc = valueToString ?? CommonValueToString;
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets or sets an additional value that should be linked with that instance.
        /// </summary>
        public object Tag
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the underlying value.
        /// </summary>
        public object Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the function that creates the string representation of
        /// that item.
        /// </summary>
        public Func<TMValueComboBoxItem, IEnumerable<char>> ValueToStringFunc
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
        public override bool Equals(object other)
        {
            if (other is TMValueComboBoxItem)
            {
                return this.Equals((TMValueComboBoxItem)other);
            }

            return base.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(TMValueComboBoxItem other)
        {
            return other == null ? false : object.Equals(this.Value, other.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()"/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return this.ValueToStringFunc(this).AsString() ?? string.Empty;
        }
        // Private Methods (1) 

        private static IEnumerable<char> CommonValueToString(TMValueComboBoxItem item)
        {
            if (item == null)
            {
                return null;
            }

            var val = item.Value;
            return val != null ? val.ToString() : string.Empty;
        }

        #endregion Methods
    }
}
