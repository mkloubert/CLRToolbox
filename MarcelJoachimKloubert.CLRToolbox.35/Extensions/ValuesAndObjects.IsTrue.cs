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
        /// Checks if a nullable boolean value is NOT <see langword="langword" />.
        /// </summary>
        /// <param name="value">the value to check.</param>
        /// <returns>Is not <see langword="true" /> is <see langword="true" />.</returns>
        public static bool IsNotTrue(this bool? value)
        {
            return IsTrue(value) == false;
        }

        /// <summary>
        /// Checks if a nullable boolean value is really <see langword="true" />.
        /// </summary>
        /// <param name="value">the value to check.</param>
        /// <returns>Is <see langword="true" /> or not.</returns>
        public static bool IsTrue(this bool? value)
        {
            return value == true;
        }

        #endregion Methods
    }
}
