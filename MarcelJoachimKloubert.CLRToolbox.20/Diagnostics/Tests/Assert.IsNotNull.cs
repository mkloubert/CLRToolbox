// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    static partial class Assert
    {
        #region Methods (7)

        // Public Methods (6) 

        /// <summary>
        /// Checks if a value is NOT <see langword="null" />.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public static void IsNotNull<T>(T? value) where T : struct
        {
            IsNotNull<T>(value, null);
        }

        /// <summary>
        /// Checks if a value is NOT <see langword="null" />.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="message">The message to display if check fails.</param>
        public static void IsNotNull<T>(T? value, string message) where T : struct
        {
            IsNotNullInner<T?>(value, message);
        }

        /// <summary>
        /// Checks if an object is NOT <see langword="null" />.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <remarks>
        /// <see cref="DBNull" /> values are handled as <see langword="null" /> references.
        /// </remarks>
        public static void IsNotNull<T>(T obj) where T : class
        {
            IsNotNull<T>(obj, true);
        }

        /// <summary>
        /// Checks if an object is NOT <see langword="null" />.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="handleDbNullAsNull">
        /// Handle <see cref="DBNull" /> values as <see langword="null" /> references or not.
        /// </param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void IsNotNull<T>(T obj, bool handleDbNullAsNull) where T : class
        {
            IsNotNull<T>(obj, handleDbNullAsNull, null);
        }

        /// <summary>
        /// Checks if an object is NOT <see langword="null" />.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="message">The message to display if check fails.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        /// <remarks>
        /// <see cref="DBNull" /> values are handled as <see langword="null" /> references.
        /// </remarks>
        public static void IsNotNull<T>(T obj, string message) where T : class
        {
            IsNotNull<T>(obj, true, message);
        }

        /// <summary>
        /// Checks if an object is NOT <see langword="null" />.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="handleDbNullAsNull">
        /// Handle <see cref="DBNull" /> values as <see langword="null" /> references or not.
        /// </param>
        /// <param name="message">The message to display if check fails.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void IsNotNull<T>(T obj, bool handleDbNullAsNull, string message) where T : class
        {
            if (handleDbNullAsNull &&
                DBNull.Value.Equals(obj))
            {
                obj = null;
            }

            IsNotNullInner<T>(obj, message);
        }

        // Private Methods (1) 

        private static void IsNotNullInner<T>(T obj, string message)
        {
            if (obj != null)
            {
                return;
            }

            ThrowAssertException(message,
                                 delegate()
                                 {
                                     return string.Format("Expected INSTANCE of '{0}'!",
                                                          typeof(T).FullName);
                                 });
        }

        #endregion Methods
    }
}