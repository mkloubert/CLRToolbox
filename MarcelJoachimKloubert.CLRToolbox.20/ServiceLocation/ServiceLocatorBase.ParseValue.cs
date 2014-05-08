// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation
{
    partial class ServiceLocatorBase
    {
        #region Methods (1)

        // Private Methods (1) 

        private static object ParseValue(object value)
        {
            object result = value;
            if (DBNull.Value.Equals(result))
            {
                result = null;
            }

            return result;
        }

        #endregion Methods
    }
}