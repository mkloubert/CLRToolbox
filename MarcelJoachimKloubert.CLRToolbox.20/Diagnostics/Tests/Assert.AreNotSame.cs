﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    static partial class Assert
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Checks if two objects DO NOT have the same reference.
        /// </summary>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void AreNotSame(object x, object y)
        {
            AreNotSame(x, y, null);
        }

        /// <summary>
        /// Checks if two objects DO NOT have the same reference.
        /// </summary>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <param name="message">The message to display if check fails.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void AreNotSame(object x, object y, string message)
        {
            if (object.ReferenceEquals(x, y) == false)
            {
                return;
            }

            ThrowAssertException(message,
                                 delegate()
                                 {
                                     return string.Format("Same instances:\nx = {0}\ny = {1}",
                                                          AreSame_ToObjectDisplayText(x),
                                                          AreSame_ToObjectDisplayText(y));
                                 });
        }

        #endregion Methods
    }
}