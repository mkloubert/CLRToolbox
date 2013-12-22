// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Simple implementation of the <see cref="IAppServerInitContext" /> interface.
    /// </summary>
    public sealed class SimpleAppServerInitContext : IAppServerInitContext
    {
        #region Properties (4)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerInitContext.Arguments" />
        public IList<string> Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerInitContext.Logger" />
        public ILoggerFacade Logger
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerInitContext.ServerContext" />
        public IAppServerContext ServerContext
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerInitContext.WorkingDirectory" />
        public string WorkingDirectory
        {
            get;
            set;
        }

        #endregion Properties
    }
}
