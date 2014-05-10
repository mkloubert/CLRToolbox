// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution.Impl
{
    /// <summary>
    /// A log command based on delegates.
    /// </summary>
    public sealed class DelegateLogCommand : LogCommandBase
    {
        #region Fields (3)

        private readonly object[] _ARGUMENTS;
        private readonly CanExecuteLogCommandHandler _CAN_EXECUTE_LOG_MESSAGE;
        private readonly ExecuteLogCommandHandler _EXECUTE_LOG_MESSAGE;

        #endregion Fields

        #region Constructors (1)

        private DelegateLogCommand(ExecuteLogCommandHandler executeLogCommand,
                                   CanExecuteLogCommandHandler canExecuteLogMessage,
                                   object[] args)
        {
            if (executeLogCommand == null)
            {
                throw new ArgumentNullException("executeLogCommand");
            }

            this._EXECUTE_LOG_MESSAGE = executeLogCommand;
            this._CAN_EXECUTE_LOG_MESSAGE = canExecuteLogMessage ?? base.CanExecute;
            this._ARGUMENTS = args ?? new object[] { null };
        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// Describes a predicate for the <see cref="DelegateLogCommand.CanExecute(ILogMessage)" /> method.
        /// </summary>
        /// <param name="orgMsg">The original message.</param>
        /// <returns>Command can be executed with the parameter in <paramref name="orgMsg" /> or not.</returns>
        public delegate bool CanExecuteLogCommandHandler(ILogMessage orgMsg);

        /// <summary>
        /// Describes a predicate for the <see cref="DelegateLogCommand.OnExecute(ILogCommandExecutionContext)" /> method.
        /// </summary>
        /// <param name="context">The execution context.</param>
        public delegate void ExecuteLogCommandHandler(ILogCommandExecutionContext context);

        #endregion Delegates and Events

        #region Methods (7)

        // Public Methods (5) 

        /// <inheriteddoc />
        public override bool CanExecute(ILogMessage param)
        {
            return this._CAN_EXECUTE_LOG_MESSAGE(param);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DelegateLogCommand"/> class.
        /// </summary>
        /// <param name="executeLogCommand">The logic for <see cref="DelegateLogCommand.OnExecute(ILogCommandExecutionContext)" />.</param>
        /// <param name="args">The execution arguments.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeLogCommand" /> is <see langword="null" />.
        /// </exception>
        public static DelegateLogCommand Create(ExecuteLogCommandHandler executeLogCommand,
                                                params object[] args)
        {
            return Create(executeLogCommand,
                          (CanExecuteLogCommandHandler)null,
                          args);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DelegateLogCommand"/> class.
        /// </summary>
        /// <param name="executeLogCommand">The logic for <see cref="DelegateLogCommand.OnExecute(ILogCommandExecutionContext)" />.</param>
        /// <param name="args">The execution arguments.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeLogCommand" /> is <see langword="null" />.
        /// </exception>
        public static DelegateLogCommand Create(ExecuteLogCommandHandler executeLogCommand,
                                                IEnumerable<object> args)
        {
            return Create(executeLogCommand,
                          CollectionHelper.AsArray(args));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DelegateLogCommand"/> class.
        /// </summary>
        /// <param name="executeLogCommand">The logic for <see cref="DelegateLogCommand.OnExecute(ILogCommandExecutionContext)" />.</param>
        /// <param name="canExecuteLogMessage">The logic for <see cref="DelegateLogCommand.CanExecute(ILogMessage)" />.</param>
        /// <param name="args">The execution arguments.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeLogCommand" /> is <see langword="null" />.
        /// </exception>
        public static DelegateLogCommand Create(ExecuteLogCommandHandler executeLogCommand,
                                                CanExecuteLogCommandHandler canExecuteLogMessage,
                                                params object[] args)
        {
            return new DelegateLogCommand(executeLogCommand,
                                          canExecuteLogMessage,
                                          args);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DelegateLogCommand"/> class.
        /// </summary>
        /// <param name="executeLogCommand">The logic for <see cref="DelegateLogCommand.OnExecute(ILogCommandExecutionContext)" />.</param>
        /// <param name="canExecuteLogMessage">The logic for <see cref="DelegateLogCommand.CanExecute(ILogMessage)" />.</param>
        /// <param name="args">The execution arguments.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executeLogCommand" /> is <see langword="null" />.
        /// </exception>
        public static DelegateLogCommand Create(ExecuteLogCommandHandler executeLogCommand,
                                                CanExecuteLogCommandHandler canExecuteLogMessage,
                                                IEnumerable<object> args)
        {
            return Create(executeLogCommand,
                          canExecuteLogMessage,
                          CollectionHelper.AsArray(args));
        }

        // Protected Methods (2) 

        /// <inheriteddoc />
        protected override IEnumerable<object> GetExecutionArguments(ILogMessage msg)
        {
            return this._ARGUMENTS;
        }

        /// <inheriteddoc />
        protected override void OnExecute(ILogCommandExecutionContext context)
        {
            this._EXECUTE_LOG_MESSAGE(context);
        }

        #endregion Methods
    }
}