// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox.Execution.Impl;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Input
{
    #region CLASS: SimpleCommand<TParam>

    /// <summary>
    /// <see cref="global::System.Windows.Input.ICommand" /> extension of <see cref="DelegateCommand{TParam}" />.
    /// </summary>
    /// <typeparam name="TParam">Type of the underlying parameters.</typeparam>
    public sealed class SimpleCommand<TParam> : DelegateCommand<TParam>, global::System.Windows.Input.ICommand
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}"/> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <param name="canExecutePredicate">The logic for <see cref="DelegateCommand{TParam}.CanExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public SimpleCommand(ExecuteHandler executeAction,
                             CanExecutePredicate canExecutePredicate)
            : base(executeAction,
                   canExecutePredicate)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}"/> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public SimpleCommand(ExecuteHandler executeAction)
            : base(executeAction)
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Private Methods (1) 

        bool global::System.Windows.Input.ICommand.CanExecute(object parameter)
        {
            return this.CanExecute(TMConvert.ChangeType<TParam>(parameter));
        }

        void global::System.Windows.Input.ICommand.Execute(object parameter)
        {
            this.Execute(TMConvert.ChangeType<TParam>(parameter));
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: SimpleCommand

    /// <summary>
    /// <see cref="global::System.Windows.Input.ICommand" /> extension of <see cref="DelegateCommand" />.
    /// </summary>
    public sealed class SimpleCommand : DelegateCommand, global::System.Windows.Input.ICommand
    {
        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCommand"/> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <param name="canExecutePredicate">The logic for <see cref="DelegateCommand{TParam}.CanExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public SimpleCommand(ExecuteHandler executeAction,
                             CanExecutePredicate canExecutePredicate)
            : base(executeAction,
                   canExecutePredicate)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <param name="canExecutePredicate">The logic for <see cref="DelegateCommand{TParam}.CanExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public SimpleCommand(ExecuteHandlerNoParameter executeAction,
                             CanExecuteHandlerNoParameter canExecutePredicate)
            : base(executeAction,
                   canExecutePredicate)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public SimpleCommand(ExecuteHandler executeAction)
            : base(executeAction)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public SimpleCommand(ExecuteHandlerNoParameter executeAction)
            : base(executeAction)
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Private Methods (1) 

        void global::System.Windows.Input.ICommand.Execute(object parameter)
        {
            this.Execute(parameter);
        }

        #endregion Methods
    }

    #endregion
}
