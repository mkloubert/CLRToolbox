// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class CollectionHelper
    {
        #region Methods (5)

        // Public Methods (5) 

        /// <summary>
        /// Invokes an action for each item of a sequence. Each thrown exception while invokation
        /// is collected and thrown or returned as an <see cref="AggregateException" />.
        /// The invokation of each action is done in an async thread, but it is wait until all actions were executed.
        /// </summary>
        /// <typeparam name="T">Type of the items of the sequence.</typeparam>
        /// <param name="items">The sequence.</param>
        /// <param name="action">The action to invoke for an item of <paramref name="items" />.</param>
        /// <returns>The list of thrown exception or <see langword="null" /> if no exception was thrown.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" /> and/or <paramref name="action" />
        /// are <see langword="null" />.
        /// </exception>
        /// <exception cref="AggregateException">At leat one exception was thrown.</exception>
        public static AggregateException ForAllAsync<T>(IEnumerable<T> items,
                                                        Action<IForAllItemExecutionContext<T>> action)
        {
            return ForAllAsync<T>(items,
                                  action,
                                  true);
        }

        /// <summary>
        /// Invokes an action for each item of a sequence. Each thrown exception while invokation
        /// is collected and thrown or returned as an <see cref="AggregateException" />.
        /// The invokation of each action is done in an async thread, but it is wait until all actions were executed.
        /// </summary>
        /// <typeparam name="T">Type of the items of the sequence.</typeparam>
        /// <param name="items">The sequence.</param>
        /// <param name="action">The action to invoke for an item of <paramref name="items" />.</param>
        /// <param name="throwExceptions">Throw exception (<see langword="true" />) or return them (<see langword="false" />).</param>
        /// <returns>The list of thrown exception or <see langword="null" /> if no exception was thrown.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" /> and/or <paramref name="action" />
        /// are <see langword="null" />.
        /// </exception>
        /// <exception cref="AggregateException">
        /// At leat one exception was thrown. This only works if <paramref name="throwExceptions" />
        /// is <see langword="true" />.
        /// </exception>
        public static AggregateException ForAllAsync<T>(IEnumerable<T> items,
                                                        Action<IForAllItemExecutionContext<T>> action,
                                                        bool throwExceptions)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return ForAllAsync<T, object>(items,
                                          delegate(IForAllItemExecutionContext<T, object> ctx)
                                          {
                                              SimpleForAllItemExecutionContext<T> ctxClone = new SimpleForAllItemExecutionContext<T>();
                                              ctxClone.Item = ctx.Item;

                                              action(ctxClone);
                                          },
                                          delegate(T i)
                                          {
                                              return null;
                                          },
                                          throwExceptions);
        }

        /// <summary>
        /// Invokes an action for each item of a sequence. Each thrown exception while invokation
        /// is collected and thrown or returned as an <see cref="AggregateException" />.
        /// The invokation of each action is done in an async thread, but it is wait until all actions were executed.
        /// </summary>
        /// <typeparam name="T">Type of the items of the sequence.</typeparam>
        /// <typeparam name="S">Type of the second paramater of <paramref name="action" />.</typeparam>
        /// <param name="items">The sequence.</param>
        /// <param name="action">The action to invoke for an item of <paramref name="items" />.</param>
        /// <param name="actionState">
        /// The value for the state argument of <paramref name="action" />.
        /// </param>
        /// <returns>The list of thrown exception or <see langword="null" /> if no exception was thrown.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" />and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="AggregateException">At leat one exception was thrown.</exception>
        /// <remarks>Exceptions are thrown.</remarks>
        public static AggregateException ForAllAsync<T, S>(IEnumerable<T> items,
                                                           Action<IForAllItemExecutionContext<T, S>> action,
                                                           S actionState)
        {
            return ForAllAsync<T, S>(items,
                                     action,
                                     actionState,
                                     true);
        }

        /// <summary>
        /// Invokes an action for each item of a sequence. Each thrown exception while invokation
        /// is collected and thrown or returned as an <see cref="AggregateException" />.
        /// The invokation of each action is done in an async thread, but it is wait until all actions were executed.
        /// </summary>
        /// <typeparam name="T">Type of the items of the sequence.</typeparam>
        /// <typeparam name="S">Type of the second paramater of <paramref name="action" />.</typeparam>
        /// <param name="items">The sequence.</param>
        /// <param name="action">The action to invoke for an item of <paramref name="items" />.</param>
        /// <param name="actionStateFactory">
        /// The factory delegate that produces the value for the second argument of <paramref name="action" />.
        /// </param>
        /// <returns>The list of thrown exception or <see langword="null" /> if no exception was thrown.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" />, <paramref name="action" /> and/or <paramref name="actionStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        /// <exception cref="AggregateException">At leat one exception was thrown.</exception>
        /// <remarks>Exceptions are thrown.</remarks>
        public static AggregateException ForAllAsync<T, S>(IEnumerable<T> items,
                                                           Action<IForAllItemExecutionContext<T, S>> action,
                                                           Func<T, S> actionStateFactory)
        {
            return ForAllAsync<T, S>(items,
                                     action,
                                     actionStateFactory,
                                     true);
        }

        /// <summary>
        /// Invokes an action for each item of a sequence. Each thrown exception while invokation
        /// is collected and thrown or returned as an <see cref="AggregateException" />.
        /// The invokation of each action is done in an async thread, but it is wait until all actions were executed.
        /// </summary>
        /// <typeparam name="T">Type of the items of the sequence.</typeparam>
        /// <typeparam name="S">Type of the second paramater of <paramref name="action" />.</typeparam>
        /// <param name="items">The sequence.</param>
        /// <param name="action">The action to invoke for an item of <paramref name="items" />.</param>
        /// <param name="actionState">
        /// The value for the state argument of <paramref name="action" />.
        /// </param>
        /// <param name="throwExceptions">Throw exception (<see langword="true" />) or return them (<see langword="false" />).</param>
        /// <returns>The list of thrown exception or <see langword="null" /> if no exception was thrown.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" />and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="AggregateException">
        /// At leat one exception was thrown. This only works if <paramref name="throwExceptions" />
        /// is <see langword="true" />.
        /// </exception>
        public static AggregateException ForAllAsync<T, S>(IEnumerable<T> items,
                                                           Action<IForAllItemExecutionContext<T, S>> action,
                                                           S actionState,
                                                           bool throwExceptions)
        {
            return ForAllAsync<T, S>(items,
                                     action,
                                     delegate(T i)
                                     {
                                         return actionState;
                                     },
                                     true);
        }

        #endregion Methods
    }
}
