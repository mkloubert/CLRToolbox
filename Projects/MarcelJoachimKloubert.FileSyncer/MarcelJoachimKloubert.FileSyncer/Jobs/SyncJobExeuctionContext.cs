// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Threading;
using SyncJobActionQueue = System.Collections.Concurrent.ConcurrentQueue<MarcelJoachimKloubert.FileSyncer.Jobs.Actions.ISyncJobAction>;

namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    internal sealed class SyncJobExeuctionContext : ISyncJobExecutionContext
    {
        #region Properties (7)

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
    }
}
