// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    static partial class Assert
    {
        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Checks if two objects are the same reference.
        /// </summary>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void AreSame(object x, object y)
        {
            AreSame(x, y, null);
        }

        /// <summary>
        /// Checks if two objects are the same reference.
        /// </summary>
        /// <param name="x">The first object to check.</param>
        /// <param name="y">The second object to check.</param>
        /// <param name="message">The message to display if check fails.</param>
        /// <exception cref="AssertException">Check failed.</exception>
        public static void AreSame(object x, object y, string message)
        {
            if (object.ReferenceEquals(x, y))
            {
                return;
            }

            if (message == null)
            {
                throw new AssertException(string.Format("Unique instances:\nx = {0}\ny = {1}",
                                                        AreSame_ToObjectDisplayText(x),
                                                        AreSame_ToObjectDisplayText(y)));
            }
            else
            {
                throw new AssertException(message);
            }
        }

        // Private Methods (1) 

        private static string AreSame_ToObjectDisplayText(object obj)
        {
            if (obj == null)
            {
                return "(null)";
            }

            return string.Format("[{0}] '{1}'; '{2}'",
                                 obj.GetHashCode(),
                                 obj.GetType().FullName,
                                 obj.GetType().Assembly.FullName);
        }

        #endregion Methods
    }
}