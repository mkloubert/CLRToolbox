// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Windows.Data;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace MarcelJoachimKloubert.FileCompare.WPF.Converters
{
    /// <summary>
    /// Returns a <see cref="Visibility" /> for an object / value.
    /// </summary>
    public sealed class ObjectToVisibilityConverter : ValueConverterBase<object, Visibility>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override Visibility Convert(object obj, string parameter, CultureInfo culture)
        {
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

            if (obj.IsNotNull())
            {
                return invert ? defaultValue : Visibility.Visible;
            }

            return invert ? Visibility.Visible : defaultValue;
        }

        #endregion Methods
    }
}