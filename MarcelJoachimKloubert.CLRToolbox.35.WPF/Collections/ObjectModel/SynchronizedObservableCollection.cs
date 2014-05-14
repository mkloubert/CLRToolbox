// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
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

        #region Methods (8)

        // Public Methods (1) 

        /// <summary>
        /// Adds a list of items.
        /// </summary>
        /// <param name="itemsToAdd">The items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="itemsToAdd" /> is <see langword="null" />.
        /// </exception>
        public void AddRange(IEnumerable<T> itemsToAdd)
        {
            if (itemsToAdd == null)
            {
                throw new ArgumentNullException("itemsToAdd");
            }

            CollectionHelper.ForEach(itemsToAdd,
                                     ctx =>
                                     {
                                         ctx.State
                                            .Collection.Add(ctx.Item);
                                     },
                                     new
                                     {
                                         Collection = this,
                                     });
        }

        // Protected Methods (7) 

        /// <inheriteddoc />
        protected override void ClearItems()
        {
            this.InvokeForCollection((coll) => base.ClearItems());
        }

        /// <inheriteddoc />
        protected override void InsertItem(int index, T item)
        {
            this.InvokeForCollection((coll, state) => base.InsertItem(state.Index, state.Item),
                                     new
                                     {
                                         Index = index,
                                         Item = item,
                                     });
        }

        /// <summary>
        /// Invokes an action for that collection.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        protected void InvokeForCollection(Action<SynchronizedObservableCollection<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.InvokeForCollection<object>((coll, state) => action(coll),
                                             (object)null);
        }

        /// <summary>
        /// Invokes an action for that collection.
        /// </summary>
        /// <typeparam name="S">Type of the state object for <paramref name="action" />.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">The additional object for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        protected virtual void InvokeForCollection<S>(Action<SynchronizedObservableCollection<T>, S> action, S actionState)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            lock (this._SYNC)
            {
                action(this, actionState);
            }
        }

        /// <inheriteddoc />
        protected override void MoveItem(int oldIndex, int newIndex)
        {
            this.InvokeForCollection((coll, state) => base.MoveItem(state.OldIndex, state.NewIndex),
                                     new
                                     {
                                         OldIndex = oldIndex,
                                         NewIndex = newIndex,
                                     });
        }

        /// <inheriteddoc />
        protected override void RemoveItem(int index)
        {
            this.InvokeForCollection((coll, state) => base.RemoveItem(state.Index),
                                     new
                                     {
                                         Index = index,
                                     });
        }

        /// <inheriteddoc />
        protected override void SetItem(int index, T item)
        {
            this.InvokeForCollection((coll, state) => base.SetItem(state.Index, state.Item),
                                     new
                                     {
                                         Index = index,
                                         Item = item,
                                     });
        }

        #endregion Methods
    }
}