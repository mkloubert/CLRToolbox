// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class MethodHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Checks if a type represents a delegate or not.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>Is delegate type or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />.
        /// </exception>
        public static bool IsDelegate(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return typeof(global::System.MulticastDelegate).Equals(type.BaseType);
        }

        #endregion Methods
    }
}
