// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Impl
{
    #region CLASS: DelegateCommand<TParam>

    /// <summary>
    /// A command that uses delegates.
    /// </summary>
    /// <typeparam name="TParam">Type of the parameters.</typeparam>
    public class DelegateCommand<TParam> : CommandBase<TParam>
    {
        #region Fields (2)

        private CanExecuteHandler _canExecutePredicate;
        private ExecuteHandler _executeAction;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}"/> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <param name="canExecutePredicate">The logic for <see cref="DelegateCommand{TParam}.CanExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public DelegateCommand(ExecuteHandler executeAction,
                               CanExecuteHandler canExecutePredicate)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }

            this._executeAction = executeAction;
            this._canExecutePredicate = canExecutePredicate ?? base.CanExecute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}"/> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public DelegateCommand(ExecuteHandler executeAction)
            : this(executeAction, null)
        {

        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// Describes a predicate for the <see cref="DelegateCommand{TParam}.CanExecute(TParam)" /> method.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>Command can be executed with the parameter in <paramref name="param" /> or not.</returns>
        public delegate bool CanExecuteHandler(TParam param);

        /// <summary>
        /// Describes a predicate for the <see cref="DelegateCommand{TParam}.OnExecute(TParam)" /> method.
        /// </summary>
        /// <param name="param">The parameter.</param>
        public delegate void ExecuteHandler(TParam param);

        #endregion Delegates and Events

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CommandBase{TParam}.CanExecute(TParam)" />
        public override bool CanExecute(TParam param)
        {
            return this._canExecutePredicate(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CommandBase{TParam}.OnExecute(TParam)" />
        protected override sealed void OnExecute(TParam param)
        {
            this._executeAction(param);
        }

        #endregion Methods

    }

    #endregion

    #region CLASS: DelegateCommand

    /// <summary>
    /// A command that uses delegates.
    /// </summary>
    public sealed class DelegateCommand : DelegateCommand<object>
    {
        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}"/> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <param name="canExecutePredicate">The logic for <see cref="DelegateCommand{TParam}.CanExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public DelegateCommand(ExecuteHandler executeAction,
                               CanExecuteHandler canExecutePredicate)
            : base(executeAction, canExecutePredicate)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}" /> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <param name="canExecutePredicate">The logic for <see cref="DelegateCommand{TParam}.CanExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public DelegateCommand(ExecuteHandlerNoParameter executeAction,
                               CanExecuteHandlerNoParameter canExecutePredicate)
            : this(delegate(object param) { executeAction(); },
                   delegate(object param) { return canExecutePredicate != null ? canExecutePredicate() : true; })
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}" /> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public DelegateCommand(ExecuteHandler executeAction)
            : base(executeAction)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TParam}" /> class.
        /// </summary>
        /// <param name="executeAction">The logic for <see cref="DelegateCommand{TParam}.OnExecute(TParam)" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeAction" /> is <see langword="null" />.
        /// </exception>
        public DelegateCommand(ExecuteHandlerNoParameter executeAction)
            : this(delegate(object param) { executeAction(); })
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }
        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DelegateCommand{TParam}.CanExecuteHandler" />.
        public delegate bool CanExecuteHandlerNoParameter();

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DelegateCommand{TParam}.ExecuteHandler" />.
        public delegate void ExecuteHandlerNoParameter();

        #endregion Delegates and Events
    }

    #endregion
}
