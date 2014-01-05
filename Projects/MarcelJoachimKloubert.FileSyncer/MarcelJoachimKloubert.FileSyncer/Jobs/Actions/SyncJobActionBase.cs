// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.FileSyncer.Jobs.Actions
{
    /// <summary>
    /// A basic sync job action.
    /// </summary>
    public abstract class SyncJobActionBase : TMObject, ISyncJobAction
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncJobActionBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected SyncJobActionBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncJobActionBase" /> class.
        /// </summary>
        protected SyncJobActionBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISyncJobAction.Execute(ISyncJobExecutionContext)" />
        public void Execute(ISyncJobExecutionContext ctx)
        {
            lock (this._SYNC)
            {
                if (ctx == null)
                {
                    throw new ArgumentNullException("ctx");
                }

                try
                {
                    this.OnExecute(ctx);
                }
                catch (Exception ex)
                {
                    throw ex as AggregateException ?? new AggregateException(ex);
                }
            }
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for <see cref="SyncJobActionBase.Execute()" /> method.
        /// </summary>
        /// <param name="ctx">The underlying context.</param>
        protected abstract void OnExecute(ISyncJobExecutionContext ctx);

        #endregion Methods
    }
}
