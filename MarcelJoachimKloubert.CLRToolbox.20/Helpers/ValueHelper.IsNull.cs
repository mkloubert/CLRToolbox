// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class ValueHelper
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Checks if an object is <see langword="null" /> or <see cref="DBNull" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>Is <see langword="null" />/<see cref="DBNull" /> or not.</returns>
        public static bool IsNull<T>(T obj) where T : class
        {
            return obj == null ||
                   DBNull.Value.Equals(obj);
        }

        /// <summary>
        /// Checks if a nullable struct is <see langword="null" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <returns>Is <see langword="null" /> or not.</returns>
        public static bool IsNull<T>(Nullable<T> value) where T : struct
        {
            return value.HasValue == false;
        }

        #endregion Methods
    }
}
