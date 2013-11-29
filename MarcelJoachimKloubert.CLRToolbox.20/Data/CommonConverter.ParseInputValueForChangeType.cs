// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    partial class CommonConverter
    {
        #region Methods (1)

        // Private Methods (1) 

        partial void ParseInputValueForChangeType<T>(ref object value, IFormatProvider provider)
        {
            if (typeof(T).Equals(typeof(global::System.DBNull)))
            {
                // target type is DBNull

                if (value == null)
                {
                    value = DBNull.Value;
                }
                else
                {
                    if (!DBNull.Value.Equals(value))
                    {
                        throw new InvalidCastException();
                    }
                }
            }
            else
            {
                if (DBNull.Value.Equals(value))
                {
                    value = null;
                }
            }
        }

        #endregion Methods
    }
}
