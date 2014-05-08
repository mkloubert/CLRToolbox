// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections;
using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Extended foreach operation for a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        public static void ForEach<T>(IEnumerable<T> seq, Action<IForEachItemExecutionContext<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            ForEach<T, object>(seq,
                               delegate(IForEachItemExecutionContext<T, object> ctx)
                               {
                                   action(ctx);
                               },
                               (object)null);
        }

        /// <summary>
        /// Extended foreach operation for a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">
        /// The state object for argument of <paramref name="action" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" /> and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        public static void ForEach<T, S>(IEnumerable<T> seq, Action<IForEachItemExecutionContext<T, S>> action, S actionState)
        {
            ForEach<T, S>(seq,
                          action,
                          delegate(T item, long index)
                          {
                              return actionState;
                          });
        }

        /// <summary>
        /// Extended foreach operation for a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">
        /// The function that returns the state object for argument of <paramref name="action" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="seq" />, <paramref name="action" /> and/or
        /// <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        public static void ForEach<T, S>(IEnumerable<T> seq, Action<IForEachItemExecutionContext<T, S>> action, Func<T, long, S> actionStateFactory)
        {
            if (seq == null)
            {
                throw new ArgumentNullException("seq");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("actionStateFactory");
            }

            IGeneralList genList = seq as IGeneralList;
            if (genList != null)
            {
                genList.ForEach<T, S>(action, actionStateFactory);
                return;
            }

            using (IEnumerator<T> e = seq.GetEnumerator())
            {
                long index = -1;

                while (e.MoveNext())
                {
                    SimpleForEachItemExecutionContext<T, S> ctx = new SimpleForEachItemExecutionContext<T, S>();
                    ctx.Cancel = false;
                    ctx.Index = ++index;
                    ctx.Item = e.Current;
                    ctx.State = actionStateFactory(ctx.Item, ctx.Index);

                    action(ctx);

                    if (ctx.Cancel)
                    {
                        // cancel whole operation
                        break;
                    }
                }
            }
        }

        #endregion Methods
    }
}