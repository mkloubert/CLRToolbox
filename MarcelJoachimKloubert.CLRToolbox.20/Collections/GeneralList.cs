// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections
{
    /// <summary>
    /// Simple implementation of <see cref="IGeneralList" /> based on <see cref="List{T}" />.
    /// </summary>
    public partial class GeneralList : List<object>, IGeneralList
    {
        #region Constructors (3)

        /// <inheriteddoc />
        public GeneralList(IEnumerable collection)
            : base(CollectionHelper.Cast<object>(collection))
        {
        }

        /// <inheriteddoc />
        public GeneralList(int capacity)
            : base(capacity)
        {
        }

        /// <inheriteddoc />
        public GeneralList()
            : base()
        {
        }

        #endregion Constructors

        #region Properties (2)

        /// <inheriteddoc />
        public bool IsEmpty
        {
            get { return this.Count == 0; }
        }

        /// <inheriteddoc />
        public bool IsNotEmpty
        {
            get { return this.IsEmpty == false; }
        }

        #endregion Properties

        #region Methods (32)

        // Public Methods (32) 

        /// <inheriteddoc />
        public void AddRange(IEnumerable items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            foreach (object i in items)
            {
                this.Add(i);
            }
        }

        /// <inheriteddoc />
        public IList<object> AsList()
        {
            return this;
        }

        /// <inheriteddoc />
        public IList<T> AsList<T>()
        {
            return (this as IList<T>) ?? ToList<T>();
        }

        /// <inheriteddoc />
        public IEnumerable<T> Cast<T>()
        {
            return this.Select<T>(delegate(object item, int i)
                                  {
                                      return GlobalConverter.Current
                                                            .ChangeType<T>(item);
                                  });
        }

        /// <inheriteddoc />
        public GeneralList Clone()
        {
            return new GeneralList(this);
        }

        /// <inheriteddoc />
        public void ForEach<T>(Action<T, int> action)
        {
            ForEach<T>(action, false);
        }

        /// <inheriteddoc />
        public void ForEach<T>(Action<T, int> action, bool takeAll)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.ForEach<T, object>(delegate(T item, int index, object state)
                                    {
                                        action(item, index);
                                    }, takeAll);
        }

        /// <inheriteddoc />
        public void ForEach<T, TState>(Action<T, int, TState> action, TState actionState)
        {
            ForEach<T, TState>(action, actionState, false);
        }

        /// <inheriteddoc />
        public void ForEach<T, TState>(Action<T, int, TState> action, TState actionState, bool takeAll)
        {
            this.ForEach<T, TState>(action,
                                    delegate(int index)
                                    {
                                        return actionState;
                                    });
        }

        /// <inheriteddoc />
        public void ForEach<T, TState>(Action<T, int, TState> action, Func<int, TState> actionStateFactory)
        {
            ForEach<T, TState>(action, actionStateFactory, false);
        }

        /// <inheriteddoc />
        public void ForEach<T, TState>(Action<T, int, TState> action, Func<int, TState> actionStateFactory, bool takeAll)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("actionStateFactory");
            }

            using (IEnumerator<object> e = this.GetEnumerator())
            {
                int index = -1;

                while (e.MoveNext())
                {
                    ++index;

                    object item = e.Current;
                    if (item is T || takeAll)
                    {
                        action(GlobalConverter.Current
                                              .ChangeType<T>(item),
                               index,
                               actionStateFactory(index));
                    }
                }
            }
        }

        /// <inheriteddoc />
        public virtual T GetItem<T>(int index)
        {
            return GlobalConverter.Current
                                  .ChangeType<T>(this[index]);
        }

        /// <inheriteddoc />
        public TResult Materialize<TResult>(Func<GeneralList, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.Materialize<object, TResult>(delegate(GeneralList l, object state)
                                                     {
                                                         return func(l);
                                                     },
                                                     (object)null);
        }

        /// <inheriteddoc />
        public TResult Materialize<TState, TResult>(Func<GeneralList, TState, TResult> func, TState funcState)
        {
            return this.Materialize<TState, TResult>(func,
                                                     delegate(GeneralList l)
                                                     {
                                                         return funcState;
                                                     });
        }

        /// <inheriteddoc />
        public TResult Materialize<TState, TResult>(Func<GeneralList, TState, TResult> func, Func<GeneralList, TState> funcStateFactory)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            return func(this,
                        funcStateFactory(this));
        }

        /// <inheriteddoc />
        public IEnumerable<T> OfType<T>()
        {
            foreach (object item in this)
            {
                if (item is T)
                {
                    yield return (T)item;
                }
            }
        }

        /// <inheriteddoc />
        public IEnumerable<object> Randomize()
        {
            return this.Randomize(new Random());
        }

        /// <inheriteddoc />
        public IEnumerable<object> Randomize(Random rand)
        {
            if (rand == null)
            {
                throw new ArgumentNullException("rand");
            }

            List<int> availableIndices = new List<int>(CollectionHelper.Range(this.Count));
            while (availableIndices.Count > 0)
            {
                int i = rand.Next(0, availableIndices.Count);

                try
                {
                    yield return this[availableIndices[i]];
                }
                finally
                {
                    availableIndices.RemoveAt(i);
                }
            }
        }

        /// <inheriteddoc />
        public IEnumerable<T> Select<T>(Func<object, int, T> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            return this.Select<object, T>(delegate(object currentItem, int index, object state)
                                          {
                                              return selector(currentItem, index);
                                          }, (object)null);
        }

        /// <inheriteddoc />
        public IEnumerable<T> Select<TState, T>(Func<object, int, TState, T> selector, TState selectState)
        {
            return this.Select<TState, T>(selector,
                                          delegate(object currentItem, int index)
                                          {
                                              return selectState;
                                          });
        }

        /// <inheriteddoc />
        public IEnumerable<T> Select<TState, T>(Func<object, int, TState, T> selector, Func<object, int, TState> selectStateFactory)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            if (selectStateFactory == null)
            {
                throw new ArgumentNullException("selectStateFactory");
            }

            using (IEnumerator<object> e = this.GetEnumerator())
            {
                int index = -1;

                while (e.MoveNext())
                {
                    object input = this[++index];

                    yield return selector(input, index,
                                          selectStateFactory(input, index));
                }
            }
        }

        /// <inheriteddoc />
        public void Shuffle()
        {
            this.Shuffle(new Random());
        }

        /// <inheriteddoc />
        public void Shuffle(Random rand)
        {
            if (rand == null)
            {
                throw new ArgumentNullException("rand");
            }

            for (int i = 0; i < this.Count; i++)
            {
                int newIdx = rand.Next(0, this.Count);
                object temp = this[i];

                this[i] = this[newIdx];
                this[newIdx] = temp;
            }
        }

        /// <inheriteddoc />
        public T[] ToArray<T>()
        {
            return this.ToArray<T>(false);
        }

        /// <inheriteddoc />
        public T[] ToArray<T>(bool ofType)
        {
            IEnumerable<T> items;
            if (ofType)
            {
                items = this.OfType<T>();
            }
            else
            {
                // cast instead
                items = this.Cast<T>();
            }

            return new TMArrayBuffer<T>(items).ToArray();
        }

        /// <inheriteddoc />
        public IList<object> ToList()
        {
            return this.ToList<object>();
        }

        /// <inheriteddoc />
        public IList<T> ToList<T>()
        {
            return this.ToList<T>(false);
        }

        /// <inheriteddoc />
        public IList<T> ToList<T>(bool ofType)
        {
            IEnumerable<T> items;
            if (ofType)
            {
                items = this.OfType<T>();
            }
            else
            {
                // cast instead
                items = this.Cast<T>();
            }

            return new List<T>(items);
        }

        /// <inheriteddoc />
        public void Transform<TNew>()
        {
            this.Transform<TNew>(delegate(object currentItem, int index)
                                 {
                                     return GlobalConverter.Current
                                                           .ChangeType<TNew>(currentItem);
                                 });
        }

        /// <inheriteddoc />
        public void Transform<TNew>(Func<object, int, TNew> transformer)
        {
            this.Transform<object, TNew>(delegate(object currentItem, int index, object state)
                                         {
                                             return transformer(currentItem, index);
                                         }, (object)null);
        }

        /// <inheriteddoc />
        public void Transform<TState, TNew>(Func<object, int, TState, TNew> transformer, TState transState)
        {
            this.Transform<TState, TNew>(transformer,
                                         delegate(object currentItem, int index)
                                         {
                                             return transState;
                                         });
        }

        /// <inheriteddoc />
        public void Transform<TState, TNew>(Func<object, int, TState, TNew> transformer, Func<object, int, TState> transStateFactory)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException("transformer");
            }

            if (transStateFactory == null)
            {
                throw new ArgumentNullException("transStateFactory");
            }

            using (IEnumerator<object> e = this.GetEnumerator())
            {
                int index = -1;

                while (e.MoveNext())
                {
                    object input = this[++index];

                    this[index] = transformer(input, index,
                                              transStateFactory(input, index));
                }
            }
        }

        #endregion Methods
    }
}