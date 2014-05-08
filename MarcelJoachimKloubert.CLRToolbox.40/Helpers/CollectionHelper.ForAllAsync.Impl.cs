// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Invokes an action for each item of a sequence. Each thrown exception while invokation
        /// is collected and thrown or returned as a single <see cref="AggregateException" />.
        /// The invokation of each action is done in an async thread, but it is wait until all actions were executed.
        /// </summary>
        /// <typeparam name="T">Type of the items of the sequence.</typeparam>
        /// <typeparam name="S">Type of the second paramater of <paramref name="action" />.</typeparam>
        /// <param name="items">The sequence.</param>
        /// <param name="action">The action to invoke for an item of <paramref name="items" />.</param>
        /// <param name="actionStateFactory">
        /// The factory delegate that produces the value for the state argument of <paramref name="action" />.
        /// </param>
        /// <param name="throwExceptions">Throw exception (<see langword="true" />) or return them (<see langword="false" />).</param>
        /// <returns>The list of thrown exception or <see langword="null" /> if no exception was thrown.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" />, <paramref name="action" /> and/or <paramref name="actionStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        /// <exception cref="AggregateException">
        /// At leat one exception was thrown. This only works if <paramref name="throwExceptions" />
        /// is <see langword="true" />.
        /// </exception>
        public static AggregateException ForAllAsync<T, S>(IEnumerable<T> items,
                                                           Action<IForAllItemExecutionContext<T, S>> action,
                                                           Func<T, long, S> actionStateFactory,
                                                           bool throwExceptions)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("actionStateFactory");
            }

            var exceptions = new List<Exception>();
            var syncRoot = new object();

            // create tuples from items
            long itemIndex = -1;
            var tuples = items.Select(i => new ForAllTuple<T, S>(
                    action: action,
                    actionStateFactory: actionStateFactory,
                    exceptions: exceptions,
                    index: ++itemIndex,
                    item: i,
                    syncRoot: syncRoot
                ));

            // create tasks from tuples
            var tasks = tuples.Select(t =>
                {
                    return new Task(action:
                        (state) =>
                        {
                            var tuple = (ForAllTuple<T, S>)state;

                            var ctx = new SimpleForAllItemExecutionContext<T, S>();
                            try
                            {
                                ctx.Index = tuple.INDEX;
                                ctx.Item = tuple.ITEM;
                                ctx.State = tuple.ACTION_STATE_FACTORY(ctx.Item, ctx.Index);

                                tuple.ACTION(ctx);
                            }
                            catch (Exception ex)
                            {
                                lock (tuple.SYNC)
                                {
                                    tuple.EXCEPTION_LIST
                                         .Add(new ForAllItemExecutionException<T, S>(ctx, ex));
                                }
                            }
                        }, state: t);
                });

            // start tasks and wait
            try
            {
                var runningTasks = new List<Task>();
                using (var enumerator = tasks.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        try
                        {
                            var t = enumerator.Current;

                            t.Start();
                            runningTasks.Add(t);
                        }
                        catch (Exception ex)
                        {
                            lock (syncRoot)
                            {
                                exceptions.Add(ex);
                            }
                        }
                    }
                }

                TaskHelper.WaitAll(runningTasks);
                runningTasks.Clear();
            }
            catch (Exception ex)
            {
                lock (syncRoot)
                {
                    exceptions.Add(ex);
                }
            }

            AggregateException result = null;
            lock (syncRoot)
            {
                if (exceptions.Count > 0)
                {
                    result = new AggregateException(exceptions);
                }
            }

            if (result != null &&
                throwExceptions)
            {
                throw result;
            }

            return result;
        }

        #endregion Methods

        #region Nested Classes (1)

        private sealed class ForAllTuple<T, S>
        {
            #region Fields (6)

            internal readonly Action<IForAllItemExecutionContext<T, S>> ACTION;
            internal readonly Func<T, long, S> ACTION_STATE_FACTORY;
            internal readonly ICollection<Exception> EXCEPTION_LIST;
            internal readonly long INDEX;
            internal readonly T ITEM;
            internal readonly object SYNC;

            #endregion Fields

            #region Constructors (1)

            internal ForAllTuple(long index,
                                 T item,
                                 Action<IForAllItemExecutionContext<T, S>> action,
                                 Func<T, long, S> actionStateFactory,
                                 ICollection<Exception> exceptions,
                                 object syncRoot)
            {
                this.ACTION = action;
                this.ACTION_STATE_FACTORY = actionStateFactory;
                this.EXCEPTION_LIST = exceptions;
                this.INDEX = index;
                this.ITEM = item;
                this.SYNC = syncRoot;
            }

            #endregion Constructors
        }

        #endregion Nested Classes
    }
}