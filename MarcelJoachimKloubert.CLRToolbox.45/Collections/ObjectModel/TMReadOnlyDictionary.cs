// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel
{
    /// <summary>
    /// A simple and common implementation of an <see cref="IReadOnlyDictionary{TKey, TValue}" /> based object.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys.</typeparam>
    /// <typeparam name="TValue">Type of the values.</typeparam>
    public class TMReadOnlyDictionary<TKey, TValue> : ReadOnlyDictionary<TKey, TValue>,
                                                      IReadOnlyDictionary<TKey, TValue>
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMReadOnlyDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The inner dictionary to use for all operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary" /> is <see langword="null" />.
        /// </exception>
        public TMReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {

        }

        #endregion Constructors
    }
}
