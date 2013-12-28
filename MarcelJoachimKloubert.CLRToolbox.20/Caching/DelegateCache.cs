// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Caching
{
    /// <summary>
    /// Caches delegate operations.
    /// </summary>
    public sealed partial class DelegateCache : DisposableBase
    {
        #region Fields (1)

        private readonly IList<CachedItem> _ITEMS = new List<CachedItem>();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCache" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DelegateCache(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCache" /> class.
        /// </summary>
        public DelegateCache()
            : base()
        {

        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// Describes a cached action.
        /// </summary>
        public delegate void CachedAction();

        /// <summary>
        /// Describes a cached function.
        /// </summary>
        /// <returns>The result of the function.</returns>
        public delegate T CachedFunc<T>();

        #endregion Delegates and Events

        #region Methods (17)

        // Public Methods (11) 

        /// <summary>
        /// Removes all cached delegates.
        /// </summary>
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._ITEMS.Clear();
            }
        }

        /// <summary>
        /// Invoked an action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public void InvokeAction(CachedAction action)
        {
            this.InvokeInner(action);
        }

        /// <summary>
        /// Invoked a function.
        /// </summary>
        /// <typeparam name="T">Result type of the function.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <returns>The result of the function.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        public T InvokeFunc<T>(CachedFunc<T> func)
        {
            return GlobalConverter.Current
                                  .ChangeType<T>(this.InvokeInner(func));
        }

        /// <summary>
        /// Removes an action from the cache.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        /// <returns>Action was removed or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public bool RemoveAction(CachedAction action)
        {
            return this.RemoveInner(action);
        }

        /// <summary>
        /// Removes a function from the cache.
        /// </summary>
        /// <typeparam name="T">Result type of the function.</typeparam>
        /// <param name="func">The function to remove.</param>
        /// <returns>Function was removed or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        public bool RemoveFunc<T>(CachedFunc<T> func)
        {
            return this.RemoveInner(func);
        }

        /// <summary>
        /// Resets the state of an action.
        /// </summary>
        /// <param name="action">The action to reset.</param>
        /// <returns>Action was resetted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public bool ResetAction(CachedAction action)
        {
            return this.ResetInner(action);
        }

        /// <summary>
        /// Resets the state of a function.
        /// </summary>
        /// <typeparam name="T">Result type of the function.</typeparam>
        /// <param name="func">The function to reset.</param>
        /// <returns>Function was resetted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        public bool ResetFunc<T>(CachedFunc<T> func)
        {
            return this.ResetInner(func);
        }

        /// <summary>
        /// Caches an action without a timeout.
        /// </summary>
        /// <param name="action">The action to cache.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public void SaveAction(CachedAction action)
        {
            this.SaveInner(action, null);
        }

        /// <summary>
        /// Caches an action.
        /// </summary>
        /// <param name="action">The action to cache.</param>
        /// <param name="timeout">The timeout / update interval.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public void SaveAction(CachedAction action, TimeSpan timeout)
        {
            this.SaveInner(action, timeout);
        }

        /// <summary>
        /// Caches a function without a timeout.
        /// </summary>
        /// <typeparam name="T">Result type of the function.</typeparam>
        /// <param name="func">The function to cache.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        public void SaveFunc<T>(CachedFunc<T> func)
        {
            this.SaveInner(func, null);
        }

        /// <summary>
        /// Caches a function without a timeout.
        /// </summary>
        /// <typeparam name="T">Result type of the function.</typeparam>
        /// <param name="func">The function to cache.</param>
        /// <param name="timeout">The timeout / update interval.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        public void SaveFunc<T>(CachedFunc<T> func, TimeSpan timeout)
        {
            this.SaveInner(func, timeout);
        }
        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DisposableBase.OnDispose(bool)" />
        protected override void OnDispose(bool disposing)
        {
            this._ITEMS
                .Clear();
        }
        // Private Methods (5) 

        private object InvokeInner(Delegate @delegate, params object[] args)
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException("delegate");
            }

            CachedItem item;
            lock (this._SYNC)
            {
                item = this.TryFindCachedItem(@delegate);
            }

            if (item != null)
            {
                return item.Invoke(args);
            }
            else
            {
                // not cached => simply invoke

                return @delegate.Method
                                .Invoke(@delegate.Target,
                                        args ?? new object[] { null });
            }
        }

        private bool RemoveInner(Delegate @delegate)
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException("delegate");
            }

            bool result = false;

            lock (this._SYNC)
            {
                CachedItem item = this.TryFindCachedItem(@delegate);
                if (item != null)
                {
                    result = this._ITEMS.Remove(item);
                }
            }

            return result;
        }

        private bool ResetInner(Delegate @delegate)
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException("delegate");
            }

            CachedItem item;
            lock (this._SYNC)
            {
                item = this.TryFindCachedItem(@delegate);
            }

            if (item != null)
            {
                return item.Reset();
            }

            return false;
        }

        private void SaveInner(Delegate @delegate, TimeSpan? timeout)
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException("delegate");
            }

            lock (this._SYNC)
            {
                CachedItem item = this.TryFindCachedItem(@delegate);

                if (item != null)
                {
                    item.Timeout = timeout;
                }
                else
                {
                    this._ITEMS
                        .Add(new CachedItem(@delegate,
                                            timeout));
                }
            }
        }

        private CachedItem TryFindCachedItem(Delegate @delegate)
        {
            return CollectionHelper.SingleOrDefault(this._ITEMS,
                                                    delegate(CachedItem item)
                                                    {
                                                        return item.Equals(@delegate);
                                                    });
        }

        #endregion Methods
    }
}
