// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    partial class CommonConverter
    {
        #region Methods (1)

        // Private Methods (1) 

        partial void ParseInputValueForChangeType(Type targetType, ref object targetValue, IFormatProvider provider)
        {
            if (!targetType.Equals(typeof(global::System.DBNull)))
            {
                if (DBNull.Value.Equals(targetValue))
                {
                    targetValue = null;
                }
            }
            else
            {
                // target type is DBNull

                if (targetValue == null)
                {
                    targetValue = DBNull.Value;
                }
                else
                {
                    if (!DBNull.Value.Equals(targetValue))
                    {
                        throw new InvalidCastException();
                    }
                }
            }
        }

        #endregion Methods
    }
}
