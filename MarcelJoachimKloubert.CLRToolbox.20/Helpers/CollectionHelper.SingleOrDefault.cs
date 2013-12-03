// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Tries to return the one and only element of a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <returns>The only element or the default value of the type if no element exists in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="seq" /> contains more than one element.
        /// </exception>
        public static T SingleOrDefault<T>(IEnumerable<T> seq)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            IList<T> list = seq as IList<T>;
            if (list != null)
            {
                switch (list.Count)
                {
                    case 0:
                        // no element => return default
                        return default(T);

                    case 1:
                        return list[0];
                }
            }
            else
            {
                using (IEnumerator<T> enumerator = seq.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                    {
                        // no element => return default
                        return default(T);
                    }

                    T result = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        return result;
                    }
                }
            }

            throw new InvalidOperationException(_ERR_MSG_SINGLE_MORE_THAN_ONE_ELEMENT);
        }

        #endregion Methods
    }
}
