// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 
        
        /// <summary>
        /// Checks if a nullable boolean value is NOT <see langword="false" />.
        /// </summary>
        /// <param name="value">the value to check.</param>
        /// <returns>Is not <see langword="false" /> is <see langword="false" />.</returns>
        public static bool IsNotFalse(this bool? value)
        {
            return IsFalse(value) == false;
        }

        /// <summary>
        /// Checks if a nullable boolean value is really <see langword="false" />.
        /// </summary>
        /// <param name="value">the value to check.</param>
        /// <returns>Is <see langword="false" /> or not.</returns>
        public static bool IsFalse(this bool? value)
        {
            return value == false;
        }

        #endregion Methods
    }
}
