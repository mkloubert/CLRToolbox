// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

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

            var result = new LogCommandExecutionResult();
            result.Command = this;
            result.Message = orgMsg;

            try
            {
                var ctx = this.CreateBasicExecutionContext(orgMsg);

                this.OnExecute(ctx);

                result.Errors = new Exception[0];
                result.DoLogMessage = ctx.DoLogMessage;
                result.MessageValueToLog = ctx.MessageValueToLog;
            }
            catch (Exception ex)
            {
                var aggEx = ex as AggregateException;
                if (aggEx != null)
                {
                    result.Errors = aggEx.InnerExceptions;
                }
                else
                {
                    result.Errors = new Exception[] { ex };
                }

                result.DoLogMessage = false;
                result.MessageValueToLog = null;
            }

            return result;
        }

        #endregion Methods
    }
}
