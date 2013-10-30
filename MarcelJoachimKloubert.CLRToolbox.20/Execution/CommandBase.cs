﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution
{
    /// <summary>
    /// A basic command.
    /// </summary>
    /// <typeparam name="TParam">Type of the parameters.</typeparam>
    public abstract class CommandBase<TParam> : ICommand<TParam>
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC = new object();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase{TParam}" /> class.
        /// </summary>
        protected CommandBase()
        {

        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Events (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICommand{TParam}.CanExecuteChanged" />
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICommand{TParam}.ExecutionError" />
        public event EventHandler<ExecutionErrorEventArgs<TParam>> ExecutionError;

        #endregion Delegates and Events

        #region Methods (5)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICommand{TParam}.CanExecute(TParam)" />
        public virtual bool CanExecute(TParam param)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICommand{TParam}.Execute(TParam)" />
        public bool? Execute(TParam param)
        {
            if (!this.CanExecute(param))
            {
                return null;
            }

            try
            {
                this.OnExecute(param);
                return true;
            }
            catch (Exception ex)
            {
                if (!this.RaiseExecutionError(param, ex))
                {
                    // re-throw exception because no event was raised
                    throw;
                }

                return false;
            }
        }

        /// <summary>
        /// Raises the <see cref="CommandBase{TParam}.CanExecuteChanged" /> event.
        /// </summary>
        /// <returns>Event was raised or not.</returns>
        public bool RaiseCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
                return true;
            }

            return false;
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic of the <see cref="CommandBase{TParam}.Execute(TParam)" /> method.
        /// </summary>
        /// <param name="param">The parameter for the execution.</param>
        protected abstract void OnExecute(TParam param);
        // Private Methods (1) 

        private bool RaiseExecutionError(TParam param, Exception ex)
        {
            EventHandler<ExecutionErrorEventArgs<TParam>> handler = this.ExecutionError;
            if (handler != null)
            {
                handler(this, new ExecutionErrorEventArgs<TParam>(param, ex));
                return true;
            }

            return false;
        }

        #endregion Methods
    }
}