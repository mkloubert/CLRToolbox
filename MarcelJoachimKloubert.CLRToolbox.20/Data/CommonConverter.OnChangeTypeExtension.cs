// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    partial class CommonConverter
    {
        #region Methods (1)

        // Private Methods (1) 

        partial void OnChangeTypeExtension(Type targetType, ref object targetValue, IFormatProvider provider, ref bool handled)
        {
            if (provider == null)
            {
                targetValue = global::System.Convert.ChangeType(targetValue, targetType);
                handled = true;
            }
        }

        #endregion Methods
    }
}
