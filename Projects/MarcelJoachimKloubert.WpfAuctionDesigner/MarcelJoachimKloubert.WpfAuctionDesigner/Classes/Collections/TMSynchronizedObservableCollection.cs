// 
// WPF based tool to create product pages for auctions on eBay, e.g.
// Copyright (C) 2013  Marcel Joachim Kloubert
//     
// This library is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or (at
// your option) any later version.
//     
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// General Public License for more details.
//     
// You should have received a copy of the GNU General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301,
// USA.
// 


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Collections
{
    /// <summary>
    /// Thread safe implementation of <see cref="ObservableCollection{T}" /> class.
    /// </summary>
    /// <typeparam name="T">type of the items.</typeparam>
    public class TMSynchronizedObservableCollection<T> : ObservableCollection<T>
    {
        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMSynchronizedObservableCollection{T}"/> class.
        /// </summary>
        /// <param name="syncRoot">The object for thread safe operations.</param>
        /// <param name="collection">The list of initial items.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> and/or <paramref name="collection" />
        /// is <see langword="null" />.
        /// </exception>
        public TMSynchronizedObservableCollection(object syncRoot, IEnumerable<T> collection)
            : base(collection: collection)
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            this.SyncRoot = syncRoot;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMSynchronizedObservableCollection{T}"/> class.
        /// </summary>
        /// <param name="syncRoot">The object for thread safe operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public TMSynchronizedObservableCollection(object syncRoot)
            : base()
        {
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }

            this.SyncRoot = syncRoot;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMSynchronizedObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="collection">The list of initial items.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection" /> is <see langword="null" />.
        /// </exception>
        public TMSynchronizedObservableCollection(IEnumerable<T> collection)
            : this(syncRoot: new object(),
                   collection: collection)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMSynchronizedObservableCollection{T}" /> class.
        /// </summary>
        public TMSynchronizedObservableCollection()
            : this(syncRoot: new object())
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the object for thread safe operations.
        /// </summary>
        public object SyncRoot
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (5)

        // Protected Methods (5) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObservableCollection{T}.ClearItems()" />
        protected override void ClearItems()
        {
            lock (this.SyncRoot)
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
            lock (this.SyncRoot)
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
            lock (this.SyncRoot)
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
            lock (this.SyncRoot)
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
            lock (this.SyncRoot)
            {
                base.SetItem(index, item);
            }
        }

        #endregion Methods
    }
}
