// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using System;
using System.Windows.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows
{
    static partial class ClrToolboxWpfExtensionMethods
    {
        #region Methods (12)

        // Public Methods (12) 

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        public static void Invoke<TDisp>(this TDisp obj, Action<TDisp> action)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            Invoke<TDisp>(obj, action, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="prio">The priority to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        public static void Invoke<TDisp>(this TDisp obj, Action<TDisp> action, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Invoke<TDisp, Action<TDisp>>(obj,
                                         delegate(TDisp o, Action<TDisp> a)
                                         {
                                             a(o);
                                         },
                                         action,
                                         prio);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">The function that provides the state object for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        public static void Invoke<TDisp, S>(this TDisp obj, Action<TDisp, S> action, S actionState)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            Invoke<TDisp, S>(obj,
                             action, actionState,
                             DispatcherPriority.Normal);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">The function that provides the state object for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" />, <paramref name="action" /> and/or <paramref name="actionStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        public static void Invoke<TDisp, S>(this TDisp obj, Action<TDisp, S> action, Func<TDisp, S> actionStateFactory)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            Invoke<TDisp, S>(obj,
                             action, actionStateFactory,
                             DispatcherPriority.Normal);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">The function that provides the state object for <paramref name="action" />.</param>
        /// <param name="prio">The priority to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        public static void Invoke<TDisp, S>(this TDisp obj, Action<TDisp, S> action, S actionState, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            Invoke<TDisp, S>(obj,
                             action,
                             delegate(TDisp o)
                             {
                                 return actionState;
                             },
                             prio);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">The function that provides the state object for <paramref name="action" />.</param>
        /// <param name="prio">The priority to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" />, <paramref name="action" /> and/or <paramref name="actionStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        public static void Invoke<TDisp, S>(this TDisp obj, Action<TDisp, S> action, Func<TDisp, S> actionStateFactory, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("actionStateFactory");
            }

            Invoke<TDisp, S, object>(obj,
                                     delegate(TDisp o, S s)
                                     {
                                         action(o, s);
                                         return null;
                                     }, actionStateFactory
                                      , prio);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="func">The function to invoke.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static TResult Invoke<TDisp, TResult>(this TDisp obj, Func<TDisp, TResult> func)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return Invoke<TDisp, TResult>(obj,
                                          func,
                                          DispatcherPriority.Normal);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="func">The function to invoke.</param>
        /// <param name="prio">The priority to use.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static TResult Invoke<TDisp, TResult>(this TDisp obj, Func<TDisp, TResult> func, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return Invoke<TDisp, Func<TDisp, TResult>, TResult>(obj,
                                                                delegate(TDisp o, Func<TDisp, TResult> f)
                                                                {
                                                                    return f(o);
                                                                },
                                                                func,
                                                                prio);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcState">The state object for <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static TResult Invoke<TDisp, S, TResult>(this TDisp obj, Func<TDisp, S, TResult> func, S funcState)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return Invoke<TDisp, S, TResult>(obj,
                                             func, funcState,
                                             DispatcherPriority.Normal);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcStateFactory">The function that provides the state object for <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" />, <paramref name="func" /> and/or <paramref name="funcStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        public static TResult Invoke<TDisp, S, TResult>(this TDisp obj, Func<TDisp, S, TResult> func, Func<TDisp, S> funcStateFactory)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return Invoke<TDisp, S, TResult>(obj,
                                             func, funcStateFactory,
                                             DispatcherPriority.Normal);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcState">The state object for <paramref name="func" />.</param>
        /// <param name="prio">The priority to use.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="func" /> are <see langword="null" />.
        /// </exception>
        public static TResult Invoke<TDisp, S, TResult>(this TDisp obj, Func<TDisp, S, TResult> func, S funcState, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return Invoke<TDisp, S, TResult>(obj,
                                             func,
                                             delegate(TDisp o)
                                             {
                                                 return funcState;
                                             },
                                             prio);
        }

        /// <summary>
        /// Directly invokes the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// of a <see cref="Dispatcher" /> instance that is provides by a <see cref="DispatcherObject.Dispatcher" /> property.
        /// </summary>
        /// <typeparam name="TDisp">Type of the dispatcher object.</typeparam>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="obj">The dispatcher object.</param>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcStateFactory">The function that provides the state object for <paramref name="func" />.</param>
        /// <param name="prio">The priority to use.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" />, <paramref name="func" /> and/or <paramref name="funcStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        public static TResult Invoke<TDisp, S, TResult>(this TDisp obj, Func<TDisp, S, TResult> func, Func<TDisp, S> funcStateFactory, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            Func<TResult> funcToInvoke = () =>
                {
                    return func(obj,
                                funcStateFactory(obj));
                };

            var dispatcher = obj.Dispatcher;
            if (dispatcher == null)
            {
                return funcToInvoke();
            }

            return GlobalConverter.Current
                                  .ChangeType<TResult>(dispatcher.Invoke(funcToInvoke,
                                                                         prio));
        }

        #endregion Methods
    }
}