// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;
using System.Threading;
using SyncJobActionQueue = System.Collections.Concurrent.ConcurrentQueue<MarcelJoachimKloubert.FileSyncer.Jobs.Actions.ISyncJobAction>;

namespace MarcelJoachimKloubert.FileSyncer.Jobs.Actions
{
    internal class DelegateSyncAction : SyncJobActionBase
    {
        #region Fields (5)

        private readonly SyncActionHandler _ACTION;
        private readonly CancellationTokenSource _CANCEL_SRC;
        private readonly DirectoryInfo _DESTINATION;
        private readonly SyncJobActionQueue _QUEUE;
        private readonly DirectoryInfo _SOURCE;

        #endregion Fields

        #region Constructors (1)

        internal DelegateSyncAction(SyncActionHandler action,
                                    DirectoryInfo src, DirectoryInfo dest)
        {
            this._ACTION = action;

            this._SOURCE = src;
            this._DESTINATION = dest;
        }

        #endregion Constructors

        #region Delegates and Events (1)

        // Delegates (1) 

        internal delegate void SyncActionHandler(DirectoryInfo src, DirectoryInfo dest,
                                                 ISyncJobExecutionContext ctx);

        #endregion Delegates and Events

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnExecute(ISyncJobExecutionContext ctx)
        {
            this._ACTION(this._SOURCE, this._DESTINATION,
                         ctx);
        }

        #endregion Methods
    }
}
