// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Windows.Data;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace MarcelJoachimKloubert.FileCompare.WPF.Converters
{
    /// <summary>
    /// Converts a boolean value to a <see cref="Visibility" />.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : ValueConverterBase<bool?, Visibility>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override Visibility Convert(bool? value, string parameter, CultureInfo culture)
        {
            if (value.HasValue == false)
            {
                return Visibility.Collapsed;
            }

            var @params = (parameter ?? string.Empty).Split(';')
                                                     .Select(s => s.Trim())
                                                     .Where(s => s != string.Empty)
                                                     .Distinct();

            var defaultValue = Visibility.Collapsed;
            var invert = false;
            foreach (var p in @params)
            {
                switch (p)
                {
                    case "hidden":
                        defaultValue = Visibility.Hidden;
                        break;

                    case "invert":
                        invert = true;
                        break;
                }
            }

            if (value.Value)
            {
                return invert ? defaultValue : Visibility.Visible;
            }

            return invert ? Visibility.Visible : defaultValue;
        }

        #endregion Methods
    }
}