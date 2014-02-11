// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CloudNET.SDK.Helpers
{
    internal static class CollectionHelper
    {
        #region Methods (1)

        // Internal Methods (1) 

        internal static T SingleOrDefault<T>(IEnumerable<T> seq, Func<T, bool> predicate)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            T result = default(T);

            using (IEnumerator<T> enumerator = seq.GetEnumerator())
            {
                byte elementCount = 0;

                while (enumerator.MoveNext())
                {
                    T current = enumerator.Current;
                    if (predicate(current) == false)
                    {
                        continue;
                    }

                    if (++elementCount > 1)
                    {
                        throw new InvalidOperationException("Sequence contains more than one element!");
                    }

                    result = current;
                }
            }

            return result;
        }

        #endregion Methods
    }
}
