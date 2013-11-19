using System;
using System.Globalization;
using System.Windows.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Data
{
    partial class ValueConverterBase<TInput, TOutput, TParam>
    {
        #region Methods (2)

        // Private Methods (2) 

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(TMConvert.ChangeType<TInput>(value),
                                TMConvert.ChangeType<TParam>(parameter),
                                culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack(TMConvert.ChangeType<TOutput>(value),
                                    TMConvert.ChangeType<TParam>(parameter),
                                    culture);
        }

        #endregion Methods
    }
}
