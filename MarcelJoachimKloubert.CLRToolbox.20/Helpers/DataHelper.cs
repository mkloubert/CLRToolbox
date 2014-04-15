// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using System.Globalization;
namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    /// <summary>
    /// Helper class for data(base) operations.
    /// </summary>
    public static partial class DataHelper
    {
        #region Methods (1)

        // Private Methods (1) 

        private static string ParseForCsvCell(object obj)
        {
            return GlobalConverter.Current
                                  .ChangeType<string>(obj,
                                                      CultureInfo.InvariantCulture);
        }

        #endregion Methods
    }
}