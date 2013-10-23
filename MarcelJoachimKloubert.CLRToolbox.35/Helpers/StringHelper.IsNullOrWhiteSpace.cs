// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Replaced in .NET 3.5 for .NET 2.0</remarks>
        public static bool IsNullOrWhiteSpace(IEnumerable<char> chars)
        {
            return chars == null ||
                   chars.All(c => char.IsWhiteSpace(c));
        }

        #endregion Methods
    }
}
