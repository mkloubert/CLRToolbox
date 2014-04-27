// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class WinFormsHelper
    {
        #region Methods (6) 

        // Public Methods (6) 

        /// <summary>
        /// Invokes logic of a control thread safe.
        /// </summary>
        /// <typeparam name="TCtrl">Type of the control.</typeparam>
        /// <param name="ctrl">The control.</param>
        /// <param name="action">The logic to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" /> and/or <paramref name="action" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static void InvokeSafe<TCtrl>(TCtrl ctrl,
                                             Action<TCtrl> action) where TCtrl : global::System.Windows.Forms.Control
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            InvokeSafe<TCtrl, object>(ctrl,
                                      delegate(TCtrl c, object s)
                                      {
                                          action(c);
                                      },
                                      delegate(TCtrl c)
                                      {
                                          return null;
                                      });
        }

        /// <summary>
        /// Invokes logic of a control thread safe.
        /// </summary>
        /// <typeparam name="TCtrl">Type of the control.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="ctrl">The control.</param>
        /// <param name="func">The logic to invoke.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" /> and/or <paramref name="func" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static TResult InvokeSafe<TCtrl, TResult>(TCtrl ctrl,
                                                         Func<TCtrl, TResult> func) where TCtrl : global::System.Windows.Forms.Control
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return InvokeSafe<TCtrl, object, TResult>(ctrl,
                                                      delegate(TCtrl c, object s)
                                                      {
                                                          return func(c);
                                                      },
                                                      delegate(TCtrl c)
                                                      {
                                                          return null;
                                                      });
        }

        /// <summary>
        /// Invokes logic of a control thread safe.
        /// </summary>
        /// <typeparam name="TCtrl">Type of the control.</typeparam>
        /// <typeparam name="TState">Type fo the second argument of <paramref name="action" />.</typeparam>
        /// <param name="ctrl">The control.</param>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="actionState">The second argument for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" /> and/or <paramref name="action" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static void InvokeSafe<TCtrl, TState>(TCtrl ctrl,
                                                     Action<TCtrl, TState> action,
                                                     TState actionState) where TCtrl : global::System.Windows.Forms.Control
        {
            InvokeSafe<TCtrl, TState>(ctrl,
                                      action,
                                      delegate(TCtrl c)
                                      {
                                          return actionState;
                                      });
        }

        /// <summary>
        /// Invokes logic of a control thread safe.
        /// </summary>
        /// <typeparam name="TCtrl">Type of the control.</typeparam>
        /// <typeparam name="TState">Type fo the second argument of <paramref name="action" />.</typeparam>
        /// <param name="ctrl">The control.</param>
        /// <param name="action">The logic to invoke.</param>
        /// <param name="actionStateFactory">The factory that produces the second argument for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" />, <paramref name="action" /> and/or <paramref name="actionStateFactory" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static void InvokeSafe<TCtrl, TState>(TCtrl ctrl,
                                                     Action<TCtrl, TState> action,
                                                     Func<TCtrl, TState> actionStateFactory) where TCtrl : global::System.Windows.Forms.Control
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            InvokeSafe<TCtrl, TState, object>(ctrl,
                                              delegate(TCtrl c, TState state)
                                              {
                                                  action(c, state);
                                                  return null;
                                              },
                                              actionStateFactory);
        }

        /// <summary>
        /// Invokes logic of a control thread safe.
        /// </summary>
        /// <typeparam name="TCtrl">Type of the control.</typeparam>
        /// <typeparam name="TState">Type fo the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="ctrl">The control.</param>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="funcState">The second argument for <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" /> and/or <paramref name="func" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static TResult InvokeSafe<TCtrl, TState, TResult>(TCtrl ctrl,
                                                                 Func<TCtrl, TState, TResult> func,
                                                                 TState funcState) where TCtrl : global::System.Windows.Forms.Control
        {
            return InvokeSafe<TCtrl, TState, TResult>(ctrl,
                                                      func,
                                                      delegate(TCtrl c)
                                                      {
                                                          return funcState;
                                                      });
        }

        /// <summary>
        /// Invokes logic of a control thread safe.
        /// </summary>
        /// <typeparam name="TCtrl">Type of the control.</typeparam>
        /// <typeparam name="TState">Type fo the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="ctrl">The control.</param>
        /// <param name="func">The logic to invoke.</param>
        /// <param name="funcStateFactory">The factory that produces the second argument for <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ctrl" />, <paramref name="func" /> and/or <paramref name="funcStateFactory" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static TResult InvokeSafe<TCtrl, TState, TResult>(TCtrl ctrl,
                                                                 Func<TCtrl, TState, TResult> func,
                                                                 Func<TCtrl, TState> funcStateFactory) where TCtrl : global::System.Windows.Forms.Control
        {
            if (ctrl == null)
            {
                throw new ArgumentNullException("ctrl");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            if (ctrl.InvokeRequired)
            {
                return (TResult)ctrl.Invoke(new Func<TCtrl, Func<TCtrl, TState, TResult>, Func<TCtrl, TState>, TResult>(InvokeSafe<TCtrl, TState, TResult>),
                                            ctrl,
                                            func,
                                            funcStateFactory);
            }

            return func(ctrl,
                        funcStateFactory(ctrl));
        }

        #endregion Methods 
    }
}
