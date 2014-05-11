// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    /// <summary>
    /// A thread safe list.
    /// </summary>
    /// <typeparam name="T">Type of the items.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    [DebuggerDisplay("SynchronizedList<{TypeOfItems}>.Count = {Count}")]
    public partial class SynchronizedList<T> :
#if !WINDOWS_PHONE
        global::System.MarshalByRefObject,
#endif
 IList<T>, IList, IReadOnlyList<T>
    {
        #region Fields (2)

        private readonly List<T> _ITEMS;
#if !WINDOWS_PHONE
        [global::System.NonSerialized]
#endif
        private readonly object _SYNC;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedList{T}" /> class.
        /// </summary>
        /// <param name="syncRoot">The object for the <see cref="SynchronizedList{T}.SyncRoot" /> property.</param>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> and/or <paramref name="items" /> are <see langword="null" />.
        /// </exception>
        public SynchronizedList(object syncRoot, IEnumerable<T> items)
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
            this._ITEMS = new List<T>(items);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedList{T}" /> class.
        /// </summary>
        /// <param name="syncRoot">The object for the <see cref="SynchronizedList{T}.SyncRoot" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedList(object syncRoot)
            : this(syncRoot, new T[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedList{T}" /> class.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedList(IEnumerable<T> items)
            : this(new object(), items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedList{T}" /> class.
        /// </summary>
        public SynchronizedList()
            : this(new object())
        {
        }

        #endregion Constructors

        #region Properties (7)

        /// <inheriteddoc />
        public int Count
        {
            get
            {
                int result;

                lock (this._SYNC)
                {
                    result = this._ITEMS.Count;
                }

                return result;
            }
        }

        /// <inheriteddoc />
        public bool IsFixedSize
        {
            get { return false; }
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
        public object SyncRoot
        {
            get { return this._SYNC; }
        }

        /// <inheriteddoc />
        public T this[int index]
        {
            get
            {
                T result;

                lock (this._SYNC)
                {
                    result = this._ITEMS[index];
                }

                return result;
            }

            set
            {
                lock (this._SYNC)
                {
                    this._ITEMS[index] = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Type" /> object of the generic parameter that is defined for the items.
        /// </summary>
        public Type TypeOfItems
        {
            get { return typeof(T); }
        }

        #endregion Properties

        #region Methods (11)

        // Public Methods (11) 
        
        /// <inheriteddoc />
        public void Add(T item)
        {
            lock (this._SYNC)
            {
                this._ITEMS.Add(item);
            }
        }

        /// <inheriteddoc />
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._ITEMS.Clear();
            }
        }

        /// <inheriteddoc />
        public bool Contains(T item)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this._ITEMS.Contains(item);
            }

            return result;
        }

        /// <inheriteddoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this._SYNC)
            {
                this._ITEMS.CopyTo(array, arrayIndex);
            }
        }

        /// <inheriteddoc />
        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> result;

            lock (this._SYNC)
            {
                result = this._ITEMS.GetEnumerator();
            }

            return result;
        }

        /// <inheriteddoc />
        public int IndexOf(T item)
        {
            int result;

            lock (this._SYNC)
            {
                result = this._ITEMS.IndexOf(item);
            }

            return result;
        }

        /// <inheriteddoc />
        public void Insert(int index, T item)
        {
            lock (this._SYNC)
            {
                this._ITEMS.Insert(index, item);
            }
        }

        /// <inheriteddoc />
        public bool Remove(T item)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this._ITEMS.Remove(item);
            }

            return result;
        }

        /// <inheriteddoc />
        public void RemoveAt(int index)
        {
            lock (this._SYNC)
            {
                this._ITEMS.RemoveAt(index);
            }
        }

        /// <summary>
        /// Returns the items of that list as new array.
        /// </summary>
        /// <returns>That list as new array.</returns>
        public T[] ToArray()
        {
            T[] result;

            lock (this._SYNC)
            {
                result = new T[this._ITEMS.Count];
                this._ITEMS.CopyTo(result, 0);
            }

            return result;
        }

        /// <summary>
        /// Creates a copy of that list as <see cref="List{T}" /> object.
        /// </summary>
        /// <returns>The copy of that list.</returns>
        public List<T> ToList()
        {
            List<T> result;

            lock (this._SYNC)
            {
                result = new List<T>(this._ITEMS);
            }

            return result;
        }

        #endregion Methods
    }
}