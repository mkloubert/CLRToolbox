// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Caching
{
    partial class DelegateCache
    {
        #region Nested Classes (1)

        private sealed class CachedItem : IEquatable<Delegate>
        {
            #region Fields (7)

            private DateTimeOffset? _cachedUntil;
            private bool _hasBeenInvoked;
            private object _lastResult;
            private readonly object _SYNC = new object();
            internal TimeSpan? Timeout;
            internal readonly Delegate DELEGATE;

            #endregion Fields

            #region Constructors (1)

            internal CachedItem(Delegate @delegate, TimeSpan? timeout)
            {
                this.DELEGATE = @delegate;
                this.Timeout = timeout;

                this.UpdateCachedUntil();
            }

            #endregion Constructors

            #region Properties (2)

            internal bool HasExpired
            {
                get
                {
                    DateTimeOffset now = this.GetNow();

                    DateTimeOffset? cu = this._cachedUntil;
                    return cu.HasValue &&
                           (cu.Value < now);
                }
            }

            internal object LastResult
            {
                get { return this._lastResult; }

                private set { this._lastResult = value; }
            }

            #endregion Properties

            #region Methods (7)

            // Public Methods (3) 

            public bool Equals(Delegate other)
            {
                return object.Equals(this.DELEGATE, other);
            }

            public override bool Equals(object other)
            {
                if (other is Delegate)
                {
                    return this.Equals((Delegate)other);
                }

                return base.Equals(other);
            }

            public override int GetHashCode()
            {
                return this.DELEGATE == null ? 0 : this.DELEGATE.GetHashCode();
            }

            // Private Methods (2) 

            private DateTimeOffset GetNow()
            {
                return DateTimeOffset.Now;
            }

            private void UpdateCachedUntil()
            {
                DateTimeOffset now = this.GetNow();

                TimeSpan? to = this.Timeout;
                if (to.HasValue)
                {
                    this._cachedUntil = now.Add(to.Value);
                }
                else
                {
                    this._cachedUntil = null;
                }
            }

            // Internal Methods (2) 

            internal object Invoke(params object[] args)
            {
                object result;

                lock (this._SYNC)
                {
                    if (this.HasExpired ||
                        (this._hasBeenInvoked == false))
                    {
                        this._hasBeenInvoked = true;

                        this.LastResult = this.DELEGATE
                                              .Method
                                              .Invoke(this.DELEGATE.Target,
                                                      args ?? new object[] { null });

                        this.UpdateCachedUntil();
                    }

                    result = this.LastResult;
                }

                return result;
            }

            internal bool Reset()
            {
                bool result;

                lock (this._SYNC)
                {
                    result = !(this._hasBeenInvoked = false);
                }

                return result;
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}