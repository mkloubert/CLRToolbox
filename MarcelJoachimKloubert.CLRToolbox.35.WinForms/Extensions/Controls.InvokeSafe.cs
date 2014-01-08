// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms
{
    static partial class ClrToolboxWinFormsExtensionMethods
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="WinFormsHelper.InvokeSafe{TCtrl}(TCtrl, Action{TCtrl})" />
        public static void InvokeSafe<TCtrl>(this TCtrl ctrl,
                                             Action<TCtrl> action) where TCtrl : global::System.Windows.Forms.Control
        {
            WinFormsHelper.InvokeSafe<TCtrl>(ctrl,
                                             action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="WinFormsHelper.InvokeSafe{TCtrl, TResult}(TCtrl, Func{TCtrl, TResult})" />
        public static TResult InvokeSafe<TCtrl, TResult>(this TCtrl ctrl,
                                                         Func<TCtrl, TResult> func) where TCtrl : global::System.Windows.Forms.Control
        {
            return WinFormsHelper.InvokeSafe<TCtrl, TResult>(ctrl,
                                                             func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="WinFormsHelper.InvokeSafe{TCtrl, TState}(TCtrl, Action{TCtrl, TState}, TState)" />
        public static void InvokeSafe<TCtrl, TState>(this TCtrl ctrl,
                                                     Action<TCtrl, TState> action,
                                                     TState actionState) where TCtrl : global::System.Windows.Forms.Control
        {
            WinFormsHelper.InvokeSafe<TCtrl, TState>(ctrl,
                                                     action, actionState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="WinFormsHelper.InvokeSafe{TCtrl, TState}(TCtrl, Action{TCtrl, TState}, Func{TCtrl, TState})" />
        public static void InvokeSafe<TCtrl, TState>(this TCtrl ctrl,
                                                     Action<TCtrl, TState> action,
                                                     Func<TCtrl, TState> actionStateFactory) where TCtrl : global::System.Windows.Forms.Control
        {
            WinFormsHelper.InvokeSafe<TCtrl, TState>(ctrl,
                                                     action, actionStateFactory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="WinFormsHelper.InvokeSafe{TCtrl, TState, TResult}(TCtrl, Func{TCtrl, TState, TResult}, TState)" />
        public static TResult InvokeSafe<TCtrl, TState, TResult>(this TCtrl ctrl,
                                                                 Func<TCtrl, TState, TResult> func,
                                                                 TState funcState) where TCtrl : global::System.Windows.Forms.Control
        {
            return WinFormsHelper.InvokeSafe<TCtrl, TState, TResult>(ctrl,
                                                                     func, funcState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="WinFormsHelper.InvokeSafe{TCtrl, TState, TResult}(TCtrl, Func{TCtrl, TState, TResult}, Func{TCtrl, TState})" />
        public static TResult InvokeSafe<TCtrl, TState, TResult>(this TCtrl ctrl,
                                                                 Func<TCtrl, TState, TResult> func,
                                                                 Func<TCtrl, TState> funcStateFactory) where TCtrl : global::System.Windows.Forms.Control
        {
            return WinFormsHelper.InvokeSafe<TCtrl, TState, TResult>(ctrl,
                                                                     func, funcStateFactory);
        }

        #endregion Methods
    }
}
