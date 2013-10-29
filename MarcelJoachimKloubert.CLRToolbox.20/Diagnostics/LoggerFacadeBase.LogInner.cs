// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
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

                this._ON_LOG_ACTION(new LogMessage()
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
                    });
            }
            catch
            {
                // ignore errors
            }
        }

        #endregion Methods
    }
}
