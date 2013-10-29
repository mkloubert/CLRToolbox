// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Execution;

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

        #region Methods (2)

        // Protected Methods (1) 

        /// <summary>
        /// The logic for <see cref="LogCommandBase.Execute(ILogMessage)" /> command.
        /// </summary>
        /// <param name="context">The execution context.</param>
        protected abstract void OnExecute(ILogCommandExecutionContext context);
        // Private Methods (1) 

        private LogCommandExecutionContext CreateBasicExecutionContext(ILogMessage msg)
        {
            LogCommandExecutionContext result = new LogCommandExecutionContext();
            result.DoLogMessage = false;
            result.Command = this;
            result.Message = msg;
            result.MessageValueToLog = null;

            return result;
        }

        #endregion Methods

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CommandBase{TParam}.OnExecute(TParam)"/>
        protected override sealed void OnExecute(ILogMessage param)
        {
            LogCommandExecutionContext ctx = this.CreateBasicExecutionContext(param);

            this.OnExecute(ctx);
        }
    }
}
