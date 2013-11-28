// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Fields (1)

        private const string _ERR_MSG_SINGLE_NO_ELEMENTS = "The sequence contains no element!";

        #endregion Fields

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Tries to return the first element of a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <returns>The first element or the default value of the type.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> is <see langword="null" />.
        /// </exception>
        public static T Single<T>(IEnumerable<T> seq)
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
                        throw new InvalidOperationException(_ERR_MSG_SINGLE_NO_ELEMENTS);

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
                        throw new InvalidOperationException(_ERR_MSG_SINGLE_NO_ELEMENTS);
                    }

                    T result = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        return result;
                    }
                }
            }

            throw new InvalidOperationException("The sequence contains more than one element!");
        }

        #endregion Methods
    }
}
