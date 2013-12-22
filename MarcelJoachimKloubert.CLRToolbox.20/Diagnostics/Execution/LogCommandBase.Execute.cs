// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution
{
    partial class LogCommandBase
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ILogCommand.Execute(ILogMessage)"/>
        public new ILogCommandExecutionResult Execute(ILogMessage orgMsg)
        {
            if (!this.CanExecute(orgMsg))
            {
                return null;
            }

            LogCommandExecutionResult result = new LogCommandExecutionResult();
            result.Command = this;
            result.Message = orgMsg;

            try
            {
                LogCommandExecutionContext ctx = this.CreateBasicExecutionContext(orgMsg);

                this.OnExecute(ctx);

                result.Errors = new Exception[0];
                result.DoLogMessage = ctx.DoLogMessage;
                result.MessageValueToLog = ctx.MessageValueToLog;
            }
            catch (Exception ex)
            {
                AggregateException aggEx = ex as AggregateException;
                if (aggEx == null)
                {
                    aggEx = new AggregateException(new Exception[] { ex });
                }

                result.Errors = CollectionHelper.AsArray(aggEx.Flatten().InnerExceptions);

                result.DoLogMessage = false;
                result.MessageValueToLog = null;
            }

            return result;
        }

        #endregion Methods
    }
}
