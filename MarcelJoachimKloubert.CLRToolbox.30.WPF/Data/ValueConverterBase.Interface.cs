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
            return this.Convert(Converter.Current.ChangeType<TInput>(value),
                                Converter.Current.ChangeType<TParam>(parameter),
                                culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack(Converter.Current.ChangeType<TOutput>(value),
                                    Converter.Current.ChangeType<TParam>(parameter),
                                    culture);
        }

        #endregion Methods
    }
}
