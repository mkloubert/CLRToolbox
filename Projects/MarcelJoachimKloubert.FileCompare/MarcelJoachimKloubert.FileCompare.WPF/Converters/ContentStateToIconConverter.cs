// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Windows.Data;
using MarcelJoachimKloubert.FileCompare.WPF.Classes;
using System;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MarcelJoachimKloubert.FileCompare.WPF.Converters
{
    /// <summary>
    /// Converts a <see cref="CompareState?" /> value to an icon.
    /// </summary>
    public sealed class ContentStateToIconConverter : ValueConverterBase<CompareState?, ImageSource>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override ImageSource Convert(CompareState? state, string parameter, CultureInfo culture)
        {
            Uri u = null;

            if (state.HasValue)
            {
                switch (state.Value)
                {
                    case CompareState.Different:
                        u = new Uri("pack://application:,,,/Resources/Icons/different2.png", UriKind.Absolute);
                        break;

                    case CompareState.Failed:
                        u = new Uri("pack://application:,,,/Resources/Icons/failed.png", UriKind.Absolute);
                        break;

                    case CompareState.InProgress:
                        u = new Uri("pack://application:,,,/Resources/Icons/working2.png", UriKind.Absolute);
                        break;

                    case CompareState.Match:
                        u = new Uri("pack://application:,,,/Resources/Icons/same.png", UriKind.Absolute);
                        break;
                }
            }
            else
            {
                u = new Uri("pack://application:,,,/Resources/Icons/unknown.png", UriKind.Absolute);
            }

            if (u != null)
            {
                var result = new BitmapImage();
                result.BeginInit();
                result.UriSource = u;
                result.EndInit();

                return result;
            }

            return null;
        }

        #endregion Methods
    }
}