// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    partial class LoggerFacadeBase
    {
        #region Methods (2)

        // Private Methods (2) 

        static partial void CreateCopyOfLogMessageExtension(ILogMessage src, LogMessage copy, object msgVal)
        {
            copy.Context = src.Context;
        }

        partial void LogInnerExtension(LogMessage logMsg,
                                       DateTimeOffset time,
                                       Assembly asm,
                                       LoggerFacadeCategories? categories,
                                       string tag,
                                       object msg)
        {
            logMsg.Context = Thread.CurrentContext;
            logMsg.Principal = Thread.CurrentPrincipal;
        }

        #endregion Methods

        #region Nested Classes (1)

        partial class LogMessage
        {
            #region Fields (1)

            private Context _context;

            #endregion Fields

            #region Properties (1)

            public Context Context
            {
                get { return this._context; }

                set { this._context = value; }
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}
