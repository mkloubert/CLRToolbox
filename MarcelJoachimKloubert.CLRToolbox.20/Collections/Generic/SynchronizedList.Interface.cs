// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    partial class SynchronizedList<T>
    {
        #region Properties (1)

        object IList.this[int index]
        {
            get { return this[index]; }

            set
            {
                this[index] = GlobalConverter.Current
                                             .ChangeType<T>(value);
            }
        }

        #endregion Properties

        #region Methods (7)

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            int result;

            lock (this._SYNC)
            {
                this._ITEMS
                    .Add(GlobalConverter.Current
                                        .ChangeType<T>(value));

                result = this._ITEMS.Count - 1;
            }

            return result;
        }

        bool IList.Contains(object value)
        {
            return ((IList)this).IndexOf(value) > -1;
        }

        /// <inheriteddoc />
        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((T[])array, index);
        }

        int IList.IndexOf(object value)
        {
            int result = -1;

            lock (this._SYNC)
            {
                for (int i = 0; i < this._ITEMS.Count; i++)
                {
                    if (object.Equals(value, this._ITEMS[i]))
                    {
                        result = i;
                        break;
                    }
                }
            }

            return result;
        }

        void IList.Insert(int index, object value)
        {
            this._ITEMS
                .Insert(index,
                        GlobalConverter.Current
                                       .ChangeType<T>(value));
        }

        void IList.Remove(object value)
        {
            lock (this._SYNC)
            {
                for (int i = 0; i < this._ITEMS.Count; i++)
                {
                    if (object.Equals(value, this._ITEMS[i]))
                    {
                        this._ITEMS.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        #endregion Methods
    }
}