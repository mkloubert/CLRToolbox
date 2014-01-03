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


using System.Globalization;
using System.Windows;
using MarcelJoachimKloubert.CLRToolbox.Windows.Data;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Converters
{
    /// <summary>
    /// Converts a <see cref="Visibility?" /> value from/to <see cref="bool?" />.
    /// </summary>
    public sealed class TMVisibilityToBoolConverter : ValueConverterBase<Visibility?, bool?>
    {
        #region Methods (2)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase<TInput, TParam, CultureInfo>" />
        protected override bool? Convert(Visibility? value, string parameter, CultureInfo culture)
        {
            if (value.HasValue)
            {
                switch (value.Value)
                {
                    case Visibility.Visible:
                        return true;

                    default:
                        return false;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase<TOutput, TParam, CultureInfo>" />
        protected override Visibility? ConvertBack(bool? value, string parameter, CultureInfo culture)
        {
            if (value.HasValue)
            {
                if (value.Value)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }

            return null;
        }

        #endregion Methods
    }
}
