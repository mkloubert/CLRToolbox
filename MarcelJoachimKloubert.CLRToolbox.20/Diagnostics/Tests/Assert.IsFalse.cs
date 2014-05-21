// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    static partial class Assert
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Checks if a value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void IsFalse(bool? value)
        {
            IsFalse(value, null);
        }

        /// <summary>
        /// Checks if a value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="message">The message to display if check fails.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void IsFalse(bool? value, string message)
        {
            if (value == false)
            {
                return;
            }

            ThrowAssertException(message,
                                 delegate()
                                 {
                                     return string.Format("Expected FALSE:\nvalue = {0}",
                                                          value.HasValue ? value.ToString() : "(null)");
                                 });
        }

        #endregion Methods
    }
}