// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Creates a new empty sequence.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <returns>The empty sequence.</returns>
        public static IEnumerable<T> Empty<T>()
        {
            yield break;
        }

        #endregion Methods
    }
}
