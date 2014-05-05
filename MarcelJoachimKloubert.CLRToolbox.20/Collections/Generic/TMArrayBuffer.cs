// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    internal struct TMArrayBuffer<T>
    {
        #region Data Members (2)
        
        private readonly T[] _BUFFER;
        private readonly int _COUNT;

        #endregion Data Members

        #region Methods (2)

        internal TMArrayBuffer(IEnumerable<T> seq)
        {
            T[] items = null;
            int ct;

            ICollection<T> coll = seq as ICollection<T>;
            if (coll != null)
            {
                ct = coll.Count;
                if (ct > 0)
                {
                    items = new T[ct];
                    coll.CopyTo(items, 0);
                }
            }
            else
            {
                ct = 0;

                using (IEnumerator<T> e = seq.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (items != null)
                        {
                            if (items.Length == ct)
                            {
                                T[] temp = new T[checked(ct * 2)];
                                Array.Copy(items, 0,
                                           temp, 0,
                                           ct);

                                items = temp;
                            }
                        }
                        else
                        {
                            // not initialized yet
                            items = new T[1];
                        }

                        items[ct] = e.Current;
                        ++ct;
                    }
                }
            }

            this._BUFFER = items;
            this._COUNT = ct < 1 ? 0 : ct;
        }

        internal T[] ToArray()
        {
            if (this._COUNT == 0)
            {
                return new T[0];
            }

            if (this._BUFFER.Length == this._COUNT)
            {
                return this._BUFFER;
            }

            T[] result = new T[this._COUNT];
            Array.Copy(this._BUFFER, 0,
                       result, 0,
                       this._COUNT);

            return result;
        }

        #endregion Methods
    }
}