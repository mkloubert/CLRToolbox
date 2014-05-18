// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Windows.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows
{
    static partial class ClrToolboxWpfExtensionMethods
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        /// Invoke the <see cref="Dispatcher.BeginInvoke(Delegate, DispatcherPriority, object[])" /> method of the
        /// dispatcher of a <see cref="DispatcherObject" /> directly.
        /// </summary>
        /// <typeparam name="TDisp">Specific type of the dispatcher object.</typeparam>
        /// <param name="obj">The object that has an own <see cref="Dispatcher" />.</param>
        /// <param name="action">The action to invoke.</param>
        /// <returns>The dispatcher operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Action is invoked with <see cref="DispatcherPriority.Normal" /> priority.
        /// </remarks>
        public static DispatcherOperation BeginInvoke<TDisp>(this TDisp obj, Action<TDisp> action)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return BeginInvoke<TDisp>(obj,
                                      action,
                                      DispatcherPriority.Normal);
        }

        /// <summary>
        /// Invoke the <see cref="Dispatcher.BeginInvoke(Delegate, DispatcherPriority, object[])" /> method of the
        /// dispatcher of a <see cref="DispatcherObject" /> directly.
        /// </summary>
        /// <typeparam name="TDisp">Specific type of the dispatcher object.</typeparam>
        /// <param name="obj">The object that has an own <see cref="Dispatcher" />.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="prio">The priority.</param>
        /// <returns>The dispatcher operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public static DispatcherOperation BeginInvoke<TDisp>(this TDisp obj, Action<TDisp> action, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return BeginInvoke<TDisp, object>(obj,
                                             (o, s) => action(o), (o) => null,
                                             prio);
        }

        /// <summary>
        /// Invoke the <see cref="Dispatcher.BeginInvoke(Delegate, DispatcherPriority, object[])" /> method of the
        /// dispatcher of a <see cref="DispatcherObject" /> directly.
        /// </summary>
        /// <typeparam name="TDisp">Specific type of the dispatcher object.</typeparam>
        /// <typeparam name="T">Type of the second parameter of <paramref name="action" />.</typeparam>
        /// <param name="obj">The object that has an own <see cref="Dispatcher" />.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">
        /// The second parameter for <paramref name="action" />.
        /// </param>
        /// <returns>The dispatcher operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="action" />
        /// are <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Action is invoked with <see cref="DispatcherPriority.Normal" /> priority.
        /// </remarks>
        public static DispatcherOperation BeginInvoke<TDisp, T>(this TDisp obj, Action<TDisp, T> action, T actionState)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return BeginInvoke<TDisp, T>(obj,
                                         action, actionState,
                                         DispatcherPriority.Normal);
        }

        /// <summary>
        /// Invoke the <see cref="Dispatcher.BeginInvoke(Delegate, DispatcherPriority, object[])" /> method of the
        /// dispatcher of a <see cref="DispatcherObject" /> directly.
        /// </summary>
        /// <typeparam name="TDisp">Specific type of the dispatcher object.</typeparam>
        /// <typeparam name="T">Type of the second parameter of <paramref name="action" />.</typeparam>
        /// <param name="obj">The object that has an own <see cref="Dispatcher" />.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">
        /// The second parameter for <paramref name="action" />.
        /// </param>
        /// <param name="prio">The priority.</param>
        /// <returns>The dispatcher operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="action" />
        /// are <see langword="null" />.
        /// </exception>
        public static DispatcherOperation BeginInvoke<TDisp, T>(this TDisp obj, Action<TDisp, T> action, T actionState, DispatcherPriority prio)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return BeginInvoke<TDisp, T>(obj,
                                         action, (o) => actionState,
                                         prio);
        }

        /// <summary>
        /// Invoke the <see cref="Dispatcher.BeginInvoke(Delegate, DispatcherPriority, object[])" /> method of the
        /// dispatcher of a <see cref="DispatcherObject" /> directly.
        /// </summary>
        /// <typeparam name="TDisp">Specific type of the dispatcher object.</typeparam>
        /// <typeparam name="T">Type of the second parameter of <paramref name="action" />.</typeparam>
        /// <param name="obj">The object that has an own <see cref="Dispatcher" />.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">
        /// The factory that creates the second parameter for <paramref name="action" />.
        /// </param>
        /// <returns>The dispatcher operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" />, <paramref name="action" /> and/or <paramref name="actionStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Action is invoked with <see cref="DispatcherPriority.Normal" /> priority.
        /// </remarks>
        public static DispatcherOperation BeginInvoke<TDisp, T>(this TDisp obj, Action<TDisp, T> action, Func<TDisp, T> actionStateFactory)
            where TDisp : global::System.Windows.Threading.DispatcherObject
        {
            return BeginInvoke<TDisp, T>(obj,
                                         action, actionStateFactory,
                                         DispatcherPriority.Normal);
        }

        /// <summary>
        /// Invoke the <see cref="Dispatcher.BeginInvoke(Delegate, DispatcherPriority, object[])" /> method of the
        /// dispatcher of a <see cref="DispatcherObject" /> directly.
        /// </summary>
        /// <typeparam name="TDisp">Specific type of the dispatcher object.</typeparam>
        /// <typeparam name="T">Type of the second parameter of <paramref name="action" />.</typeparam>
        /// <param name="obj">The object that has an own <see cref="Dispatcher" />.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">
        /// The factory that creates the second parameter for <paramref name="action" />.
        /// </param>
        /// <param name="prio">The priority.</param>
        /// <returns>The dispatcher operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" />, <paramref name="action" /> and/or <paramref name="actionStateFactory" />
        /// are <see langword="null" />.
        /// </exception>
        public static DispatcherOperation BeginInvoke<TDisp, T>(this TDisp obj, Action<TDisp, T> action, Func<TDisp, T> actionStateFactory, DispatcherPriority prio)
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

            return obj.Dispatcher
                      .BeginInvoke(action,
                                   prio,
                                   obj, actionStateFactory(obj));
        }

        #endregion Methods
    }
}