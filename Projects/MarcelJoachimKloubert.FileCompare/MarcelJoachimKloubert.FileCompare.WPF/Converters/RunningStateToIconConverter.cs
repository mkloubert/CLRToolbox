// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Windows.Data;
using System;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MarcelJoachimKloubert.FileCompare.WPF.Converters
{
    /// <summary>
    /// Converts a boolean running state value to an icon.
    /// </summary>
    public sealed class RunningStateToIconConverter : ValueConverterBase<bool?, ImageSource>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override ImageSource Convert(bool? input, string parameter, CultureInfo culture)
        {
            BitmapImage result = null;

            if (input.HasValue)
            {
                Uri u;
                if (input.Value)
                {
                    u = new Uri("pack://application:,,,/Resources/Icons/start.png", UriKind.Absolute);
                }
                else
                {
                    u = new Uri("pack://application:,,,/Resources/Icons/stop.png", UriKind.Absolute);
                }

                result = new BitmapImage();
                result.BeginInit();
                result.UriSource = u;
                result.EndInit();
            }

            return result;
        }

        #endregion Methods
    }
}