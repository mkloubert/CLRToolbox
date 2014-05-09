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
        /// Checks if an object is NOT <see langword="null" /> and NOT <see cref="DBNull" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>Is <see langword="null" />/<see cref="DBNull" /> (<see langword="false" />) or not (<see langword="true" />).</returns>
        public static bool IsNotNull<T>(T obj) where T : class
        {
            return IsNull<T>(obj) == false;
        }

        /// <summary>
        /// Checks if a nullable struct is NOT <see langword="null" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <returns>Is <see langword="null" /> (<see langword="false" />) or not (<see langword="true" />).</returns>
        public static bool IsNotNull<T>(Nullable<T> value) where T : struct
        {
            return IsNull<T>(value) == false;
        }

        #endregion Methods
    }
}