// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    static partial class Assert
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Checks if one or more objects represents the same reference.
        /// </summary>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <param name="additionalObjs">The optional list of additional objects to check.</param>
        public static void AreSame(object x, object y, IEnumerable<IEnumerable<object>> additionalObjs)
        {
            AreSame(x, y, (IEnumerable<IEnumerable<object>>)additionalObjs);
        }

        /// <summary>
        /// Checks if one or more objects represents the same reference.
        /// </summary>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <param name="additionalObjs">The optional list of additional objects to check.</param>
        public static void AreSame(object x, object y, params object[] additionalObjs)
        {
            AreSame(x, y, (IEnumerable<IEnumerable<object>>)additionalObjs);
        }

        /// <summary>
        /// Checks if one or more objects represents the same reference.
        /// </summary>
        /// <param name="message">The message to display if check fails.</param>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <param name="additionalObjs">The optional list of additional objects to check.</param>
        public static void AreSame(IEnumerable<char> message, object x, object y, IEnumerable<IEnumerable<object>> additionalObjs)
        {
            AreSame(null, x, y, additionalObjs);
        }

        /// <summary>
        /// Checks if one or more objects represents the same reference.
        /// </summary>
        /// <param name="message">The message to display if check fails.</param>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <param name="additionalObjs">The optional list of additional objects to check.</param>
        public static void AreSame(IEnumerable<char> message, object x, object y, params object[] additionalObjs)
        {
            IEnumerable<object> otherItems = CollectionHelper.Concat(new object[] { y },
                                                                     CollectionHelper.ToEnumerableSafe(additionalObjs));

            if (false == CollectionHelper.All(otherItems,
                                              delegate(object item, long index)
                                              {
                                                  return object.ReferenceEquals(x, item);
                                              }))
            {
                string msg = StringHelper.AsString(message);
                if (msg == null)
                {
                    throw new AssertException();
                }
                else
                {
                    throw new AssertException(msg);
                }
            }
        }

        #endregion Methods
    }
}