// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    partial class SynchronizedDictionary<TKey, TValue>
    {
        #region Properties (5)

        ICollection IDictionary.Keys
        {
            get
            {
                ICollection result;

                lock (this._SYNC)
                {
                    result = ((IDictionary)this._DICT).Keys;
                }

                return result;
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                return this[GlobalConverter.Current
                                           .ChangeType<TKey>(key)];
            }

            set
            {
                this[GlobalConverter.Current
                                    .ChangeType<TKey>(key)] = GlobalConverter.Current
                                                                             .ChangeType<TValue>(value);
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                ICollection result;

                lock (this._SYNC)
                {
                    result = ((IDictionary)this._DICT).Values;
                }

                return result;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get { return this.Keys; }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get { return this.Values; }
        }

        #endregion Properties

        #region Methods (6)

        // Private Methods (6) 

        /// <inheriteddoc />
        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
        }

        void IDictionary.Add(object key, object value)
        {
            this.Add(GlobalConverter.Current
                                    .ChangeType<TKey>(key),
                     GlobalConverter.Current
                                    .ChangeType<TValue>(value));
        }

        bool IDictionary.Contains(object key)
        {
            return this.ContainsKey(GlobalConverter.Current
                                                   .ChangeType<TKey>(key));
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            IDictionaryEnumerator result;

            lock (this._SYNC)
            {
                result = ((IDictionary)this._DICT).GetEnumerator();
            }

            return result;
        }

        void IDictionary.Remove(object key)
        {
            this.Remove(GlobalConverter.Current
                                       .ChangeType<TKey>(key));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            bool result;

            lock (this._SYNC)
            {
                result = ((ICollection<KeyValuePair<TKey, TValue>>)this._DICT).Remove(item);
            }

            return result;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            lock (this._SYNC)
            {
                ((ICollection<KeyValuePair<TKey, TValue>>)this._DICT).Add(item);
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            bool result;

            lock (this._SYNC)
            {
                result = ((ICollection<KeyValuePair<TKey, TValue>>)this._DICT).Contains(item);
            }

            return result;
        }
    }
}