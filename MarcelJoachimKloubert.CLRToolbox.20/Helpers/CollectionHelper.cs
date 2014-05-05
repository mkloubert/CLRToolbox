// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    /// <summary>
    /// Helper class for collection operations.
    /// </summary>
    public static partial class CollectionHelper
    {
        #region Fields (2)

        private const string _ERR_MSG_SINGLE_MORE_THAN_ONE_ELEMENT = "The sequence contains more than one element!";
        private const string _ERR_MSG_SINGLE_NO_ELEMENTS = "The sequence contains no element!";

        #endregion Fields

        #region Methods (1)

        // Private Methods (1) 

        private static long? TryGetCountFromProperty<T>(IEnumerable<T> seq)
        {
            // try cast as generic collection
            ICollection<T> coll = seq as ICollection<T>;
            if (coll != null)
            {
                return coll.Count;
            }

            // try cast as GENERAL collection
            ICollection coll2 = seq as ICollection;
            if (coll2 != null)
            {
                return coll2.Count;
            }

            // has no property
            return null;
        }

        #endregion Methods
    }
}