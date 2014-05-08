// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    /// <summary>
    /// A thread safe dictionary.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys.</typeparam>
    /// <typeparam name="TValue">Type of the values.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    [ComVisible(false)]
    [DebuggerDisplay("SynchronizedDictionary<{TypeOfKeys}, {TypeOfValues}>.Count = {Count}")]
    public partial class SynchronizedDictionary<TKey, TValue> :
#if !WINDOWS_PHONE
        global::System.MarshalByRefObject,
#endif
        IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>
    {
        #region Fields (2)

        private readonly Dictionary<TKey, TValue> _DICT;
#if !WINDOWS_PHONE
        [global::System.NonSerialized]
#endif
        private readonly object _SYNC;

        #endregion Fields

        #region Constructors (7)

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="syncRoot">The object for the <see cref="SynchronizedDictionary{TKey, TValue}.SyncRoot" /> property.</param>
        /// <param name="items">The items to add.</param>
        /// <param name="keyComparer">
        /// The comparer for the keys to use.
        /// If <see langword="null" /> the default comparer is used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> and/or <paramref name="items" /> are <see langword="null" />.
        /// </exception>
        public SynchronizedDictionary(object syncRoot, IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey> keyComparer)
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

            if (keyComparer == null)
            {
                // try find default comparer by another way
                // like checking sequence of submitted items

                Dictionary<TKey, TValue> itemsAsDict = items as Dictionary<TKey, TValue>;
                if (itemsAsDict != null)
                {
                    // use the comparer from 'Comparer' dictionary
                    keyComparer = itemsAsDict.Comparer;
                }
            }

            // create oinner dictionary
            if (keyComparer == null)
            {
                this._DICT = new Dictionary<TKey, TValue>();
            }
            else
            {
                this._DICT = new Dictionary<TKey, TValue>(keyComparer);
            }

            // copy items
            foreach (KeyValuePair<TKey, TValue> i in items)
            {
                this._DICT
                    .Add(i.Key, i.Value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <param name="keyComparer">
        /// The comparer for the keys to use.
        /// If <see langword="null" /> the default comparer is used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey> keyComparer)
            : this(new object(), items, keyComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="syncRoot">The object for the <see cref="SynchronizedDictionary{TKey, TValue}.SyncRoot" /> property.</param>
        /// <param name="keyComparer">
        /// The comparer for the keys to use.
        /// If <see langword="null" /> the default comparer is used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedDictionary(object syncRoot, IEqualityComparer<TKey> keyComparer)
            : this(syncRoot, new KeyValuePair<TKey, TValue>[0], keyComparer)
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
            : this(items, (IEqualityComparer<TKey>)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="syncRoot">The object for the <see cref="SynchronizedDictionary{TKey, TValue}.SyncRoot" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedDictionary(object syncRoot)
            : this(syncRoot, (IEqualityComparer<TKey>)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="keyComparer">
        /// The comparer for the keys to use.
        /// If <see langword="null" /> the default comparer is used.
        /// </param>
        public SynchronizedDictionary(IEqualityComparer<TKey> keyComparer)
            : this(new object(), keyComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedDictionary{TKey, TValue}" /> class.
        /// </summary>
        public SynchronizedDictionary()
            : this((IEqualityComparer<TKey>)null)
        {
        }

        #endregion Constructors

        #region Properties (10)

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

        /// <summary>
        /// Gets the <see cref="Type" /> object of the generic parameter that is defined for the keys.
        /// </summary>
        public Type TypeOfKeys
        {
            get { return typeof(TKey); }
        }

        /// <summary>
        /// Gets the <see cref="Type" /> object of the generic parameter that is defined for the values.
        /// </summary>
        public Type TypeOfValues
        {
            get { return typeof(TValue); }
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