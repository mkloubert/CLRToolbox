// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    /// <summary>
    /// A thread safe list.
    /// </summary>
    /// <typeparam name="T">Type of the items.</typeparam>
    public partial class SynchronizedList<T> : IList<T>, IList
    {
        #region Fields (2)

        private readonly List<T> _ITEMS;
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
            this._ITEMS = new List<T>();
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

        #region Properties (6)

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

        #endregion Properties

        #region Methods (10)

        // Public Methods (10) 
        
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
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
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

        #endregion Methods
    }
}