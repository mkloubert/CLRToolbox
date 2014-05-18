// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution;
using MarcelJoachimKloubert.CLRToolbox.Execution.Commands;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    partial class LoggerFacadeBase
    {
        #region Methods (2)

        // Private Methods (2) 

        private bool LogInner(DateTimeOffset time,
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
#if !(MONO2 || MONO20 || MONO4 || MONO40)

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
                    // ignore errors here
                }

#endif
                // collect categories
                List<LoggerFacadeCategories> listOfCategories = new List<LoggerFacadeCategories>();

                string strCategories = categories.ToString();
                if (StringHelper.IsNullOrWhiteSpace(strCategories) == false)
                {
                    CollectionHelper.ForEach(strCategories.Split(','),
                                             delegate(IForEachItemExecutionContext<string, List<LoggerFacadeCategories>> ctx)
                                             {
                                                 List<LoggerFacadeCategories> loc = ctx.State;

                                                 LoggerFacadeCategories loggerCat = (LoggerFacadeCategories)Enum.Parse(typeof(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.LoggerFacadeCategories),
                                                                                                                       ctx.Item,    // enum value name
                                                                                                                       false);

                                                 if (loc.Contains(loggerCat) == false)
                                                 {
                                                     loc.Add(loggerCat);
                                                 }
                                             }, listOfCategories);
                }

                listOfCategories.Sort();

                LogMessage orgMsg = new LogMessage();
                orgMsg.Assembly = asm;
                orgMsg.Categories = listOfCategories.ToArray();
                orgMsg.Id = Guid.NewGuid();
                orgMsg.LogTag = StringHelper.AsString(tag);
                orgMsg.Member = member;
                orgMsg.Message = msg;
                orgMsg.Thread = thread;
                orgMsg.Time = time;

#if !WINDOWS_PHONE

                try
                {
                    orgMsg.Context = global::System.Threading.Thread.CurrentContext;
                }
                catch
                {
                    // ignore errors here
                }

                try
                {
                    orgMsg.Principal = global::System.Threading.Thread.CurrentPrincipal;
                }
                catch
                {
                    // ignore errors here
                }

#endif

                ILogMessage messageToLog = orgMsg;

                bool checkAgain;
                do
                {
                    checkAgain = false;

                    if (messageToLog == null)
                    {
                        break;
                    }

                    ILogCommand logCmd = messageToLog.Message as ILogCommand;
                    if (logCmd != null)
                    {
                        // logic to execute

                        messageToLog = null;
                        if (logCmd.CanExecute(orgMsg))
                        {
                            ILogCommandExecutionResult result = logCmd.Execute(orgMsg);
                            if (result != null)
                            {
                                if (result.HasFailed)
                                {
                                    throw new AggregateException(result.Errors);
                                }

                                if (result.DoLogMessage)
                                {
                                    // send 'result.MessageValueToLog'
                                    // to "real" logger logic

                                    messageToLog = CreateCopyOfLogMessage(orgMsg,
                                                                          result.MessageValueToLog);
                                }
                            }
                        }

                        // maybe 'messageToLog' can be a log command again
                        checkAgain = true;
                    }
                    else
                    {
                        ICommand<ILogMessage> cmd = messageToLog.Message as ICommand<ILogMessage>;
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
                }
                while (checkAgain);

                this._ON_LOG_ACTION(messageToLog);
                return true;
            }
            catch
            {
                // ignore errors
                return false;
            }
        }

        #endregion Methods
    }
}