// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Tries to peek the most upper value of a <see cref="Stack{T}" />.
        /// </summary>
        /// <typeparam name="T">Type of the items of the stack.</typeparam>
        /// <param name="stack">The stack.</param>
        /// <returns>The most upper value or the default instance of the item's type.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stack" /> is <see langword="null" />.
        /// </exception>
        public static T PeekOrDefault<T>(this Stack<T> stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException("stack");
            }

            return stack.Count > 0 ? stack.Peek() : default(T);
        }

        #endregion Methods
    }
}