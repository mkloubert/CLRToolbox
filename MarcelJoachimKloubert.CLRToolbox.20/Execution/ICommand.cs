// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution
{
    /// <summary>
    /// Describes a command.
    /// </summary>
    /// <typeparam name="TParam">Type of the parameters.</typeparam>
    public interface ICommand<TParam> : ITMObject
    {
        #region Delegates and Events (2)

        // Events (2) 

        /// <summary>
        /// Is invoked if <see cref="ICommand{TParam}.CanExecute(TParam)" /> might return another result.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Is invoked if <see cref="ICommand{TParam}.Execute(TParam)" /> has thrown an exception.
        /// </summary>
        event EventHandler<ExecutionErrorEventArgs<TParam>> ExecutionError;

        #endregion Delegates and Events

        #region Operations (2)

        /// <summary>
        /// Returns if command can be executed by using a specific parameter or not.
        /// </summary>
        /// <param name="param">The parameter to check.</param>
        /// <returns>Command can be executed or not.</returns>
        bool CanExecute(TParam param);

        /// <summary>
        /// Executes the command with a specific parameter.
        /// </summary>
        /// <param name="param">The parameter to check.</param>
        /// <returns>
        /// Command was executed successfully or the execution false.
        /// <see langword="null" /> indicates that command cannot be executed with the value
        /// of <paramref name="param" />.
        /// </returns>
        bool? Execute(TParam param);

        #endregion Operations
    }
}