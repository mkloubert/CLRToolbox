// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Threading;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.FileSyncer.Diagnostics;
using SyncJobActionQueue = System.Collections.Concurrent.ConcurrentQueue<MarcelJoachimKloubert.FileSyncer.Jobs.Actions.ISyncJobAction>;

namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    internal sealed class SyncJobExeuctionContext : ISyncJobExecutionContext
    {
        #region Properties (9)

        public CancellationTokenSource CancelSource
        {
            get;
            internal set;
        }

        public CancellationToken CancelToken
        {
            get { return this.CancelSource.Token; }
        }

        public string DestionationDirectory
        {
            get;
            internal set;
        }

        public ISyncJob Job
        {
            get;
            internal set;
        }

        internal LogHandler LogAction
        {
            get;
            set;
        }

        internal Action<IEnumerable<char>, int?> ProgressChangedHandler
        {
            get;
            set;
        }

        public SyncJobActionQueue Queue
        {
            get;
            internal set;
        }

        public string SourceDirectory
        {
            get;
            internal set;
        }

        public object SyncRoot
        {
            get;
            internal set;
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Delegates (1) 

        internal delegate void LogHandler(DateTimeOffset time,
                                          SyncLogType? type,
                                          string tag,
                                          object msg);

        #endregion Delegates and Events

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISyncJobExecutionContext.Log(object, SyncLogType?, " />
        public void Log(object msg,
                        IEnumerable<char> tag = null,
                        SyncLogType? type = null)
        {
            try
            {
                var now = DateTimeOffset.Now;

                var handler = this.LogAction;
                if (handler != null)
                {
                    var strTag = (tag.AsString() ?? string.Empty).Trim();

                    if (DBNull.Value.Equals(msg))
                    {
                        msg = null;
                    }

                    handler(now,
                            type,
                            strTag != string.Empty ? strTag : string.Empty,
                            msg);
                }
            }
            catch
            {
                // ignore
            }
        }

        public void RaiseProgressChanged(IEnumerable<char> text = null,
                                         int? percentage = null)
        {
            var handler = this.ProgressChangedHandler;
            if (handler != null)
            {
                handler(text, percentage);
            }
        }

        #endregion Methods
    }
}
