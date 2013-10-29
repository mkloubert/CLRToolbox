// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    partial class AsyncLogger
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="LoggerFacadeBase.OnLog(ILogMessage)" />
        protected override void OnLog(ILogMessage msg)
        {
            ThreadPool.QueueUserWorkItem(this.OnLogAsync,
                                         msg);
        }

        #endregion Methods
    }
}
