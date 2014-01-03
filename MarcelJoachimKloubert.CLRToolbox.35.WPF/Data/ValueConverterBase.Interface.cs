// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Globalization;
using System.Windows.Data;
using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Data
{
    partial class ValueConverterBase<TInput, TOutput, TParam>
    {
        #region Methods (2)

        // Private Methods (2) 

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(GlobalConverter.Current.ChangeType<TInput>(value),
                                GlobalConverter.Current.ChangeType<TParam>(parameter),
                                culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack(GlobalConverter.Current.ChangeType<TOutput>(value),
                                    GlobalConverter.Current.ChangeType<TParam>(parameter),
                                    culture);
        }

        #endregion Methods
    }
}
