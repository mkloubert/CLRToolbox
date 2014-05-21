// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    static partial class Assert
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Checks if a value is NOT <see langword="true" />.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void IsNotTrue(bool? value)
        {
            IsNotTrue(value, null);
        }

        /// <summary>
        /// Checks if a value is NOT <see langword="true" />.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="message">The message to display if check fails.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void IsNotTrue(bool? value, string message)
        {
            if (value != true)
            {
                return;
            }

            ThrowAssertException(message,
                                 delegate()
                                 {
                                     return "Expected FALSE or NULL!";
                                 });
        }

        #endregion Methods
    }
}