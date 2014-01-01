// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace AppServerProcessManager.Views
{
    partial class MainViewModel
    {
        #region Nested Classes (1)

        private sealed class OnBusyArgs<T>
        {
            #region Fields (3)

            internal readonly Action<MainViewModel, T> ACTION;
            internal readonly T STATE;
            internal readonly MainViewModel VIEW_MODEL;

            #endregion Fields

            #region Constructors (1)

            internal OnBusyArgs(MainViewModel vm,
                                Action<MainViewModel, T> action,
                                T state)
            {
                this.ACTION = action;
                this.STATE = state;
                this.VIEW_MODEL = vm;
            }

            #endregion Constructors

            #region Methods (1)

            // Internal Methods (1) 

            internal void InvokeAction()
            {
                this.ACTION(this.VIEW_MODEL,
                            this.STATE);
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
