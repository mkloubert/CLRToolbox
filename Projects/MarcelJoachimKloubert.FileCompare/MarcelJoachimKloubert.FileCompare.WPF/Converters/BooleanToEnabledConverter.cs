// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Windows.Data;
using System.Globalization;
using System.Linq;

namespace MarcelJoachimKloubert.FileCompare.WPF.Converters
{
    /// <summary>
    /// Converts a boolean value to another boolean value that represents the enabled state of something.
    /// </summary>
    public sealed class BooleanToEnabledConverter : ValueConverterBase<bool?, bool>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override bool Convert(bool? input, string parameter, CultureInfo culture)
        {
            if (input.HasValue == false)
            {
                return false;
            }

            var @params = (parameter ?? string.Empty).Split(';')
                                                     .Select(s => s.Trim())
                                                     .Where(s => s != string.Empty)
                                                     .Distinct();

            var invert = false;
            foreach (var p in @params)
            {
                switch (p)
                {
                    case "invert":
                        invert = true;
                        break;
                }
            }

            return invert ? input.Value == false
                          : input.Value;
        }

        #endregion Methods
    }
}