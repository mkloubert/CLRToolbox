// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution;
using MarcelJoachimKloubert.CLRToolbox.Execution;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    partial class LoggerFacadeBase
    {
        #region Methods (1)

        // Private Methods (1) 

        private void LogInner(DateTimeOffset time,
                              Assembly asm,
                              LoggerFacadeCategories? categories,
                              string tag,
                              object msg)
        {
            try
            {
                if (DBNull.Value.Equals(msg))
                {
                    msg = null;
                }

                if (msg is IEnumerable<char>)
                {
                    msg = StringHelper.AsString((IEnumerable<char>)msg);
                }

                Thread thread = Thread.CurrentThread;

                MemberInfo member = null;
                try
                {
                    StackTrace st = new StackTrace(thread, false);
                    StackFrame sf = st.GetFrame(2);

                    member = sf.GetMethod();
                }
                catch
                {
                    member = null;
                }

                // collect categories
                List<LoggerFacadeCategories> listOfCategories = new List<LoggerFacadeCategories>();
                foreach (string s in categories.ToString()
                                               .Split(','))
                {
                    LoggerFacadeCategories loggerCat = (LoggerFacadeCategories)Enum.Parse(typeof(LoggerFacadeCategories), s, false);
                    if (!listOfCategories.Contains(loggerCat))
                    {
                        listOfCategories.Add(loggerCat);
                    }
                }

                listOfCategories.Sort();

                LogMessage orgMsg = new LogMessage()
                    {
                        Assembly = asm,
                        Categories = listOfCategories.ToArray(),
                        Context = Thread.CurrentContext,
                        Id = Guid.NewGuid(),
                        Member = member,
                        Message = msg,
                        Principal = Thread.CurrentPrincipal,
                        Tag = StringHelper.AsString(tag),
                        Thread = thread,
                        Time = time,
                    };

                ILogMessage messageToLog = orgMsg;

                ILogCommand logCmd = msg as ILogCommand;
                if (logCmd != null)
                {
                    // logic to execute

                    messageToLog = null;
                    if (logCmd.CanExecute(orgMsg))
                    {
                        ILogCommandExecutionResult result = logCmd.Execute(orgMsg);
                        if (result != null)
                        {
                            if (result.DoLogMessage)
                            {
                                // send 'result.MessageValueToLog'
                                // to "real" logger logic

                                messageToLog = CreateCopyOfLogMessage(orgMsg,
                                                                      result.MessageValueToLog);
                            }
                        }
                    }
                }
                else
                {
                    ICommand<ILogMessage> cmd = msg as ICommand<ILogMessage>;
                    if (cmd != null)
                    {
                        // general command

                        messageToLog = null;
                        if (cmd.CanExecute(orgMsg))
                        {
                            cmd.Execute(orgMsg);
                        }
                    }
                }

                this._ON_LOG_ACTION(messageToLog);
            }
            catch
            {
                // ignore errors
            }
        }

        #endregion Methods
    }
}
