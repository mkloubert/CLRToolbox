// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="StringHelper.AsHexString(IEnumerable{byte})" />
        public static string AsHexString(this IEnumerable<byte> binData)
        {
            return StringHelper.AsHexString(binData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="StringHelper.AsHexString(IEnumerable{byte}, bool)" />
        public static string AsHexString(this IEnumerable<byte> binData, bool lowerCase)
        {
            return StringHelper.AsHexString(binData, lowerCase);
        }

        #endregion Methods
    }
}
