// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.ObjectModel;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel
{
    /// <summary>
    /// A thread safe version of <see cref="ObservableCollection{T}" />.
    /// </summary>
    /// <typeparam name="T">Type of the items.</typeparam>
    public class SynchronizedObservableCollection<T> : ObservableCollection<T>
    {
        #region Fields (1)

        /// <summary>
        /// An uniue object for sync operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="SynchronizedObservableCollection{T}._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SynchronizedObservableCollection(object syncRoot)
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            this._SYNC = syncRoot;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedObservableCollection{T}" /> class.
        /// </summary>
        public SynchronizedObservableCollection()
            : this(new object())
        {

        }

        #endregion Constructors

        #region Methods (5)

        // Protected Methods (5) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObservableCollection{T}.ClearItems()" />
        protected override void ClearItems()
        {
            lock (this._SYNC)
            {
                base.ClearItems();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObservableCollection{T}.InsertItem(int, T)" />
        protected override void InsertItem(int index, T item)
        {
            lock (this._SYNC)
            {
                base.InsertItem(index, item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObservableCollection{T}.MoveItem(int, int)" />
        protected override void MoveItem(int oldIndex, int newIndex)
        {
            lock (this._SYNC)
            {
                base.MoveItem(oldIndex, newIndex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObservableCollection{T}.RemoveItem(int)" />
        protected override void RemoveItem(int index)
        {
            lock (this._SYNC)
            {
                base.RemoveItem(index);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObservableCollection{T}.SetItem(int, T)" />
        protected override void SetItem(int index, T item)
        {
            lock (this._SYNC)
            {
                base.SetItem(index, item);
            }
        }

        #endregion Methods
    }
}
