// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Globalization;
using System.Windows;
using MarcelJoachimKloubert.CLRToolbox.Windows.Data;

namespace AppServerProcessManager.Data
{
    /// <summary>
    /// Converts <see cref="bool?" /> to <see cref="Visibility" />.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : ValueConverterBase<bool?, Visibility>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase{TInput, TOutput}.Convert(TInput, object, CultureInfo)" />
        protected override Visibility Convert(bool? value, string parameter, CultureInfo culture)
        {
            Visibility result;
            if (value.HasValue)
            {
                if (value.Value)
                {
                    //TODO handle parameter
                    result = Visibility.Visible;
                }
                else
                {
                    //TODO handle parameter
                    result = Visibility.Collapsed;
                }
            }
            else
            {
                //TODO handle parameter
                result = Visibility.Collapsed;
            }

            return result;
        }

        #endregion Methods
    }
}
