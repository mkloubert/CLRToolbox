// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CloudNET.Classes.Helpers
{
    internal static class TimeHelper
    {
        #region Methods (1)

        // Internal Methods (1) 

        internal static DateTime? NormalizeValue(DateTime? input)
        {
            if (input.HasValue == false)
            {
                return null;
            }

            return new DateTime(input.Value.Year, input.Value.Month, input.Value.Day,
                                input.Value.Hour, input.Value.Minute, input.Value.Second,
                                input.Value.Kind);
        }

        #endregion Methods
    }
}
