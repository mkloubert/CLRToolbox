// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution;
using MarcelJoachimKloubert.CLRToolbox.Execution;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    partial class LoggerFacadeBase
    {
        #region Methods (2)

        // Private Methods (2) 

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
#if !MONO2 && !MONO20 && !MONO4 && !MONO40
                try
                {
#pragma warning disable 618
                    global::System.Diagnostics.StackTrace st = new global::System.Diagnostics.StackTrace(thread, false);
#pragma warning restore 618
                    global::System.Diagnostics.StackFrame sf = st.GetFrame(2);

                    member = sf.GetMethod();
                }
                catch
                {
                    member = null;
                }

#endif
                // collect categories
                List<LoggerFacadeCategories> listOfCategories = new List<LoggerFacadeCategories>();

                string strCategories = categories.ToString();
                if (StringHelper.IsNullOrWhiteSpace(strCategories) == false)
                {
                    foreach (string s in strCategories.Split(','))
                    {
                        LoggerFacadeCategories loggerCat = (LoggerFacadeCategories)Enum.Parse(typeof(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.LoggerFacadeCategories), s, false);
                        if (listOfCategories.Contains(loggerCat) == false)
                        {
                            listOfCategories.Add(loggerCat);
                        }
                    }
                }

                listOfCategories.Sort();

                LogMessage orgMsg = new LogMessage()
                    {
                        Assembly = asm,
                        Categories = listOfCategories.ToArray(),
                        Id = Guid.NewGuid(),
                        LogTag = StringHelper.AsString(tag),
                        Member = member,
                        Message = msg,
                        Thread = thread,
                        Time = time,
                    };

                this.LogInnerExtension(orgMsg,
                                       time,
                                       asm,
                                       categories,
                                       tag,
                                       msg);

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

        partial void LogInnerExtension(LogMessage logMsg,
                                       DateTimeOffset time,
                                       Assembly asm,
                                       LoggerFacadeCategories? categories,
                                       string tag,
                                       object msg);

        #endregion Methods
    }
}
