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
        /// Pushes a list of items to a stack collections.
        /// </summary>
        /// <typeparam name="T">Type of the items of the stack.</typeparam>
        /// <param name="stack">The stack.</param>
        /// <param name="seq">The items to push.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stack" /> and/or <paramref name="seq" /> are <see langword="null" /> references.
        /// </exception>
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> seq)
        {
            if (stack == null)
            {
                throw new ArgumentNullException("stack");
            }

            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            using (IEnumerator<T> e = seq.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    stack.Push(e.Current);
                }
            }
        }

        #endregion Methods
    }
}