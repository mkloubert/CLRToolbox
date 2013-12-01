// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel
{
    /// <summary>
    /// A simple and common implementation of an <see cref="IReadOnlyDictionary{TKey, TValue}" /> based object.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys.</typeparam>
    /// <typeparam name="TValue">Type of the values.</typeparam>
    public class TMReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        #region Fields (1)

        private readonly IDictionary<TKey, TValue> _DICTIONARY;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMReadOnlyDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The value for the <see cref="TMReadOnlyDictionary{TKey, TValue}._DICTIONARY" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary" /> is <see langword="null" />.
        /// </exception>
        public TMReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            this._DICTIONARY = dictionary;
        }

        #endregion Constructors

        #region Properties (4)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IReadOnlyCollection{T}.Count" />
        public int Count
        {
            get { return this._DICTIONARY.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IReadOnlyDictionary{TKey, TValue}.Keys" />
        public IEnumerable<TKey> Keys
        {
            get { return this._DICTIONARY.Keys; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IReadOnlyDictionary{TKey, TValue}.this[TKey]" />
        public TValue this[TKey key]
        {
            get { return this._DICTIONARY[key]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IReadOnlyDictionary{TKey, TValue}.Values" />
        public IEnumerable<TValue> Values
        {
            get { return this._DICTIONARY.Values; }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IReadOnlyDictionary{TKey, TValue}.ContainsKey(TKey)" />
        public bool ContainsKey(TKey key)
        {
            return this._DICTIONARY.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEnumerable{T}.GetEnumerator()" />
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this._DICTIONARY
                       .GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IReadOnlyDictionary{TKey, TValue}.TryGetValue(TKey, out TValue)" />
        public bool TryGetValue(TKey key, out TValue value)
        {
            return this._DICTIONARY
                       .TryGetValue(key, out value);
        }
        // Private Methods (1) 

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods
    }
}
