// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    /// <summary>
    /// A thread safe dictionary.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys.</typeparam>
    /// <typeparam name="TValue">Type of the values.</typeparam>
    public partial class SynchronizedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>
    {
        #region Fields (2)

        private readonly Dictionary<TKey, TValue> _DICT;
        private readonly object _SYNC;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="syncRoot">The object for the <see cref="SynchronizedDictionary{TKey, TValue}.SyncRoot" /> property.</param>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> and/or <paramref name="items" /> are <see langword="null" />.
        /// </exception>
        public SynchronizedDictionary(object syncRoot, IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this._SYNC = syncRoot;

            this._DICT = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> i in items)
            {
                this._DICT
                    .Add(i.Key, i.Value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="syncRoot">The object for the <see cref="SynchronizedDictionary{TKey, TValue}.SyncRoot" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedDictionary(object syncRoot)
            : this(syncRoot, new KeyValuePair<TKey, TValue>[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items)
            : this(new object(), items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        public SynchronizedDictionary()
            : this(new object())
        {
        }

        #endregion Constructors

        #region Properties (8)
        
        /// <inheriteddoc />
        public int Count
        {
            get
            {
                int result;

                lock (this._SYNC)
                {
                    result = this._DICT.Count;
                }

                return result;
            }
        }
        
        /// <inheriteddoc />
        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }
        
        /// <inheriteddoc />
        public bool IsReadOnly
        {
            get { return false; }
        }
        
        /// <inheriteddoc />
        public bool IsSynchronized
        {
            get { return true; }
        }
        
        /// <inheriteddoc />
        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> result;

                lock (this._SYNC)
                {
                    result = this._DICT.Keys;
                }

                return result;
            }
        }
        
        /// <inheriteddoc />
        public object SyncRoot
        {
            get { return this._SYNC; }
        }
        
        /// <inheriteddoc />
        public TValue this[TKey key]
        {
            get
            {
                TValue result;

                lock (this._SYNC)
                {
                    result = this._DICT[key];
                }

                return result;
            }

            set
            {
                lock (this._SYNC)
                {
                    this._DICT[key] = value;
                }
            }
        }
        
        /// <inheriteddoc />
        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> result;

                lock (this._SYNC)
                {
                    result = this._DICT.Values;
                }

                return result;
            }
        }

        #endregion Properties

        #region Methods (7)

        // Public Methods (7) 
        
        /// <inheriteddoc />
        public void Add(TKey key, TValue value)
        {
            lock (this._SYNC)
            {
                this._DICT.Add(key, value);
            }
        }
        
        /// <inheriteddoc />
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._DICT.Clear();
            }
        }
        
        /// <inheriteddoc />
        public bool ContainsKey(TKey key)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this._DICT.ContainsKey(key);
            }

            return result;
        }
        
        /// <inheriteddoc />
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            lock (this._SYNC)
            {
                ((ICollection<KeyValuePair<TKey, TValue>>)this._DICT).CopyTo(array, arrayIndex);
            }
        }
        
        /// <inheriteddoc />
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            IEnumerator<KeyValuePair<TKey, TValue>> result;

            lock (this._SYNC)
            {
                result = this._DICT.GetEnumerator();
            }

            return result;
        }
        
        /// <inheriteddoc />
        public bool Remove(TKey key)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this._DICT.Remove(key);
            }

            return result;
        }
        
        /// <inheriteddoc />
        public bool TryGetValue(TKey key, out TValue value)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this._DICT
                             .TryGetValue(key, out value);
            }

            return result;
        }

        #endregion Methods
    }
}