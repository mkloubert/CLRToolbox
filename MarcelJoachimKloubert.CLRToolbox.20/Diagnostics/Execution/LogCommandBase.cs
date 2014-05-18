// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Execution.Commands;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution
{
    /// <summary>
    /// A basic log command.
    /// </summary>
    public abstract partial class LogCommandBase : CommandBase<ILogMessage>, ILogCommand
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="LogCommandBase"/> class.
        /// </summary>
        protected LogCommandBase()
        {
        }

        #endregion Constructors

        #region Methods (4)

        // Protected Methods (3) 

        /// <summary>
        /// Returns the list of arguments for an execution.
        /// </summary>
        /// <param name="msg">The log message from where to get the arguments from.</param>
        /// <returns>The list of arguments.</returns>
        protected virtual IEnumerable<object> GetExecutionArguments(ILogMessage msg)
        {
            return null;
        }

        /// <inheriteddoc />
        protected override sealed void OnExecute(ILogMessage param)
        {
            LogCommandExecutionContext ctx = this.CreateBasicExecutionContext(param);

            this.OnExecute(ctx);
        }

        /// <summary>
        /// The logic for <see cref="LogCommandBase.Execute(ILogMessage)" /> command.
        /// </summary>
        /// <param name="context">The execution context.</param>
        protected abstract void OnExecute(ILogCommandExecutionContext context);

        // Private Methods (1) 

        private LogCommandExecutionContext CreateBasicExecutionContext(ILogMessage msg)
        {
            LogCommandExecutionContext result = new LogCommandExecutionContext();
            result.Arguments = CollectionHelper.AsArray(this.GetExecutionArguments(msg)) ?? new object[0];
            result.DoLogMessage = false;
            result.Command = this;
            result.Message = msg;
            result.MessageValueToLog = null;

            return result;
        }

        #endregion Methods
    }
}