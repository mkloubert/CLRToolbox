﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Execution;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

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

        #region Methods (3)

        // Protected Methods (2) 

        /// <summary>
        /// Returns the list of arguments for a 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected virtual IEnumerable<object> GetExecutionArguments(ILogMessage msg)
        {
            return null;
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